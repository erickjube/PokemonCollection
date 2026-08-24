using Microsoft.EntityFrameworkCore;
using PokemonCollection.Application.Interfaces.Repositories;
using PokemonCollection.Domain.Common;
using PokemonCollection.Domain.Entities;
using PokemonCollection.Infrastructure.Data;

namespace PokemonCollection.Infrastructure.Repositories;

public class CollectionRepository : ICollectionRepository
{
    private readonly AppDbContext _context;

    public CollectionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedList<CollectionEntry>> GetAllAsync(int skip, int take)
    {
        var totalCont = await _context.CollectionEntries.CountAsync();
        var data = await _context.CollectionEntries
            .Include(c => c.Card)
                .ThenInclude(c => c.Pokemon)
             .Skip(skip).Take(take)
            .ToListAsync();

        return new PagedList<CollectionEntry> { Data = data, TotalCount = totalCont };
    }

    public async Task<CollectionEntry?> GetByIdAsync(int collectionEntryId)
    {
        return await _context.CollectionEntries
        .Include(c => c.Card)
            .ThenInclude(card => card.Pokemon)
        .FirstOrDefaultAsync(ce => ce.Id == collectionEntryId);
    }

    public async Task<CollectionEntry?> GetByPokemonIdAsync(int pokemonId)
    {
        return await _context.CollectionEntries
        .Include(c => c.Card)
        .ThenInclude(c => c.Pokemon)
        .FirstOrDefaultAsync(c => c.PokemonId == pokemonId);
    }

    public async Task AddAsync(CollectionEntry? entry)
    {
        await _context.CollectionEntries.AddAsync(entry);
    }

    public async Task DeleteAsync(CollectionEntry entry)
    {
        _context.CollectionEntries.Remove(entry);
    }
}
