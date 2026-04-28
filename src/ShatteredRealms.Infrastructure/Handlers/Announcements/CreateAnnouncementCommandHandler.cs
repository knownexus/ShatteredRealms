using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Announcements;
using ShatteredRealms.Application.Features.Announcements.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities.Announcement;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Announcements;

public sealed class CreateAnnouncementCommandHandler : IRequestHandler<CreateAnnouncementCommand, Result<AnnouncementDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IAnalyticsService _analytics;

    public CreateAnnouncementCommandHandler(ApplicationDbContext context, IAnalyticsService analytics)
    {
        _context = context;
        _analytics = analytics;
    }

    public async Task<Result<AnnouncementDto>> Handle(CreateAnnouncementCommand request, CancellationToken cancellationToken)
    {
        if (request.LinkedEventId.HasValue)
        {
            var eventExists = await _context.Event
                .AnyAsync(e => e.Id == request.LinkedEventId.Value, cancellationToken);
            if (!eventExists)
            {
                return Result.Failure<AnnouncementDto>(DomainErrors.Event.NotFound);
            }
        }

        var announcement = new Announcement
        {
            Title         = request.Title,
            Body          = request.Body,
            LinkedEventId = request.LinkedEventId,
            AuthorId      = request.AuthorId,
            CreatedAt     = DateTime.UtcNow,
        };

        _context.Announcement.Add(announcement);
        await _context.SaveChangesAsync(cancellationToken);

        await _analytics.TrackAsync(TelemetryEventType.AnnouncementCreated, request.AuthorId, string.Empty,
            targetId: announcement.Id.ToString(), targetName: announcement.Title, cancellationToken: cancellationToken);

        var author = await _context.Users.FindAsync([request.AuthorId], cancellationToken);

        return Result.Success(new AnnouncementDto
        {
            Id               = announcement.Id,
            Title            = announcement.Title,
            Body             = announcement.Body,
            LinkedEventId    = announcement.LinkedEventId,
            LinkedEventTitle = null,
            AuthorId         = announcement.AuthorId,
            AuthorName       = author?.UserName ?? string.Empty,
            CreatedAt        = announcement.CreatedAt,
            UpdatedAt        = announcement.UpdatedAt,
        });
    }
}
