using PokemonCollection.Application.DTOs.CollectionEntryDtos;
using PokemonCollection.Application.Helpers;
using PokemonCollection.Application.Interfaces.Repositories;
using PokemonCollection.Application.Interfaces.Services;
using PokemonCollection.Application.Pagination;
using PokemonCollection.Domain.Common;
using PokemonCollection.Domain.Entities;
using PokemonCollection.Domain.ENUMs;

namespace PokemonCollection.Application.Services;

public class CollectionEntryService : ICollectionEntryService
{
    private readonly ICollectionRepository _collectionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICardRepository _cardRepository;
    private readonly IPokemonRepository _pokemonRepository;

    public CollectionEntryService(ICollectionRepository collectionRepository, 
                                  IUnitOfWork unitOfWork, 
                                  ICardRepository cardRepository,
                                  IPokemonRepository pokemonRepository)
    {
        _collectionRepository = collectionRepository;
        _unitOfWork = unitOfWork;
        _cardRepository = cardRepository;
        _pokemonRepository = pokemonRepository;
    }

    public async Task<PagedList<CollectionCardResponseDto>> GetCollectionAsync(QueryParameters parameters)  
    {
        var skip = (parameters.PageNumber - 1) * parameters.PageSize;
        var result = await _collectionRepository.GetAllAsync(skip, parameters.PageSize);
        if (result == null) throw new ArgumentException("Erro ao buscar Coleção.");
        ValidatePagination.Validate(parameters.PageNumber, parameters.PageSize, result.TotalCount);

        return new PagedList<CollectionCardResponseDto>
        {
            Data = result.Data.Select(c => new CollectionCardResponseDto
            {
                Id = c.Id,
                CardId = c.CardId,
                CardName = c.Card.Name,
                PokemonName = c.Card.Pokemon.Name,
                ImageUrl = c.Card.ImageUrl,
                SetName = c.Card.SetName,
                Condition = c.Condition.ToString(),
                Language = c.Language.ToString(),
                Extra = c.Extra.ToString(),
                DateAdded = c.DateAdded,
                CardRarity = c.Card.Rarity,
                CardNumber = c.Card.CardNumber,
                SetPrintedTotal = c.Card.SetPrintedTotal
            }),
            TotalCount = result.TotalCount,
            PageNumber = parameters.PageNumber,
            PageSize = parameters.PageSize
        };
    }

    public async Task<CollectionCardResponseDto> GetCollectionCardByPokedexNumberAsync(int pokedexNumber)
    {
        var pokemon = await _pokemonRepository.GetByPokedexNumberAsync(pokedexNumber);
        if (pokemon is null) throw new Exception("Pokemon não encontrado.");

        var collection = await _collectionRepository.GetByPokemonIdAsync(pokemon.Id);
        if (collection is null) return null;

        return new CollectionCardResponseDto
        {
            Id = collection.Id,
            CardId = collection.CardId,
            CardName = collection.Card.Name,
            PokemonName = collection.Card.Pokemon.Name,
            ImageUrl = collection.Card.ImageUrl,
            SetName = collection.Card.SetName,
            Condition = collection.Condition.ToString(),
            Language = collection.Language.ToString(),
            Extra = collection.Extra.ToString(),
            DateAdded = collection.DateAdded,
            CardRarity = collection.Card.Rarity,
            CardNumber = collection.Card.CardNumber,
            SetPrintedTotal = collection.Card.SetPrintedTotal
        };
    }

    public async Task SelectCardAsync(int pokedexNumber, CollectionCardRequestDto dto)
    {
        var pokemon = await _pokemonRepository.GetByPokedexNumberAsync(pokedexNumber);
        if (pokemon is null) throw new Exception("Pokemon não encontrado.");

        var entry = await _collectionRepository.GetByPokemonIdAsync(pokemon.Id);
        var card = await _cardRepository.GetById(dto.CardId);
        if (card == null) throw new ArgumentException("Carta não encontrada!");

        if (pokemon.Id != card.PokemonId) throw new ArgumentException("Pokemon passado diferente do pokemon da carta");

        var enumCondition = ParseEnum<ConditionCard>(dto.Condition);
        var enumLanguage = ParseEnum<LanguageCard>(dto.Language);
        var enumExtra = ParseEnum<ExtraInfoCard>(dto.Extra);

        if (entry == null)
        {
            entry = new CollectionEntry(dto.CardId, pokemon.Id, enumCondition, enumLanguage, enumExtra);
            await _collectionRepository.AddAsync(entry);
        }
        else
        {
            entry.ChangeCard(dto.CardId);
            entry.UpdateInfo(enumCondition, enumLanguage, enumExtra);
        }
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateCardAsync(int collectionId, CollectionCardUpdateRequestDto dto)
    {
        var entry = await _collectionRepository.GetByIdAsync(collectionId);

        if (entry is null)
            throw new ArgumentException("Carta não encontrada.");

        if (dto.Condition is not null)
        {
            var condition = ParseEnum<ConditionCard>(dto.Condition);
            entry.ChangeCondition(condition);
        }

        if (dto.Language is not null)
        {
            var language = ParseEnum<LanguageCard>(dto.Language);
            entry.ChangeLanguage(language);
        }

        if (dto.Extra is not null)
        {
            var extra = ParseEnum<ExtraInfoCard>(dto.Extra);
            entry.ChangeExtra(extra);
        }

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteCardAsync(int collectionId)
    {
        var entry = await _collectionRepository.GetByIdAsync(collectionId);
        if (entry is null) throw new ArgumentException("Carta não encontrada.");

        await _collectionRepository.DeleteAsync(entry);
        await _unitOfWork.SaveChangesAsync();
    }

    private static T ParseEnum<T>(string value) where T : struct, Enum
    {
        if (Enum.TryParse<T>(value, true, out var result))
            return result;

        throw new ArgumentException($"Valor '{value}' inválido.");
    }
}
