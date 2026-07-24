using Microsoft.AspNetCore.Mvc;
using PokemonCollection.Application.DTOs.PokemonsDtos;
using PokemonCollection.Application.Interfaces.Services;

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
    public async Task<ActionResult<IEnumerable<PokemonResponseDto>>> GetAll()
    {
        var pokemons = await _pokemonService.GetAllAsync();
        return Ok(pokemons);
    }

    [HttpGet("{pokemonId}")]
    public async Task<ActionResult<PokemonResponseDto>> GetById(int pokemonId)
    {
        var pokemon = await _pokemonService.GetByIdAsync(pokemonId);
        return Ok(pokemon);
    }
}
