namespace PokemonCollection.Application.DTOs.CollectionEntryDtos;

public class CollectionCardResponseDto
{
    public int Id { get; set; }
    public int CardId { get; set; }
    public string CardName { get; set; }
    public string PokemonName { get; set; }
    public string ImageUrl { get; set; }
    public string SetName { get; set; }
    public string Condition { get; set; }
    public string Language { get; set; }
    public string Extra { get; set; }
    public DateTime DateAdded { get; set; } = DateTime.UtcNow;
}
