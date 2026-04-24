using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Announcements;
using ShatteredRealms.Application.Features.Announcements.Commands;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Announcements;

public sealed class UpdateAnnouncementCommandHandler : IRequestHandler<UpdateAnnouncementCommand, Result<AnnouncementDto>>
{
    private readonly ApplicationDbContext _context;

    public UpdateAnnouncementCommandHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result<AnnouncementDto>> Handle(UpdateAnnouncementCommand request, CancellationToken cancellationToken)
    {
        var announcement = await _context.Announcement.FindAsync([request.Id], cancellationToken);
        if (announcement is null)
            return Result.Failure<AnnouncementDto>(DomainErrors.Announcement.NotFound);

        if (request.LinkedEventId.HasValue)
        {
            var eventExists = await _context.Event
                .AnyAsync(e => e.Id == request.LinkedEventId.Value, cancellationToken);
            if (!eventExists)
                return Result.Failure<AnnouncementDto>(DomainErrors.Event.NotFound);
        }

        announcement.Title         = request.Title;
        announcement.Body          = request.Body;
        announcement.LinkedEventId = request.LinkedEventId;
        announcement.UpdatedAt     = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        var linkedEventTitle = announcement.LinkedEventId.HasValue
            ? await _context.Event
                .Where(e => e.Id == announcement.LinkedEventId.Value)
                .Select(e => e.Title)
                .FirstOrDefaultAsync(cancellationToken)
            : null;

        var author = await _context.Users.FindAsync([announcement.AuthorId], cancellationToken);

        return Result.Success(new AnnouncementDto
        {
            Id               = announcement.Id,
            Title            = announcement.Title,
            Body             = announcement.Body,
            LinkedEventId    = announcement.LinkedEventId,
            LinkedEventTitle = linkedEventTitle,
            AuthorId         = announcement.AuthorId,
            AuthorName       = author?.UserName ?? string.Empty,
            CreatedAt        = announcement.CreatedAt,
            UpdatedAt        = announcement.UpdatedAt,
        });
    }
}
