using Microsoft.AspNetCore.Mvc;
using PokemonCollection.API.Header;
using PokemonCollection.Application.DTOs.CollectionEntryDtos;
using PokemonCollection.Application.Interfaces.Services;
using PokemonCollection.Application.Pagination;

namespace PokemonCollection.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CollectionEntryController : ControllerBase
{
    private readonly ICollectionEntryService _collectionService;

    public CollectionEntryController(ICollectionEntryService collectionService)
    {
        _collectionService = collectionService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CollectionCardResponseDto>>> GetAll([FromQuery] QueryParameters parameters)
    {
        var metadata =  await _collectionService.GetCollectionAsync(parameters);
        Response.AppendPaginationHeader(metadata);
        return Ok(metadata.Data);
    }

    [HttpGet("{pokedexNumber}")]
    public async Task<ActionResult<CollectionCardResponseDto>> GetById(int pokedexNumber)
    {
        var collection = await _collectionService.GetCollectionCardByPokedexNumberAsync(pokedexNumber);
        return Ok(collection);
    }

    [HttpPut("{pokedexNumber}")]
    public async Task<ActionResult> AddCardToCollection(int pokedexNumber, CollectionCardRequestDto dto)
    {
        await _collectionService.SelectCardAsync(pokedexNumber, dto);
        return NoContent();
    }

    [HttpPatch("{collectionId}")]
    public async Task<ActionResult> UpdateInfo(int collectionId, CollectionCardUpdateRequestDto dto)
    {
        await _collectionService.UpdateCardAsync(collectionId, dto);
        return NoContent();
    }

    [HttpDelete("{pokedexNumber}")]
    public async Task<ActionResult> Delete(int pokedexNumber)
    {
        await _collectionService.DeleteCardAsync(pokedexNumber);
        return NoContent();
    }
}
