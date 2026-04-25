namespace ShatteredRealms.Application.DTOs.Characters;

public class CharacterDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string OwnerName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;
    public int Level { get; set; }
    public int Experience { get; set; }
    public DateTime CreatedAt { get; set; }
}
