using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Characters;
using ShatteredRealms.Application.Features.Characters.Queries;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Characters;

public sealed class GetCharacterHistoryQueryHandler : IRequestHandler<GetCharacterHistoryQuery, Result<List<CharacterHistoryDto>>>
{
    private readonly ApplicationDbContext _context;

    public GetCharacterHistoryQueryHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result<List<CharacterHistoryDto>>> Handle(GetCharacterHistoryQuery request, CancellationToken cancellationToken)
    {
        var exists = await _context.Character.AnyAsync(c => c.Id == request.CharacterId, cancellationToken);
        if (!exists)
            return Result.Failure<List<CharacterHistoryDto>>(DomainErrors.Character.NotFound);

        var history = await _context.ActivityLog
            .Where(al => al.CharacterId == request.CharacterId)
            .Include(al => al.User)
            .OrderByDescending(al => al.Date)
            .Select(al => new CharacterHistoryDto
            {
                Date            = al.Date,
                Description     = al.Description,
                PerformedByName = al.User != null ? (al.User.UserName ?? al.UserId) : al.UserId,
            })
            .ToListAsync(cancellationToken);

        return Result.Success(history);
    }
}
