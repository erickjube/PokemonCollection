using PokemonCollection.Application.DTOs.CardsDtos;
using PokemonCollection.Application.Pagination;
using PokemonCollection.Domain.Common;

namespace PokemonCollection.Application.Interfaces.Services;

public interface ICardService
{
    Task<PagedList<CardResponseDto>> GetByPokedexNumberAsync(int pokedexNumber, QueryParameters parameters);
    Task<CardResponseDto> GetByIdAsync(int cardId);
}
