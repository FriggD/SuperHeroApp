namespace SuperheroApp.Core.DTOs;

public class HeroDto
{
    public int Id { get; set; }
    
    /// <summary>
    /// O nome real do herói
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// O codinome do super-herói
    /// </summary>
    public string HeroName { get; set; } = string.Empty;
    
    /// <summary>
    /// Descrição do histórico e habilidades do herói
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// A data de nascimento do herói
    /// </summary>
    public DateTime DateOfBirth { get; set; }
    
    /// <summary>
    /// A altura do herói em centímetros
    /// </summary>
    public decimal Height { get; set; }
    
    /// <summary>
    /// O peso do herói em quilogramas
    /// </summary>
    public decimal Weight { get; set; }
    
    /// <summary>
    /// Lista de superpoderes possuídos pelo herói
    /// </summary>
    public List<SuperpowerDto> Superpowers { get; set; } = new List<SuperpowerDto>();
}

public class CreateHeroDto
{
    /// <summary>
    /// O nome real do herói
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// O codinome do super-herói
    /// </summary>
    public string HeroName { get; set; } = string.Empty;
    
    /// <summary>
    /// Descrição do histórico e habilidades do herói
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// A data de nascimento do herói
    /// </summary>
    public DateTime DateOfBirth { get; set; }
    
    /// <summary>
    /// A altura do herói em centímetros
    /// </summary>
    public decimal Height { get; set; }
    
    /// <summary>
    /// O peso do herói em quilogramas
    /// </summary>
    public decimal Weight { get; set; }
    
    /// <summary>
    /// IDs de superpoderes existentes para atribuir ao herói
    /// </summary>
    public List<int> SuperpowerIds { get; set; } = new List<int>();
    
    /// <summary>
    /// Novos superpoderes para criar e atribuir ao herói
    /// </summary>
    public List<CreateSuperpowerDto> NewSuperpowers { get; set; } = new List<CreateSuperpowerDto>();
}

public class UpdateHeroDto
{
    /// <summary>
    /// O nome real do herói
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// O codinome do super-herói
    /// </summary>
    public string HeroName { get; set; } = string.Empty;
    
    /// <summary>
    /// Descrição do histórico e habilidades do herói
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// A data de nascimento do herói
    /// </summary>
    public DateTime DateOfBirth { get; set; }
    
    /// <summary>
    /// A altura do herói em centímetros
    /// </summary>
    public decimal Height { get; set; }
    
    /// <summary>
    /// O peso do herói em quilogramas
    /// </summary>
    public decimal Weight { get; set; }
    
    /// <summary>
    /// IDs de superpoderes existentes para atribuir ao herói
    /// </summary>
    public List<int> SuperpowerIds { get; set; } = new List<int>();
    
    /// <summary>
    /// Novos superpoderes para criar e atribuir ao herói
    /// </summary>
    public List<CreateSuperpowerDto> NewSuperpowers { get; set; } = new List<CreateSuperpowerDto>();
}