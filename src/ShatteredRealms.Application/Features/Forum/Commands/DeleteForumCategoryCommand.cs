using MediatR;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Forum.Commands;

/// <summary>Deletes a forum category and its threads. Admin only.</summary>
public sealed record DeleteForumCategoryCommand(int CategoryId) : IRequest<Result>;
