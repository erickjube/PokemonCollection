using PokemonCollection.Application.DTOs.PokemonsDtos;
using PokemonCollection.Application.Interfaces.Repositories;
using PokemonCollection.Application.Interfaces.Services;
using PokemonCollection.Domain.Entities;
using PokemonCollection.Domain.ENUMs;

namespace PokemonCollection.Application.Services;

public class PokemonImportService : IPokemonImportService
{
    private readonly IPokeApiClient _pokeApiClient;
    private readonly IPokemonRepository _pokeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PokemonImportService(IPokeApiClient pokeApiClient, IPokemonRepository pokeRepository, IUnitOfWork unitOfWork)
    {
        _pokeApiClient = pokeApiClient;
        _pokeRepository = pokeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ImportAsync()
    {
        var list = await _pokeApiClient.GetAllPokemonAsync(0, 1025);
        if (list == null) throw new Exception("Erro ao importar pokemons!");

        foreach (var pokemon in list.Results)
        {
            var details = await _pokeApiClient.GetPokemonAsync(pokemon.Name);
            if (details == null) throw new Exception($"Erro ao importar o pokemon {pokemon.Name}!");

            var species = await _pokeApiClient.GetSpeciesAsync(details.Name);
            if (species == null) throw new Exception($"Erro ao importar species do pokemon {pokemon.Name}!");

            if (await _pokeRepository.ExistsByPokedexNumberAsync(details.Id)) continue;

            var entity = CreatePokemon(details, species);

            await _pokeRepository.AddAsync(entity);
        }

        await _unitOfWork.SaveChangesAsync();
    }

    private Pokemon CreatePokemon(PokemonResponseDto details, PokemonSpeciesResponseDto species)
    {
        var image = details.Sprites.Other.OfficialArtwork.FrontDefault;
        var enumRegion = GetRegion(species.Generation.Name);
        var enumGeneration = GetGeneration(species.Generation.Name);
        var enumPrimaryType = GetType(details.Types.First(t => t.Slot == 1).Type.Name);
        var secondaryType = details.Types.FirstOrDefault(t => t.Slot == 2);
        TypesPokemon? enumSecondaryType = secondaryType == null ? null : GetType(secondaryType.Type.Name);

        return new Pokemon(
            pokedexNumber: details.Id,
            name: details.Name,
            generation: enumGeneration,
            region: enumRegion,
            primaryType: enumPrimaryType,
            secondaryType: enumSecondaryType,
            imageUrl: image
         );
    }

    private Generations GetGeneration(string generation)
    {
        return generation switch
        {
            "generation-i" => Generations.Gen1,
            "generation-ii" => Generations.Gen2,
            "generation-iii" => Generations.Gen3,
            "generation-iv" => Generations.Gen4,
            "generation-v" => Generations.Gen5,
            "generation-vi" => Generations.Gen6,
            "generation-vii" => Generations.Gen7,
            "generation-viii" => Generations.Gen8,
            "generation-ix" => Generations.Gen9,
            _ => throw new ArgumentException($"Generation '{generation}' inválida.")
        };
    }

    private TypesPokemon GetType(string type)
    {
        return type switch
        {
            "normal" => TypesPokemon.Normal,
            "fire" => TypesPokemon.Fire,
            "water" => TypesPokemon.Water,
            "grass" => TypesPokemon.Grass,
            "flying" => TypesPokemon.Flying,
            "fighting" => TypesPokemon.Fighting,
            "poison" => TypesPokemon.Poison,
            "electric" => TypesPokemon.Electric,
            "ground" => TypesPokemon.Ground,
            "rock" => TypesPokemon.Rock,
            "psychic" => TypesPokemon.Psychic,
            "ice" => TypesPokemon.Ice,
            "bug" => TypesPokemon.Bug,
            "ghost" => TypesPokemon.Ghost,
            "steel" => TypesPokemon.Steel,
            "dragon" => TypesPokemon.Dragon,
            "dark" => TypesPokemon.Dark,
            "fairy" => TypesPokemon.Fairy,
            _ => throw new ArgumentException($"Type '{type}' inválido.")
        };
    }

    private Regions GetRegion(string region)
    {
        return region switch
        {
            "generation-i" => Regions.Kanto,
            "generation-ii" => Regions.Johto,
            "generation-iii" => Regions.Hoenn,
            "generation-iv" => Regions.Sinnoh,
            "generation-v" => Regions.Unova,
            "generation-vi" => Regions.Kalos,
            "generation-vii" => Regions.Alola,
            "generation-viii" => Regions.Galar,
            "generation-ix" => Regions.Paldea,
            _ => throw new ArgumentException($"Type '{region}' inválida.")
        };
    }
}
