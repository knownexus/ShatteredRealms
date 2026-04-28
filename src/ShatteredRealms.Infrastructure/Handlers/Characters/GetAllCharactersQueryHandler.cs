using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Characters;
using ShatteredRealms.Application.Features.Characters.Queries;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Characters;

public sealed class GetAllCharactersQueryHandler : IRequestHandler<GetAllCharactersQuery, Result<List<CharacterDto>>>
{
    private readonly ApplicationDbContext _context;

    public GetAllCharactersQueryHandler(ApplicationDbContext context) => _context = context;

    public async Task<Result<List<CharacterDto>>> Handle(GetAllCharactersQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Character
            .Include(c => c.Owner)
            .Include(c => c.Position)
            .AsQueryable();

        // Filter by specific user ID
        if (!string.IsNullOrEmpty(request.UserId))
        {
            query = query.Where(c => c.UserId == request.UserId);
        }

        // Filter by user name search
        if (!string.IsNullOrEmpty(request.UserName))
        {
            query = query.Where(c => c.Owner != null && c.Owner.UserName != null && c.Owner.UserName.Contains(request.UserName));
        }

        // Filter by role
        if (!string.IsNullOrEmpty(request.Role))
        {
            query = query.Where(c => c.Owner != null && c.Owner.UserRoles.Any(ur => ur.Role.Name == request.Role));
        }

        // Filter by nation (nationality)
        if (!string.IsNullOrEmpty(request.Nation))
        {
            query = query.Where(c => c.Nationality.Contains(request.Nation));
        }

        // Search by character name
        if (!string.IsNullOrEmpty(request.Search))
        {
            query = query.Where(c => c.Name.Contains(request.Search));
        }

        var characters = await query
            .OrderBy(c => c.Owner != null ? c.Owner.UserName : string.Empty)
            .ThenBy(c => c.CreatedAt)
            .ToListAsync(cancellationToken);

        return Result.Success(characters.Select(c => CreateCharacterCommandHandler.MapToDto(c, null)).ToList());
    }
}
