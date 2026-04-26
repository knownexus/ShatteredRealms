namespace ShatteredRealms.Application.DTOs.Characters;

public class UpdateCharacterByAdminRequest
{
    public string Name { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;
    public string Faction { get; set; } = string.Empty;
    public int Level { get; set; }
    public int Experience { get; set; }
}
