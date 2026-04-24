using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.Features.Events.Commands;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Events;

public sealed class CancelRegistrationCommandHandler : IRequestHandler<CancelRegistrationCommand, Result>
{
    private readonly ApplicationDbContext _context;

    public CancelRegistrationCommandHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result> Handle(CancelRegistrationCommand request, CancellationToken cancellationToken)
    {
        var attendee = await _context.EventAttendee
            .FirstOrDefaultAsync(a => a.EventId == request.EventId && a.UserId == request.UserId, cancellationToken);

        if (attendee is null)
            return Result.Failure(DomainErrors.Event.NotRegistered);

        _context.EventAttendee.Remove(attendee);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
