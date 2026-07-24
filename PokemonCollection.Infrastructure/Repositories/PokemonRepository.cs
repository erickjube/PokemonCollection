using Microsoft.EntityFrameworkCore;
using PokemonCollection.Application.Interfaces.Repositories;
using PokemonCollection.Domain.Common;
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

    public async Task<PagedList<Pokemon>> GetAllAsync(int skip, int take)
    {
        var totalCont = await _context.Pokemons.CountAsync();
        var data = await _context.Pokemons.Skip(skip).Take(take).ToListAsync();
        return new PagedList<Pokemon> { Data = data, TotalCount = totalCont };
    }

    public async Task<IEnumerable<Pokemon>> GetAllForImportAsync()
    {
        return await _context.Pokemons.ToListAsync();
    }

    public async Task<Pokemon?> GetByIdAsync(int pokemonId)
    {
        return await _context.Pokemons.FirstOrDefaultAsync(p => p.Id == pokemonId);
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
