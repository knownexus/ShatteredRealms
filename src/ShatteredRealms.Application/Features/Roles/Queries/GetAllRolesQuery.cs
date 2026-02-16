using MediatR;
using ShatteredRealms.Application.DTOs.Roles;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Roles.Queries;

public sealed record GetAllRolesQuery : IRequest<Result<List<RoleDto>>>;
