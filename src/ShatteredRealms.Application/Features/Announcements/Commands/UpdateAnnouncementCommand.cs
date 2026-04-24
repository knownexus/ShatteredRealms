using MediatR;
using ShatteredRealms.Application.DTOs.Announcements;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Announcements.Commands;

public record UpdateAnnouncementCommand(
    int Id,
    string Title,
    string Body,
    int? LinkedEventId
) : IRequest<Result<AnnouncementDto>>;
