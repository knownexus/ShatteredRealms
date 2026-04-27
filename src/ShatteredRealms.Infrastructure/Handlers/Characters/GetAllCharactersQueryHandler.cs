using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Characters;
using ShatteredRealms.Application.Features.Characters.Queries;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Characters;

public sealed class GetAllCharactersQueryHandler : IRequestHandler<GetAllCharactersQuery, Result<List<CharacterDto>>>
{
    private readonly ApplicationDbContext _context;

    public GetAllCharactersQueryHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result<List<CharacterDto>>> Handle(GetAllCharactersQuery request, CancellationToken cancellationToken)
    {
        var characters = await _context.Character
            .Include(c => c.Owner)
            .Include(c => c.Position)
            .OrderBy(c => c.Owner != null ? c.Owner.UserName : string.Empty)
            .ThenBy(c => c.CreatedAt)
            .ToListAsync(cancellationToken);

        return Result.Success(characters.Select(c => CreateCharacterCommandHandler.MapToDto(c, null)).ToList());
    }
}
