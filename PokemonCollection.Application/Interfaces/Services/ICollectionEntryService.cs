using PokemonCollection.Application.DTOs.CollectionEntryDtos;
using PokemonCollection.Application.Pagination;
using PokemonCollection.Domain.Common;

namespace PokemonCollection.Application.Interfaces.Services;

public interface ICollectionEntryService
{
    Task<PagedList<CollectionCardResponseDto>> GetCollectionAsync(QueryParameters parameters);
    Task<CollectionCardResponseDto> GetCollectionCardByPokedexNumberAsync(int pokedexNumber);
    Task SelectCardAsync(int pokedexNumber, CollectionCardRequestDto dto);
    Task UpdateCardAsync(int collectionId, CollectionCardUpdateRequestDto dto);
    Task DeleteCardAsync(int pokemonId);
}