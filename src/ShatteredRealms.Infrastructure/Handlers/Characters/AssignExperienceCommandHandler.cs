using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Characters;
using ShatteredRealms.Application.Features.Characters.Commands;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Characters;

public sealed class AssignExperienceCommandHandler : IRequestHandler<AssignExperienceCommand, Result<CharacterDto>>
{
    private readonly ApplicationDbContext _context;

    public AssignExperienceCommandHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result<CharacterDto>> Handle(AssignExperienceCommand request, CancellationToken cancellationToken)
    {
        var character = await _context.Character
            .Include(c => c.Owner)
            .FirstOrDefaultAsync(c => c.Id == request.CharacterId, cancellationToken);

        if (character is null)
            return Result.Failure<CharacterDto>(DomainErrors.Character.NotFound);

        character.Experience = Math.Max(0, character.Experience + request.XpToAdd);

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success(CreateCharacterCommandHandler.MapToDto(character, null));
    }
}
