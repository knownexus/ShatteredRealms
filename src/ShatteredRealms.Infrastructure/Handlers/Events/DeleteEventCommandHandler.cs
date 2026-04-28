using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.Features.Events.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Events;

public sealed class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, Result>
{
    private readonly ApplicationDbContext _context;
    private readonly IAnalyticsService _analytics;

    public DeleteEventCommandHandler(ApplicationDbContext context, IAnalyticsService analytics)
    {
        _context = context;
        _analytics = analytics;
    }

    public async Task<Result> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        var ev = await _context.Event.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (ev is null)
        {
            return Result.Failure(DomainErrors.Event.NotFound);
        }

        ev.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);

        if (!string.IsNullOrEmpty(request.ActorId))
        {
            await _analytics.TrackAsync(TelemetryActionType.EventDeleted, request.ActorId, string.Empty,
                targetId: ev.Id.ToString(), targetName: ev.Title, cancellationToken: cancellationToken);
        }

        return Result.Success();
    }
}
