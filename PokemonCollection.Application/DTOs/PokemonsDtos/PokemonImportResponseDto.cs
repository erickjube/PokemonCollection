namespace PokemonCollection.Application.DTOs.PokemonsDtos;

public class PokemonImportResponseDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public List<PokemonTypeDto> Types { get; set; } = [];

    public SpritesDto Sprites { get; set; } = null!;
    public NamedApiResourceDto Species { get; set; } = null!;

}