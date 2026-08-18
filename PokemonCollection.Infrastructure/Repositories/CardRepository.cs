using PokemonCollection.Application.Interfaces.Repositories;
using PokemonCollection.Domain.Entities;
using PokemonCollection.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using PokemonCollection.Domain.Common;
using PokemonCollection.Application.Filters;

namespace PokemonCollection.Infrastructure.Repositories;

public class CardRepository : ICardRepository
{
    private readonly AppDbContext _context;

    public CardRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedList<Card>> GetByPokemonIdAsync(int pokemonId, int skip, int take, CardFilter filter)
    {
        var query = _context.Cards.Where(c => c.PokemonId == pokemonId).AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter?.Search))
            query = query.Where(c => c.Name.Contains(filter.Search));

        if (!string.IsNullOrWhiteSpace(filter?.CardNumber))
            query = query.Where(c => c.CardNumber.Contains(filter.CardNumber));

        if (filter?.SetPrintedTotal.HasValue == true)
            query = query.Where(c => c.SetPrintedTotal == filter.SetPrintedTotal.Value);

        if (!string.IsNullOrWhiteSpace(filter?.SetName))
            query = query.Where(c => c.SetName.Contains(filter.SetName));

        var totalCount = await query.CountAsync();
        var data = await query.Skip(skip).Take(take).ToListAsync();
        return new PagedList<Card> { Data = data, TotalCount = totalCount };
    }

    public async Task<Card?> GetById(int cardId)
    {
        return await _context.Cards.FindAsync(cardId);
    }

    public async Task AddRangeAsync(IEnumerable<Card> cards)
    {
        await _context.Cards.AddRangeAsync(cards);
    }

    public async Task<bool> ExistsByExternalIdAsync(string externalId)
    {
        var card = await _context.Cards.FirstOrDefaultAsync(c => c.ExternalId == externalId);
        return card != null;
    }

    public async Task<HashSet<string>> GetAllExternalIdsAsync()
    {
        return await _context.Cards.Select(c => c.ExternalId).ToHashSetAsync();
    }
}
