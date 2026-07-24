using PokemonCollection.Domain.ENUMs;

namespace PokemonCollection.Application.DTOs.CardsDtos;

public class CardResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CardNumber { get; set; } = string.Empty;
    public string Rarity { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string SetName { get; set; } = string.Empty; 
}
