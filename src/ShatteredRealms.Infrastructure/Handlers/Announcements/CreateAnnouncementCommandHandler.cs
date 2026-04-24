using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Announcements;
using ShatteredRealms.Application.Features.Announcements.Commands;
using ShatteredRealms.Domain.Entities.Announcement;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Announcements;

public sealed class CreateAnnouncementCommandHandler : IRequestHandler<CreateAnnouncementCommand, Result<AnnouncementDto>>
{
    private readonly ApplicationDbContext _context;

    public CreateAnnouncementCommandHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result<AnnouncementDto>> Handle(CreateAnnouncementCommand request, CancellationToken cancellationToken)
    {
        if (request.LinkedEventId.HasValue)
        {
            var eventExists = await _context.Event
                .AnyAsync(e => e.Id == request.LinkedEventId.Value, cancellationToken);
            if (!eventExists)
                return Result.Failure<AnnouncementDto>(DomainErrors.Event.NotFound);
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
