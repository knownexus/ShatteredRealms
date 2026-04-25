using MediatR;
using ShatteredRealms.Application.DTOs.Characters;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Features.Characters.Queries;

public sealed record GetMyCharactersQuery(string UserId) : IRequest<Result<List<CharacterDto>>>;
