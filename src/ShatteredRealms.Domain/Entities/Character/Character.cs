namespace ShatteredRealms.Domain.Entities.Character;

public class Character
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;
    public int Level { get; set; } = 1;
    public int Experience { get; set; } = 0;
    public DateTime CreatedAt { get; set; }

    public User.User? Owner { get; set; }
}
