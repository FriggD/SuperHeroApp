namespace SuperheroApp.Core.Entities;

public class Hero
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string HeroName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    
    public ICollection<HeroSuperpower> HeroSuperpowers { get; set; } = new List<HeroSuperpower>();
}