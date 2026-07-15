namespace PokemonCollection.Application.DTOs.CollectionEntryDtos;

public class CollectionCardRequestDto
{
   public int CardId { get; set; }
   public string? Condition { get; set; }
   public string? Language { get; set; }
   public string? Extra { get; set; }
}
