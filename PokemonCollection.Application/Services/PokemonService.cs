using PokemonCollection.Application.DTOs.PokemonsDtos;
using PokemonCollection.Application.Helpers;
using PokemonCollection.Application.Interfaces.Repositories;
using PokemonCollection.Application.Interfaces.Services;
using PokemonCollection.Application.Pagination;
using PokemonCollection.Domain.Common;

namespace PokemonCollection.Application.Services;

public class PokemonService : IPokemonService
{
    private readonly IPokemonRepository _pokemonRepository;

    public PokemonService(IPokemonRepository pokemonRepository)
    {
        _pokemonRepository = pokemonRepository;
    }

    public async Task<PagedList<PokemonResponseDto>> GetAllAsync(QueryParameters parameters)  //paginar
    {
        var skip = (parameters.PageNumber - 1) * parameters.PageSize;
        var result = await _pokemonRepository.GetAllAsync(skip, parameters.PageSize);
        if (result == null) throw new ArgumentException("Erro ao buscar Categorias.");
        ValidatePagination.Validate(parameters.PageNumber, parameters.PageSize, result.TotalCount);


        var dtos = result.Data.Select(p => new PokemonResponseDto
        {
            Id = p.Id,
            Name = p.Name,
            Generation = p.Generation.ToString(),
            Region = p.Region.ToString(),
            PrimaryType = p.PrimaryType.ToString(),
            SecondaryType = p.SecondaryType.ToString(),
            ImageUrl = p.ImageUrl,
        });

        return new PagedList<PokemonResponseDto>
        {
            Data = result.Data.Select(p => new PokemonResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Generation = p.Generation.ToString(),
                Region = p.Region.ToString(),
                PrimaryType = p.PrimaryType.ToString(),
                SecondaryType = p.SecondaryType.ToString(),
                ImageUrl = p.ImageUrl,
            }),
            TotalCount = result.TotalCount,
            PageNumber = parameters.PageNumber,
            PageSize = parameters.PageSize
        };
    }

    public async Task<PokemonResponseDto> GetByIdAsync(int pokemonId)
    {
        var pokemon = await _pokemonRepository.GetByIdAsync(pokemonId);
        if (pokemon == null) throw new ArgumentException("Pokemon não encotrado!");

        return new PokemonResponseDto
        {
            Id = pokemon.Id,
            Name = pokemon.Name,
            Generation = pokemon.Generation.ToString(),
            Region = pokemon.Region.ToString(),
            PrimaryType = pokemon.PrimaryType.ToString(),
            SecondaryType = pokemon.SecondaryType.ToString(),
            ImageUrl = pokemon.ImageUrl,
        };
    }
}
