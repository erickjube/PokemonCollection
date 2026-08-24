using PokemonCollection.Domain.ENUMs;

namespace PokemonCollection.Application.Filters;

public class PokemonFilter
{
    public string? Search { get; set; }
    public string? Generation { get; set; }
    public string? Type { get; set; }
    public string? Region { get; set; }
    public PokemonSort SortBy { get; set; } = PokemonSort.PokedexNumber;
    public bool Descending { get; set; }
}