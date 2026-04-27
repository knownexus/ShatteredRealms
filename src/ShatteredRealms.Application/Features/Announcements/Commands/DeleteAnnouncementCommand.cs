using MediatR;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Announcements.Commands;

public record DeleteAnnouncementCommand(int Id, string? ActorId = null) : IRequest<Result>;
