namespace PokemonCollection.Application.DTOs.CollectionEntryDtos;

public class CollectionCardResponseDto
{
    public int Id { get; set; }
    public int CardId { get; set; }
    public string CardName { get; set; } = string.Empty;
    public string PokemonName { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string SetName { get; set; } = string.Empty;
    public string Condition { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public string Extra { get; set; } = string.Empty;
    public DateTime DateAdded { get; set; } = DateTime.UtcNow;
    public string CardRarity { get; set; } = string.Empty;
    public string CardNumber { get; set; } = string.Empty;
    public int SetPrintedTotal { get; set; }
}
