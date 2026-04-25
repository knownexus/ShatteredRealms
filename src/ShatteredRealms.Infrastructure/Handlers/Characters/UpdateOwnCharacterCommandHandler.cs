using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Characters;
using ShatteredRealms.Application.Features.Characters.Commands;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Characters;

public sealed class UpdateOwnCharacterCommandHandler : IRequestHandler<UpdateOwnCharacterCommand, Result<CharacterDto>>
{
    private readonly ApplicationDbContext _context;

    public UpdateOwnCharacterCommandHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result<CharacterDto>> Handle(UpdateOwnCharacterCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return Result.Failure<CharacterDto>(DomainErrors.Character.NameRequired);

        if (string.IsNullOrWhiteSpace(request.Nationality))
            return Result.Failure<CharacterDto>(DomainErrors.Character.NationalityRequired);

        var character = await _context.Character
            .Include(c => c.Owner)
            .FirstOrDefaultAsync(c => c.Id == request.CharacterId, cancellationToken);

        if (character is null)
            return Result.Failure<CharacterDto>(DomainErrors.Character.NotFound);

        if (character.UserId != request.RequestingUserId)
            return Result.Failure<CharacterDto>(DomainErrors.Character.NotOwner);

        character.Name        = request.Name.Trim();
        character.Nationality = request.Nationality.Trim();

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success(CreateCharacterCommandHandler.MapToDto(character, null));
    }
}
