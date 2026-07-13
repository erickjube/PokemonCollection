using PokemonCollection.Domain.Entities;

namespace PokemonCollection.Application.Interfaces.Repositories;

public interface ICardRepository
{
    Task AddRangeAsync(IEnumerable<Card> cards);
    Task<bool> ExistsByExternalIdAsync(string externalId);
    Task<HashSet<string>> GetAllExternalIdsAsync();
}
