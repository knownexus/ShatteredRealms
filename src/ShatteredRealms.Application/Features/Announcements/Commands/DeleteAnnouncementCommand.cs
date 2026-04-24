using MediatR;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Announcements.Commands;

public record DeleteAnnouncementCommand(int Id) : IRequest<Result>;
