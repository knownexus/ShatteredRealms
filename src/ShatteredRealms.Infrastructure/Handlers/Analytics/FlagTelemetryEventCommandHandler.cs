using MediatR;
using ShatteredRealms.Application.Features.Analytics.Commands;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Analytics;

public sealed class FlagTelemetryEventCommandHandler : IRequestHandler<FlagTelemetryEventCommand, Result>
{
    private readonly ApplicationDbContext _context;

    public FlagTelemetryEventCommandHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result> Handle(FlagTelemetryEventCommand request, CancellationToken cancellationToken)
    {
        var ev = await _context.TelemetryEvent.FindAsync([request.EventId], cancellationToken);
        if (ev is null)
            return Result.Failure(DomainErrors.Analytics.EventNotFound);

        ev.IsFlagged   = true;
        ev.FlagReason  = request.Reason;
        ev.FlaggedAt   = DateTime.UtcNow;
        ev.FlaggedById = request.ActorId;

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}

public sealed class UnflagTelemetryEventCommandHandler : IRequestHandler<UnflagTelemetryEventCommand, Result>
{
    private readonly ApplicationDbContext _context;

    public UnflagTelemetryEventCommandHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result> Handle(UnflagTelemetryEventCommand request, CancellationToken cancellationToken)
    {
        var ev = await _context.TelemetryEvent.FindAsync([request.EventId], cancellationToken);
        if (ev is null)
            return Result.Failure(DomainErrors.Analytics.EventNotFound);

        ev.IsFlagged   = false;
        ev.FlagReason  = null;
        ev.FlaggedAt   = null;
        ev.FlaggedById = null;

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
