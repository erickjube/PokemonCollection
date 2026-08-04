using Microsoft.EntityFrameworkCore;
using PokemonCollection.Application.Filters;
using PokemonCollection.Application.Interfaces.Repositories;
using PokemonCollection.Domain.Common;
using PokemonCollection.Domain.Entities;
using PokemonCollection.Domain.ENUMs;
using PokemonCollection.Infrastructure.Data;

namespace PokemonCollection.Infrastructure.Repositories;

public class PokemonRepository : IPokemonRepository
{
    private readonly AppDbContext _context;

    public PokemonRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedList<Pokemon>> GetAllAsync(int skip, int take, PokemonFilter filter)
    {
        var query = _context.Pokemons.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter?.Search))
            query = query.Where(p => p.Name.Contains(filter.Search));

        if (!string.IsNullOrWhiteSpace(filter?.Generation))
            query = query.Where(p => p.Generation.ToString().Contains(filter.Generation));

        if (!string.IsNullOrWhiteSpace(filter?.Type))
        {
            query = query.Where(p => p.PrimaryType.ToString().Contains(filter.Type) ||
                (p.SecondaryType.HasValue && p.SecondaryType.Value.ToString().Contains(filter.Type)));
        }
        
        if (!string.IsNullOrWhiteSpace(filter?.Region))
            query = query.Where(p => p.Region.ToString().Contains(filter.Region));

        switch (filter.SortBy)
        {
            case PokemonSort.Name:
                query = filter.Descending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name);
                break;

            case PokemonSort.PokedexNumber:
            default:
                query = filter.Descending ? query.OrderByDescending(p => p.PokedexNumber) : query.OrderBy(p => p.PokedexNumber);
                break;
        }

        var totalCount = await query.CountAsync();
        var data = await query.Skip(skip).Take(take).ToListAsync();

        return new PagedList<Pokemon> { Data = data, TotalCount = totalCount };
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
