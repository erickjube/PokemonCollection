using PokemonCollection.Domain.Entities;

namespace PokemonCollection.Application.Interfaces.Repositories;

public interface IPokemonRepository
{
    Task AddAsync(Pokemon pokemon);
    Task<bool> ExistsByPokedexNumberAsync(int pokedexNumber);
}
