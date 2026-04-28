using MediatR;
using ShatteredRealms.Application.Features.Users.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Users;

public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result>
{
    private readonly IUserService _userService;
    private readonly IAnalyticsService _analytics;

    public DeleteUserCommandHandler(IUserService userService, IAnalyticsService analytics)
    {
        _userService = userService;
        _analytics = analytics;
    }

    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _userService.DeleteUserAsync(request.UserId, cancellationToken);
        if (result.IsSuccess)
        {
            var actorId = request.ActorId ?? request.UserId;
            await _analytics.TrackAsync(TelemetryActionType.UserDeleted, actorId, string.Empty,
                targetId: request.UserId, cancellationToken: cancellationToken);
        }
        return result;
    }
}
