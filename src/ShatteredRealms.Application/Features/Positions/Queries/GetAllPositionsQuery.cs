using MediatR;
using ShatteredRealms.Application.DTOs.Positions;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Positions.Queries;

public sealed record GetAllPositionsQuery : IRequest<Result<List<PositionDto>>>;
