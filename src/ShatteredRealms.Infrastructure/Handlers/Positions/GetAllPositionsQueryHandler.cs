using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Positions;
using ShatteredRealms.Application.Features.Positions.Queries;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Positions;

public sealed class GetAllPositionsQueryHandler : IRequestHandler<GetAllPositionsQuery, Result<List<PositionDto>>>
{
    private readonly ApplicationDbContext _context;

    public GetAllPositionsQueryHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result<List<PositionDto>>> Handle(GetAllPositionsQuery request, CancellationToken cancellationToken)
    {
        var positions = await _context.Position
            .OrderBy(p => p.Name)
            .Select(p => new PositionDto { Id = p.Id, Name = p.Name, Description = p.Description })
            .ToListAsync(cancellationToken);

        return Result.Success(positions);
    }
}
