using MediatR;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Events.Commands;

public sealed record SetEventBannerCommand(int EventId, string RelativePath) : IRequest<Result>;
