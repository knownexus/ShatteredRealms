using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Events;
using ShatteredRealms.Application.Features.Events.Queries;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Events;

public sealed class GetMyEventsQueryHandler : IRequestHandler<GetMyEventsQuery, Result<List<EventDto>>>
{
    private readonly ApplicationDbContext _context;

    public GetMyEventsQueryHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result<List<EventDto>>> Handle(GetMyEventsQuery request, CancellationToken cancellationToken)
    {
        var events = await _context.EventAttendee
            .Where(a => a.UserId == request.UserId)
            .Select(a => new EventDto
            {
                Id             = a.Event.Id,
                Title          = a.Event.Title,
                Description    = a.Event.Description,
                StartsAt       = a.Event.StartsAt,
                EndsAt         = a.Event.EndsAt,
                Location       = a.Event.Location,
                BannerImagePath = a.Event.BannerImagePath,
                MemberCap      = a.Event.MemberCap,
                CreatedById    = a.Event.CreatedById,
                CreatedAt      = a.Event.CreatedAt,
                AttendeeCount  = a.Event.Attendees.Count,
                IsGoing        = true,
            })
            .OrderBy(e => e.StartsAt)
            .ToListAsync(cancellationToken);

        return Result.Success(events);
    }
}
