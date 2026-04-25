using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Characters;
using ShatteredRealms.Application.Features.Characters.Queries;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;
using ShatteredRealms.Infrastructure.Handlers.Characters;

namespace ShatteredRealms.Infrastructure.Handlers.Characters;

public sealed class GetMyCharactersQueryHandler : IRequestHandler<GetMyCharactersQuery, Result<List<CharacterDto>>>
{
    private readonly ApplicationDbContext _context;

    public GetMyCharactersQueryHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result<List<CharacterDto>>> Handle(GetMyCharactersQuery request, CancellationToken cancellationToken)
    {
        var characters = await _context.Character
            .Include(c => c.Owner)
            .Where(c => c.UserId == request.UserId)
            .OrderBy(c => c.CreatedAt)
            .ToListAsync(cancellationToken);

        return Result.Success(characters.Select(c => CreateCharacterCommandHandler.MapToDto(c, null)).ToList());
    }
}
