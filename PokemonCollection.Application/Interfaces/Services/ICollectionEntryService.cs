using PokemonCollection.Application.DTOs.CollectionEntryDtos;

namespace PokemonCollection.Application.Interfaces.Services;

public interface ICollectionEntryService
{
    Task<IEnumerable<CollectionCardResponseDto>> GetCollectionAsync();
    Task<CollectionCardResponseDto> GetCollectionCardByIdAsync(int id);
    Task SelectCardAsync(int pokemonId, CollectionCardRequestDto dto);
    Task UpdateCardAsync(int collectionId, CollectionCardUpdateRequestDto dto);
    Task DeleteCardAsync(int pokemonId);
}