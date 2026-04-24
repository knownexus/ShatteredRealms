using MediatR;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Auth.Commands;

public sealed record ResendConfirmationEmailCommand(string Email) : IRequest<Result>;
