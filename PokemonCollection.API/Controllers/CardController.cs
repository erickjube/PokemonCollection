using Microsoft.AspNetCore.Mvc;
using PokemonCollection.API.Header;
using PokemonCollection.Application.DTOs.CardsDtos;
using PokemonCollection.Application.Filters;
using PokemonCollection.Application.Interfaces.Services;
using PokemonCollection.Application.Pagination;

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

    [HttpGet("{pokedexNumber}/cards")]
    public async Task<ActionResult<IEnumerable<CardResponseDto>>> GetByPokemonId(int pokedexNumber, [FromQuery] QueryParameters parameters, [FromQuery] CardFilter filter)
    {
        var metadata = await _cardService.GetByPokedexNumberAsync(pokedexNumber, parameters, filter);
        Response.AppendPaginationHeader(metadata);
        return Ok(metadata.Data);
    }

    [HttpGet("{cardId}")]
    public async Task<ActionResult<CardResponseDto>> GetById(int cardId)
    {
        var card = await _cardService.GetByIdAsync(cardId);
        return Ok(card);
    }
}
