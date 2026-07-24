using PokemonCollection.Application.DTOs.PokemonsDtos;
using PokemonCollection.Application.Interfaces.Repositories;
using PokemonCollection.Application.Interfaces.Services;

namespace PokemonCollection.Application.Services;

public class PokemonService : IPokemonService
{
    private readonly IPokemonRepository _pokemonRepository;

    public PokemonService(IPokemonRepository pokemonRepository)
    {
        _pokemonRepository = pokemonRepository;
    }

    public async Task<IEnumerable<PokemonResponseDto>> GetAllAsync()
    {
        var pokemons = await _pokemonRepository.GetAllAsync();

        return pokemons.Select(p => new PokemonResponseDto
        {
            Id = p.Id,
            Name = p.Name,
            Generation = p.Generation.ToString(),
            Region = p.Region.ToString(),
            PrimaryType = p.PrimaryType.ToString(),
            SecondaryType = p.SecondaryType.ToString(),
            ImageUrl = p.ImageUrl,
        });
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
