using MediatR;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Auth.Commands;

public sealed record ConfirmEmailCommand(string UserId, string Token) : IRequest<Result>;
