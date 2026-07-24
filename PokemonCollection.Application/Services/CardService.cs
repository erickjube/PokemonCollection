using PokemonCollection.Application.DTOs.CardsDtos;
using PokemonCollection.Application.Interfaces.Repositories;
using PokemonCollection.Application.Interfaces.Services;

namespace PokemonCollection.Application.Services;

public class CardService : ICardService
{
    private readonly ICardRepository _cardRepository;

    public CardService(ICardRepository cardRepository)
    {
         _cardRepository = cardRepository;
    }

    public async Task<IEnumerable<CardResponseDto>> GetByPokemonIdAsync(int pokemonId)
    {
        var cards = await _cardRepository.GetByPokemonIdAsync(pokemonId);

        return cards.Select(c => new CardResponseDto
        {
            Id = c.Id,
            Name = c.Name,
            CardNumber = c.CardNumber,
            Rarity = c.Rarity.ToString(),
            ImageUrl = c.ImageUrl,
            SetName = c.SetName,
        });
    }

    public async Task<CardResponseDto> GetByIdAsync(int cardId)
    {
        var card = await _cardRepository.GetById(cardId);

        if (card == null) throw new ArgumentException("Carta não encontrada!");

        return new CardResponseDto
        {
            Id = card.Id,
            Name = card.Name,
            CardNumber = card.CardNumber,
            Rarity = card.Rarity.ToString(),
            ImageUrl = card.ImageUrl,
            SetName = card.SetName,
        };
    }
}
