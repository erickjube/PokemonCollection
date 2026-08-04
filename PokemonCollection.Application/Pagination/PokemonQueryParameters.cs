namespace PokemonCollection.Application.Pagination;

public class PokemonQueryParameters : QueryParameters
{
    public string? Search { get; set; }
    public string? Type { get; set; }
    public string? Region { get; set; }
    public int? Generation { get; set; }
}