using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.Features.Events.Commands;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Events;

public sealed class SetEventBannerCommandHandler : IRequestHandler<SetEventBannerCommand, Result>
{
    private readonly ApplicationDbContext _context;

    public SetEventBannerCommandHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result> Handle(SetEventBannerCommand request, CancellationToken cancellationToken)
    {
        var ev = await _context.Event
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(e => e.Id == request.EventId, cancellationToken);

        if (ev is null)
            return Result.Failure(DomainErrors.Event.NotFound);

        ev.BannerImagePath = request.RelativePath;
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
