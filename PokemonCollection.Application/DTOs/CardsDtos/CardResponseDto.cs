namespace PokemonCollection.Application.DTOs.CardsDtos;

public class CardResponseDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string? Rarity { get; set; }
    public CardImageDto Images { get; set; } = null!;
    public CardSetDto Set { get; set; } = null!;
}
