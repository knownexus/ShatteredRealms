using MediatR;
using ShatteredRealms.Application.DTOs.Roles;
using ShatteredRealms.Application.Features.Roles.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Roles;

public sealed class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Result<RoleDto>>
{
    private readonly IRoleService _roleService;
    private readonly IAnalyticsService _analytics;

    public UpdateRoleCommandHandler(IRoleService roleService, IAnalyticsService analytics)
    {
        _roleService = roleService;
        _analytics = analytics;
    }

    public async Task<Result<RoleDto>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var result = await _roleService.UpdateRoleAsync(request.RoleId, new UpdateRoleRequest
        {
            Name = request.Name,
            Description = request.Description,
            PermissionIds = request.PermissionIds,
        }, cancellationToken);

        if (result.IsSuccess && !string.IsNullOrEmpty(request.ActorId))
        {
            await _analytics.TrackAsync(TelemetryEventType.RoleUpdated, request.ActorId, string.Empty,
                targetId: request.RoleId, targetName: request.Name, cancellationToken: cancellationToken);
        }

        return result;
    }
}
