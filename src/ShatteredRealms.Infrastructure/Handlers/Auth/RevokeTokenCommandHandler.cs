using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.Features.Auth.Commands;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Auth;

public sealed class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand, Result>
{
    private readonly ApplicationDbContext _context;

    public RevokeTokenCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
    {
        var storedToken = await _context.RefreshToken
            .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken, cancellationToken);

        if (storedToken is not { IsActive: true })
        {
            return Result.Failure(DomainErrors.Authentication.InvalidRefreshToken);
        }

        storedToken.RevokedAt = DateTime.UtcNow;
        storedToken.RevokedByIp = request.RequestingIpAddress;
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
