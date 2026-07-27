using PokemonCollection.Application.Interfaces.Repositories;
using PokemonCollection.Domain.Entities;
using PokemonCollection.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using PokemonCollection.Domain.Common;

namespace PokemonCollection.Infrastructure.Repositories;

public class CardRepository : ICardRepository
{
    private readonly AppDbContext _context;

    public CardRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedList<Card>> GetByPokemonIdAsync(int pokemonId, int skip, int take)
    {
        var totalCont = await _context.Cards.Where(c => c.PokemonId == pokemonId).CountAsync();
        var data = await _context.Cards.Where(c => c.PokemonId == pokemonId).Skip(skip).Take(take).ToListAsync();
        return new PagedList<Card> { Data = data, TotalCount = totalCont };
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
