using MediatR;
using ShatteredRealms.Application.DTOs.Permissions;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Permissions.Queries;

public sealed record GetAllPermissionsQuery : IRequest<Result<List<PermissionDto>>>;
