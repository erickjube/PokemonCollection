using PokemonCollection.Application.DTOs.CardsDtos;
using PokemonCollection.Application.Helpers;
using PokemonCollection.Application.Interfaces.Repositories;
using PokemonCollection.Application.Interfaces.Services;
using PokemonCollection.Application.Pagination;
using PokemonCollection.Domain.Common;

namespace PokemonCollection.Application.Services;

public class CardService : ICardService
{
    private readonly ICardRepository _cardRepository;

    public CardService(ICardRepository cardRepository)
    {
         _cardRepository = cardRepository;
    }

    public async Task<PagedList<CardResponseDto>> GetByPokemonIdAsync(int pokemonId ,QueryParameters parameters)
    {
        var skip = (parameters.PageNumber - 1) * parameters.PageSize;
        var result = await _cardRepository.GetByPokemonIdAsync(pokemonId, skip, parameters.PageSize);
        if (result == null) throw new ArgumentException("Erro ao buscar cartas.");
        ValidatePagination.Validate(parameters.PageNumber, parameters.PageSize, result.TotalCount);

        return new PagedList<CardResponseDto>
        {
            Data = result.Data.Select(c => new CardResponseDto
            {
                Id = c.Id,
                Name = c.Name,
                CardNumber = c.CardNumber,
                Rarity = c.Rarity.ToString(),
                ImageUrl = c.ImageUrl,
                SetName = c.SetName,
            }),
            TotalCount = result.TotalCount,
            PageNumber = parameters.PageNumber,
            PageSize = parameters.PageSize
        };
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
