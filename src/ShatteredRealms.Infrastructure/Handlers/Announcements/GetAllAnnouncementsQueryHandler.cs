using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Announcements;
using ShatteredRealms.Application.Features.Announcements.Queries;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Announcements;

public sealed class GetAllAnnouncementsQueryHandler : IRequestHandler<GetAllAnnouncementsQuery, Result<List<AnnouncementDto>>>
{
    private readonly ApplicationDbContext _context;

    public GetAllAnnouncementsQueryHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result<List<AnnouncementDto>>> Handle(GetAllAnnouncementsQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.Announcement
            .Include(a => a.Author)
            .Include(a => a.LinkedEvent)
            .OrderByDescending(a => a.CreatedAt)
            .Select(a => new AnnouncementDto
            {
                Id               = a.Id,
                Title            = a.Title,
                Body             = a.Body,
                LinkedEventId    = a.LinkedEventId,
                LinkedEventTitle = a.LinkedEvent != null ? a.LinkedEvent.Title : null,
                AuthorId         = a.AuthorId,
                AuthorName       = a.Author.UserName ?? string.Empty,
                CreatedAt        = a.CreatedAt,
                UpdatedAt        = a.UpdatedAt,
            })
            .ToListAsync(cancellationToken);

        return Result.Success(list);
    }
}
