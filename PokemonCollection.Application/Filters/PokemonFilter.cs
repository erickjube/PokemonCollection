namespace PokemonCollection.Application.Filters;

public class PokemonFilter
{
    public string? Search { get; set; }
    public string? Generation { get; set; }
    public string? Type { get; set; }
    public string? Region { get; set; }
    public int? PokedexNumber { get; set; }
}