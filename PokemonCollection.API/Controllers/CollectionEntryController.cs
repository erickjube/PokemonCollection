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

    [HttpGet("{collectionId}")]
    public async Task<ActionResult<CollectionCardResponseDto>> GetById(int collectionId)
    {
        var collection = await _collectionService.GetCollectionCardByIdAsync(collectionId);
        return Ok(collection);
    }

    [HttpPut("{pokemonId}")]
    public async Task<ActionResult> AddCardToCollection(int pokemonId, CollectionCardRequestDto dto)
    {
        await _collectionService.SelectCardAsync(pokemonId, dto);
        return NoContent();
    }

    [HttpPatch("{collectionId}")]
    public async Task<ActionResult> UpdateInfo(int collectionId, CollectionCardUpdateRequestDto dto)
    {
        await _collectionService.UpdateCardAsync(collectionId, dto);
        return NoContent();
    }

    [HttpDelete("{collectionId}")]
    public async Task<ActionResult> Delete(int collectionId)
    {
        await _collectionService.DeleteCardAsync(collectionId);
        return NoContent();
    }
}
