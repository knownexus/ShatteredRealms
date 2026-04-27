using MediatR;
using ShatteredRealms.Application.DTOs.Roles;
using ShatteredRealms.Application.Features.Roles.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Roles;

public sealed class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Result<RoleDto>>
{
    private readonly IRoleService _roleService;
    private readonly IAnalyticsService _analytics;

    public CreateRoleCommandHandler(IRoleService roleService, IAnalyticsService analytics)
    {
        _roleService = roleService;
        _analytics = analytics;
    }

    public async Task<Result<RoleDto>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var result = await _roleService.CreateRoleAsync(new CreateRoleRequest
        {
            Name = request.Name,
            Description = request.Description,
            PermissionIds = request.PermissionIds,
        }, cancellationToken);

        if (result.IsSuccess && !string.IsNullOrEmpty(request.ActorId))
        {
            await _analytics.TrackAsync(TelemetryEventType.RoleCreated, request.ActorId, string.Empty,
                targetId: result.Value.Id, targetName: result.Value.Name, cancellationToken: cancellationToken);
        }

        return result;
    }
}
