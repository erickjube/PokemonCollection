using PokemonCollection.Domain.Common;
using PokemonCollection.Domain.Entities;

namespace PokemonCollection.Application.Interfaces.Repositories;

public interface ICardRepository
{
    Task<PagedList<Card>> GetByPokemonIdAsync(int pokemonId, int skip, int take);
    Task<Card?> GetById(int cardId);
    Task AddRangeAsync(IEnumerable<Card> cards);
    Task<bool> ExistsByExternalIdAsync(string externalId);
    Task<HashSet<string>> GetAllExternalIdsAsync();
}
