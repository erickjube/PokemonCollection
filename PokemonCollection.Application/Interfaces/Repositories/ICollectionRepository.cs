using PokemonCollection.Domain.Common;
using PokemonCollection.Domain.Entities;

namespace PokemonCollection.Application.Interfaces.Repositories;

public interface ICollectionRepository
{
    Task<PagedList<CollectionEntry>> GetAllAsync(int skip, int take);
    Task<CollectionEntry?> GetByIdAsync(int collectionEntryId);
    Task<CollectionEntry?> GetByPokemonIdAsync(int pokemonId);
    Task AddAsync(CollectionEntry? entry);
    Task DeleteAsync(CollectionEntry entry);
}
