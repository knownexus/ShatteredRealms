using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Events;
using ShatteredRealms.Application.Features.Events.Queries;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Events;

public sealed class GetEventAttendeesQueryHandler : IRequestHandler<GetEventAttendeesQuery, Result<List<EventAttendeeDto>>>
{
    private readonly ApplicationDbContext _context;

    public GetEventAttendeesQueryHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result<List<EventAttendeeDto>>> Handle(GetEventAttendeesQuery request, CancellationToken cancellationToken)
    {
        var eventExists = await _context.Event.AnyAsync(e => e.Id == request.EventId, cancellationToken);
        if (!eventExists)
            return Result.Failure<List<EventAttendeeDto>>(DomainErrors.Event.NotFound);

        var attendees = await _context.EventAttendee
            .Where(a => a.EventId == request.EventId)
            .Include(a => a.User)
            .OrderBy(a => a.RegisteredAt)
            .Select(a => new EventAttendeeDto
            {
                UserId       = a.UserId,
                UserName     = a.User.UserName ?? string.Empty,
                RegisteredAt = a.RegisteredAt,
            })
            .ToListAsync(cancellationToken);

        return Result.Success(attendees);
    }
}
