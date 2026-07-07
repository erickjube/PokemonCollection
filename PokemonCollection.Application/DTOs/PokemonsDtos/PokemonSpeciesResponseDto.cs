namespace PokemonCollection.Application.DTOs.PokemonsDtos;

public class PokemonSpeciesResponseDto
{
    public int Id { get; set; }

    public NamedApiResourceDto Generation { get; set; } = null!;
}
