using PokemonCollection.Application.DTOs.CardsDtos;
using PokemonCollection.Application.Filters;
using PokemonCollection.Application.Pagination;
using PokemonCollection.Domain.Common;

namespace PokemonCollection.Application.Interfaces.Services;

public interface ICardService
{
    Task<PagedList<CardResponseDto>> GetByPokedexNumberAsync(int pokedexNumber, QueryParameters parameters, CardFilter filter);
    Task<CardResponseDto> GetByIdAsync(int cardId);
}
