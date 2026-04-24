using MediatR;
using ShatteredRealms.Application.DTOs.Announcements;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Announcements.Commands;

public record CreateAnnouncementCommand(
    string AuthorId,
    string Title,
    string Body,
    int? LinkedEventId
) : IRequest<Result<AnnouncementDto>>;
