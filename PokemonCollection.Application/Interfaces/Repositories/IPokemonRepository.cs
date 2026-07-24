using PokemonCollection.Domain.Common;
using PokemonCollection.Domain.Entities;

namespace PokemonCollection.Application.Interfaces.Repositories;

public interface IPokemonRepository
{
    Task<PagedList<Pokemon>> GetAllAsync(int skip, int take);
    Task<IEnumerable<Pokemon>> GetAllForImportAsync();
    Task<Pokemon?> GetByIdAsync(int pokemonId);
    Task AddAsync(Pokemon pokemon);
    Task<bool> ExistsByPokedexNumberAsync(int pokedexNumber);
    Task<Pokemon?> GetByPokedexNumberAsync(int pokedexNumber);
}
