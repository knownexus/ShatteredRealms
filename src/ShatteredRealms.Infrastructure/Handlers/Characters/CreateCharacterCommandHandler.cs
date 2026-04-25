using MediatR;
using ShatteredRealms.Application.DTOs.Characters;
using ShatteredRealms.Application.Features.Characters.Commands;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Characters;

public sealed class CreateCharacterCommandHandler : IRequestHandler<CreateCharacterCommand, Result<CharacterDto>>
{
    private readonly ApplicationDbContext _context;

    public CreateCharacterCommandHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result<CharacterDto>> Handle(CreateCharacterCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return Result.Failure<CharacterDto>(DomainErrors.Character.NameRequired);

        if (string.IsNullOrWhiteSpace(request.Nationality))
            return Result.Failure<CharacterDto>(DomainErrors.Character.NationalityRequired);

        var character = new Domain.Entities.Character.Character
        {
            UserId      = request.UserId,
            Name        = request.Name.Trim(),
            Nationality = request.Nationality.Trim(),
            Level       = 1,
            Experience  = 0,
            CreatedAt   = DateTime.UtcNow,
        };

        _context.Character.Add(character);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(MapToDto(character, null));
    }

    internal static CharacterDto MapToDto(Domain.Entities.Character.Character c, string? ownerName) => new()
    {
        Id          = c.Id,
        UserId      = c.UserId,
        OwnerName   = ownerName ?? c.Owner?.UserName ?? string.Empty,
        Name        = c.Name,
        Nationality = c.Nationality,
        Level       = c.Level,
        Experience  = c.Experience,
        CreatedAt   = c.CreatedAt,
    };
}
