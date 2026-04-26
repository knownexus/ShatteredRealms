using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Characters;
using ShatteredRealms.Application.Features.Characters.Commands;
using ShatteredRealms.Domain.Entities.ActivityLog;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Characters;

public sealed class AssignPositionCommandHandler : IRequestHandler<AssignPositionCommand, Result<CharacterDto>>
{
    private readonly ApplicationDbContext _context;

    public AssignPositionCommandHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result<CharacterDto>> Handle(AssignPositionCommand request, CancellationToken cancellationToken)
    {
        var character = await _context.Character
            .Include(c => c.Owner)
            .Include(c => c.Position)
            .FirstOrDefaultAsync(c => c.Id == request.CharacterId, cancellationToken);

        if (character is null)
            return Result.Failure<CharacterDto>(DomainErrors.Character.NotFound);

        if (request.PositionId.HasValue)
        {
            var position = await _context.Position
                .FirstOrDefaultAsync(p => p.Id == request.PositionId.Value, cancellationToken);

            if (position is null)
                return Result.Failure<CharacterDto>(DomainErrors.Position.NotFound);

            character.PositionId = position.Id;
            character.Position   = position;
        }
        else
        {
            character.PositionId = null;
            character.Position   = null;
        }

        var positionName = character.Position?.Name ?? "none";
        _context.ActivityLog.Add(new ActivityLog
        {
            Id          = Guid.NewGuid(),
            UserId      = request.RequestingUserId,
            Description = $"Assigned position '{positionName}' to character '{character.Name}'",
            Date        = DateTime.UtcNow,
        });

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success(CreateCharacterCommandHandler.MapToDto(character, null));
    }
}
