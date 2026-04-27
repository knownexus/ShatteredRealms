using MediatR;
using ShatteredRealms.Application.Features.Announcements.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Announcements;

public sealed class DeleteAnnouncementCommandHandler : IRequestHandler<DeleteAnnouncementCommand, Result>
{
    private readonly ApplicationDbContext _context;
    private readonly IAnalyticsService _analytics;

    public DeleteAnnouncementCommandHandler(ApplicationDbContext context, IAnalyticsService analytics)
    {
        _context = context;
        _analytics = analytics;
    }

    public async Task<Result> Handle(DeleteAnnouncementCommand request, CancellationToken cancellationToken)
    {
        var announcement = await _context.Announcement.FindAsync([request.Id], cancellationToken);
        if (announcement is null)
            return Result.Failure(DomainErrors.Announcement.NotFound);

        announcement.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);

        if (!string.IsNullOrEmpty(request.ActorId))
        {
            await _analytics.TrackAsync(TelemetryEventType.AnnouncementDeleted, request.ActorId, string.Empty,
                targetId: announcement.Id.ToString(), targetName: announcement.Title, cancellationToken: cancellationToken);
        }

        return Result.Success();
    }
}
