using PokemonCollection.Application.DTOs.CollectionEntryDtos;

namespace PokemonCollection.Application.Interfaces.Services;

public interface ICollectionEntryService
{
    Task<IEnumerable<CollectionCardResponseDto>> GetCollectionAsync();
    Task<CollectionCardResponseDto> GetCollectionCardByIdAsync(int id);
    Task SelectCardAsync(CollectionCardRequestDto dto);
    Task UpdateCardAsync(CollectionCardRequestDto dto);
}