using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Events;
using ShatteredRealms.Application.Features.Events.Queries;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Events;

public sealed class GetAllEventsQueryHandler : IRequestHandler<GetAllEventsQuery, Result<List<EventDto>>>
{
    private readonly ApplicationDbContext _context;

    public GetAllEventsQueryHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result<List<EventDto>>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        var query = _context.Event.AsQueryable();

        if (request.UpcomingOnly)
            query = query.Where(e => e.EndsAt >= now);

        var events = await query
            .OrderBy(e => e.StartsAt)
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
            .ToListAsync(cancellationToken);

        return Result.Success(events);
    }
}
