using PokemonCollection.Domain.Entities;

namespace PokemonCollection.Application.Interfaces.Repositories;

public interface IPokemonRepository
{
    Task<IEnumerable<Pokemon>> GetAllAsync();
    Task AddAsync(Pokemon pokemon);
    Task<bool> ExistsByPokedexNumberAsync(int pokedexNumber);
    Task<Pokemon?> GetByPokedexNumberAsync(int pokedexNumber);
}
