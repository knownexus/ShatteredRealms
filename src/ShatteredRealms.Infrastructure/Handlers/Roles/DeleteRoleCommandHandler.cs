using MediatR;
using ShatteredRealms.Application.Features.Roles.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Roles;

public sealed class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Result>
{
    private readonly IRoleService _roleService;

    public DeleteRoleCommandHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public Task<Result> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        => _roleService.DeleteRoleAsync(request.RoleId, cancellationToken);
}
