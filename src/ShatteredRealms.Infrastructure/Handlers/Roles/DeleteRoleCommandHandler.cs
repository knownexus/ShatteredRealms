using MediatR;
using ShatteredRealms.Application.Features.Roles.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Roles;

public sealed class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Result>
{
    private readonly IRoleService _roleService;
    private readonly IAnalyticsService _analytics;

    public DeleteRoleCommandHandler(IRoleService roleService, IAnalyticsService analytics)
    {
        _roleService = roleService;
        _analytics = analytics;
    }

    public async Task<Result> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var result = await _roleService.DeleteRoleAsync(request.RoleId, cancellationToken);
        if (result.IsSuccess && !string.IsNullOrEmpty(request.ActorId))
        {
            await _analytics.TrackAsync(TelemetryEventType.RoleDeleted, request.ActorId, string.Empty,
                targetId: request.RoleId, cancellationToken: cancellationToken);
        }
        return result;
    }
}
