using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.Features.Auth.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Auth;

public sealed class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand, Result>
{
    private readonly ApplicationDbContext _context;
    private readonly IAnalyticsService _analytics;

    public RevokeTokenCommandHandler(ApplicationDbContext context, IAnalyticsService analytics)
    {
        _context = context;
        _analytics = analytics;
    }

    public async Task<Result> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
    {
        var storedToken = await _context.RefreshToken
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken, cancellationToken);

        if (storedToken is not { IsActive: true })
        {
            return Result.Failure(DomainErrors.Authentication.InvalidRefreshToken);
        }

        storedToken.RevokedAt = DateTime.UtcNow;
        storedToken.RevokedByIp = request.RequestingIpAddress;
        await _context.SaveChangesAsync(cancellationToken);

        await _analytics.TrackAsync(TelemetryActionType.UserLoggedOut,
            storedToken.UserId, storedToken.User?.Email ?? string.Empty,
            cancellationToken: cancellationToken);

        return Result.Success();
    }
}
