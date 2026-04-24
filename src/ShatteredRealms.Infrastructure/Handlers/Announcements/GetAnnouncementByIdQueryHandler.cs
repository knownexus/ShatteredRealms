using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Announcements;
using ShatteredRealms.Application.Features.Announcements.Queries;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Announcements;

public sealed class GetAnnouncementByIdQueryHandler : IRequestHandler<GetAnnouncementByIdQuery, Result<AnnouncementDto>>
{
    private readonly ApplicationDbContext _context;

    public GetAnnouncementByIdQueryHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result<AnnouncementDto>> Handle(GetAnnouncementByIdQuery request, CancellationToken cancellationToken)
    {
        var a = await _context.Announcement
            .Include(a => a.Author)
            .Include(a => a.LinkedEvent)
            .Where(a => a.Id == request.Id)
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
            .FirstOrDefaultAsync(cancellationToken);

        return a is not null
            ? Result.Success(a)
            : Result.Failure<AnnouncementDto>(DomainErrors.Announcement.NotFound);
    }
}
