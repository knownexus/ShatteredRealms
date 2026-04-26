using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Characters;
using ShatteredRealms.Application.Features.Characters.Queries;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Characters;

public sealed class GetCharactersByUserQueryHandler : IRequestHandler<GetCharactersByUserQuery, Result<List<CharacterDto>>>
{
    private readonly ApplicationDbContext _context;

    public GetCharactersByUserQueryHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result<List<CharacterDto>>> Handle(GetCharactersByUserQuery request, CancellationToken cancellationToken)
    {
        var characters = await _context.Character
            .Include(c => c.Owner)
            .Include(c => c.Position)
            .Where(c => c.UserId == request.UserId)
            .OrderBy(c => c.CreatedAt)
            .ToListAsync(cancellationToken);

        return Result.Success(characters.Select(c => CreateCharacterCommandHandler.MapToDto(c, null)).ToList());
    }
}
