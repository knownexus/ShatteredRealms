using MediatR;
using ShatteredRealms.Application.Features.Announcements.Commands;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Announcements;

public sealed class DeleteAnnouncementCommandHandler : IRequestHandler<DeleteAnnouncementCommand, Result>
{
    private readonly ApplicationDbContext _context;

    public DeleteAnnouncementCommandHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result> Handle(DeleteAnnouncementCommand request, CancellationToken cancellationToken)
    {
        var announcement = await _context.Announcement.FindAsync([request.Id], cancellationToken);
        if (announcement is null)
            return Result.Failure(DomainErrors.Announcement.NotFound);

        announcement.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
