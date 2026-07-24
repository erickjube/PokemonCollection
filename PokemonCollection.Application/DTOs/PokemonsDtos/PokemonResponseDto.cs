namespace PokemonCollection.Application.DTOs.PokemonsDtos;

public class PokemonResponseDto
{
    public int Id { get; set; }
    public int PokedexNumber { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Generation { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string PrimaryType { get; set; } = string.Empty;
    public string? SecondaryType { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}
