using MediatR;
using ShatteredRealms.Application.DTOs.Announcements;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Announcements.Queries;

public record GetAnnouncementByIdQuery(int Id) : IRequest<Result<AnnouncementDto>>;
