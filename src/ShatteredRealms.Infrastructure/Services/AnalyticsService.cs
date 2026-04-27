using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Services;

public sealed class AnalyticsService : IAnalyticsService
{
    private readonly ApplicationDbContext _context;

    public AnalyticsService(ApplicationDbContext context) => _context = context;

    public async Task TrackAsync(
        TelemetryEventType eventType,
        string actorId,
        string actorEmail,
        string? targetId = null,
        string? targetName = null,
        string? details = null,
        CancellationToken cancellationToken = default)
    {
        var actorName = actorEmail;
        var actorRole = string.Empty;

        if (!string.IsNullOrEmpty(actorId))
        {
            var user = await _context.Users.FindAsync([actorId], cancellationToken);
            if (user is not null)
            {
                actorName  = user.UserName ?? user.Email ?? actorId;
                actorEmail = user.Email    ?? actorEmail;
            }

            actorRole = await _context.UserRoles
                .Where(ur => ur.UserId == actorId)
                .Join(_context.Roles, ur => ur.RoleId, r => r.Id, (_, r) => r.Name)
                .OrderByDescending(name => name)
                .FirstOrDefaultAsync(cancellationToken) ?? string.Empty;
        }

        _context.TelemetryEvent.Add(new TelemetryEvent
        {
            Id         = Guid.NewGuid(),
            EventType  = eventType,
            ActorId    = actorId,
            ActorName  = actorName ?? actorId,
            ActorEmail = actorEmail,
            ActorRole  = actorRole,
            TargetId   = targetId,
            TargetName = targetName,
            Details    = details,
            OccurredAt = DateTime.UtcNow,
        });

        await _context.SaveChangesAsync(cancellationToken);
    }
}
