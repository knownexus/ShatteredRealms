namespace ShatteredRealms.Application.DTOs.Characters;

public class CharacterHistoryDto
{
    public DateTime Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public string PerformedByName { get; set; } = string.Empty;
}
