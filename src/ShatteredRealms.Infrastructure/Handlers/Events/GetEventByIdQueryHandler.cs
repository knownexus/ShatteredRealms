using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Events;
using ShatteredRealms.Application.Features.Events.Queries;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Events;

public sealed class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, Result<EventDto>>
{
    private readonly ApplicationDbContext _context;

    public GetEventByIdQueryHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result<EventDto>> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
    {
        var dto = await _context.Event
            .Where(e => e.Id == request.Id)
            .Select(e => new EventDto
            {
                Id             = e.Id,
                Title          = e.Title,
                Description    = e.Description,
                StartsAt       = e.StartsAt,
                EndsAt         = e.EndsAt,
                Location       = e.Location,
                BannerImagePath = e.BannerImagePath,
                MemberCap      = e.MemberCap,
                CreatedById    = e.CreatedById,
                CreatedAt      = e.CreatedAt,
                AttendeeCount  = e.Attendees.Count,
                IsGoing        = request.CurrentUserId != null && e.Attendees.Any(a => a.UserId == request.CurrentUserId),
            })
            .FirstOrDefaultAsync(cancellationToken);

        return dto is null
            ? Result.Failure<EventDto>(DomainErrors.Event.NotFound)
            : Result.Success(dto);
    }
}
