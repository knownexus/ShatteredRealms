using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.Features.Events.Commands;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Events;

public sealed class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, Result>
{
    private readonly ApplicationDbContext _context;

    public DeleteEventCommandHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        var ev = await _context.Event.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (ev is null)
            return Result.Failure(DomainErrors.Event.NotFound);

        ev.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
