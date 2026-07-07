namespace PokemonCollection.Application.DTOs.PokemonsDtos;

public class PokemonTypeDto
{
    public int Slot { get; set; }

    public NamedApiResourceDto Type { get; set; } = null!;
}