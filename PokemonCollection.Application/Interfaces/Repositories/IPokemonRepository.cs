using PokemonCollection.Domain.Entities;

namespace PokemonCollection.Application.Interfaces.Repositories;

public interface IPokemonRepository
{
    Task<IEnumerable<Pokemon>> GetAllAsync();
    Task<Pokemon?> GetByIdAsync(int pokemonId);
    Task AddAsync(Pokemon pokemon);
    Task<bool> ExistsByPokedexNumberAsync(int pokedexNumber);
    Task<Pokemon?> GetByPokedexNumberAsync(int pokedexNumber);
}
