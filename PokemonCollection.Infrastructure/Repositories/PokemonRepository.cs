using Microsoft.EntityFrameworkCore;
using PokemonCollection.Application.Interfaces.Repositories;
using PokemonCollection.Domain.Entities;
using PokemonCollection.Infrastructure.Data;

namespace PokemonCollection.Infrastructure.Repositories;

public class PokemonRepository : IPokemonRepository
{
    private readonly AppDbContext _context;

    public PokemonRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Pokemon>> GetAllAsync()
    {
        return await _context.Pokemons.ToListAsync();
    }

    public async Task AddAsync(Pokemon pokemon)
    {
        _context.Pokemons.Add(pokemon);
    }

    public async Task<bool> ExistsByPokedexNumberAsync(int pokedexNumber)
    {
        var pokemon = await _context.Pokemons.FindAsync(pokedexNumber);
        if (pokemon == null) return false;
        return true;
    }

    public async Task<Pokemon?> GetByPokedexNumberAsync(int pokedexNumber)
    {
        return await _context.Pokemons.FirstOrDefaultAsync(p => p.PokedexNumber == pokedexNumber);
    }
}
