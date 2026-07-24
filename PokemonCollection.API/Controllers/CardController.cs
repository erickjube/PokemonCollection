using Microsoft.AspNetCore.Mvc;
using PokemonCollection.Application.DTOs.CardsDtos;
using PokemonCollection.Application.Interfaces.Services;

namespace PokemonCollection.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CardController : ControllerBase
{
    private readonly ICardService _cardService;

    public CardController(ICardService cardService)
    {
        _cardService = cardService;
    }

    [HttpGet("{pokemonId}/cards")]
    public async Task<ActionResult<IEnumerable<CardResponseDto>>> GetByPokemonId(int pokemonId)
    {
        var cards = await _cardService.GetByPokemonIdAsync(pokemonId);
        return Ok(cards);
    }

    [HttpGet("{cardId}")]
    public async Task<ActionResult<CardResponseDto>> GetById(int cardId)
    {
        var card = await _cardService.GetByIdAsync(cardId);
        return Ok(card);
    }
}
