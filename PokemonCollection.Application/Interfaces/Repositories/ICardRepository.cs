using PokemonCollection.Domain.Entities;

namespace PokemonCollection.Application.Interfaces.Repositories;

public interface ICardRepository
{
    Task<IEnumerable<Card>> GetByPokemonIdAsync(int pokemonId);
    Task<Card?> GetById(int cardId);
    Task AddRangeAsync(IEnumerable<Card> cards);
    Task<bool> ExistsByExternalIdAsync(string externalId);
    Task<HashSet<string>> GetAllExternalIdsAsync();
}
