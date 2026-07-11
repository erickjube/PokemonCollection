using PokemonCollection.Application.Interfaces.Repositories;
using PokemonCollection.Domain.Entities;
using PokemonCollection.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace PokemonCollection.Infrastructure.Repositories;

public class CardRepository : ICardRepository
{
    private readonly AppDbContext _context;

    public CardRepository(AppDbContext context)
    {
        _context = context;
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
}
