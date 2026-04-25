using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.Features.Characters.Commands;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Characters;

public sealed class DeleteOwnCharacterCommandHandler : IRequestHandler<DeleteOwnCharacterCommand, Result>
{
    private readonly ApplicationDbContext _context;

    public DeleteOwnCharacterCommandHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result> Handle(DeleteOwnCharacterCommand request, CancellationToken cancellationToken)
    {
        var character = await _context.Character
            .FirstOrDefaultAsync(c => c.Id == request.CharacterId, cancellationToken);

        if (character is null)
            return Result.Failure(DomainErrors.Character.NotFound);

        if (character.UserId != request.RequestingUserId)
            return Result.Failure(DomainErrors.Character.NotOwner);

        _context.Character.Remove(character);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
