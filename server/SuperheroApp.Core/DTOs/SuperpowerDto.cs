namespace SuperheroApp.Core.DTOs;

public class SuperpowerDto
{
    /// <summary>
    /// O identificador único para o superpoder
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// O nome do superpoder
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Descrição do que o superpoder faz
    /// </summary>
    public string Description { get; set; } = string.Empty;
}

public class CreateSuperpowerDto
{
    /// <summary>
    /// O nome do superpoder
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Descrição do que o superpoder faz
    /// </summary>
    public string Description { get; set; } = string.Empty;
}

public class UpdateSuperpowerDto
{
    /// <summary>
    /// O nome do superpoder
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Descrição do que o superpoder faz
    /// </summary>
    public string Description { get; set; } = string.Empty;
}