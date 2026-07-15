using Microsoft.AspNetCore.Mvc;
using PokemonCollection.Application.DTOs.CollectionEntryDtos;
using PokemonCollection.Application.Interfaces.Services;

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
    public async Task<ActionResult<IEnumerable<CollectionCardResponseDto>>> GetAll()
    {
        var collections =  await _collectionService.GetCollectionAsync();
        return Ok(collections);
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
