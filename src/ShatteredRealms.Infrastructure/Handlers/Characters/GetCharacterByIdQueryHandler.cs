using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Characters;
using ShatteredRealms.Application.Features.Characters.Queries;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Characters;

public sealed class GetCharacterByIdQueryHandler : IRequestHandler<GetCharacterByIdQuery, Result<CharacterDto>>
{
    private readonly ApplicationDbContext _context;

    public GetCharacterByIdQueryHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result<CharacterDto>> Handle(GetCharacterByIdQuery request, CancellationToken cancellationToken)
    {
        var character = await _context.Character
            .Include(c => c.Owner)
            .FirstOrDefaultAsync(c => c.Id == request.CharacterId, cancellationToken);

        return character is null
            ? Result.Failure<CharacterDto>(DomainErrors.Character.NotFound)
            : Result.Success(CreateCharacterCommandHandler.MapToDto(character, null));
    }
}
