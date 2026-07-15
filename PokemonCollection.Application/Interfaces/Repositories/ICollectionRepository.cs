using PokemonCollection.Domain.Entities;

namespace PokemonCollection.Application.Interfaces.Repositories;

public interface ICollectionRepository
{
    Task<IEnumerable<CollectionEntry>> GetAllAsync();
    Task<CollectionEntry?> GetByIdAsync(int collectionEntryId);
    Task<CollectionEntry?> GetByPokemonIdAsync(int pokemonId);
    Task AddAsync(CollectionEntry? entry);
    Task DeleteAsync(CollectionEntry entry);
}
