using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.Features.Characters.Commands;
using ShatteredRealms.Domain.Entities.ActivityLog;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Characters;

public sealed class DeleteCharacterCommandHandler : IRequestHandler<DeleteCharacterCommand, Result>
{
    private readonly ApplicationDbContext _context;

    public DeleteCharacterCommandHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result> Handle(DeleteCharacterCommand request, CancellationToken cancellationToken)
    {
        var character = await _context.Character
            .FirstOrDefaultAsync(c => c.Id == request.CharacterId, cancellationToken);

        if (character is null)
            return Result.Failure(DomainErrors.Character.NotFound);

        _context.Character.Remove(character);

        _context.ActivityLog.Add(new ActivityLog
        {
            Id          = Guid.NewGuid(),
            UserId      = request.RequestingUserId,
            Description = $"Deleted character '{character.Name}' (id: {character.Id})",
            Date        = DateTime.UtcNow,
        });

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
