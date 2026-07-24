using Microsoft.AspNetCore.Mvc;
using PokemonCollection.API.Header;
using PokemonCollection.Application.DTOs.PokemonsDtos;
using PokemonCollection.Application.Interfaces.Services;
using PokemonCollection.Application.Pagination;

namespace PokemonCollection.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PokemonController : ControllerBase
{
    private readonly IPokemonService _pokemonService;

    public PokemonController(IPokemonService pokemonService)
    {
        _pokemonService = pokemonService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PokemonResponseDto>>> GetAll([FromQuery] QueryParameters parameters)
    {
        var metadata = await _pokemonService.GetAllAsync(parameters);
        Response.AppendPaginationHeader(metadata);
        return Ok(metadata.Data);
    }

    [HttpGet("{pokemonId}")]
    public async Task<ActionResult<PokemonResponseDto>> GetById(int pokemonId)
    {
        var pokemon = await _pokemonService.GetByIdAsync(pokemonId);
        return Ok(pokemon);
    }
}
