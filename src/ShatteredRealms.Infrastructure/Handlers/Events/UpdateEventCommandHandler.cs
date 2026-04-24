using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Events;
using ShatteredRealms.Application.Features.Events.Commands;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Events;

public sealed class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Result<EventDto>>
{
    private readonly ApplicationDbContext _context;

    public UpdateEventCommandHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result<EventDto>> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        if (request.EndsAt <= request.StartsAt)
            return Result.Failure<EventDto>(DomainErrors.Event.InvalidDates);

        var ev = await _context.Event
            .Include(e => e.Attendees)
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (ev is null)
            return Result.Failure<EventDto>(DomainErrors.Event.NotFound);

        ev.Title       = request.Title;
        ev.Description = request.Description;
        ev.StartsAt    = request.StartsAt;
        ev.EndsAt      = request.EndsAt;
        ev.Location    = request.Location;
        ev.MemberCap   = request.MemberCap;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(new EventDto
        {
            Id            = ev.Id,
            Title         = ev.Title,
            Description   = ev.Description,
            StartsAt      = ev.StartsAt,
            EndsAt        = ev.EndsAt,
            Location      = ev.Location,
            BannerImagePath = ev.BannerImagePath,
            MemberCap     = ev.MemberCap,
            CreatedById   = ev.CreatedById,
            CreatedAt     = ev.CreatedAt,
            AttendeeCount = ev.Attendees.Count,
            IsGoing       = false,
        });
    }
}
