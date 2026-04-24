using MediatR;
using ShatteredRealms.Application.Features.Auth.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Handlers.Auth;

public sealed class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Result>
{
    private readonly IUserService _userService;

    public ConfirmEmailCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Result> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        => await _userService.ConfirmEmailAsync(request.UserId, request.Token, cancellationToken);
}
