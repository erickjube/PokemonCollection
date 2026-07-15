using PokemonCollection.Application.DTOs.CollectionEntryDtos;
using PokemonCollection.Application.Interfaces.Repositories;
using PokemonCollection.Application.Interfaces.Services;
using PokemonCollection.Domain.Entities;
using PokemonCollection.Domain.ENUMs;

namespace PokemonCollection.Application.Services;

public class CollectionEntryService : ICollectionEntryService
{
    private readonly ICollectionRepository _collectionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CollectionEntryService(ICollectionRepository collectionRepository, IUnitOfWork unitOfWork)
    {
        _collectionRepository = collectionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<CollectionCardResponseDto>> GetCollectionAsync()
    {
        var collections = await _collectionRepository.GetAllAsync();

        return collections.Select(c => new CollectionCardResponseDto
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
            DateAdded = c.DateAdded
        });
    }

    public async Task<CollectionCardResponseDto> GetCollectionCardByIdAsync(int collectionEntryId)
    {
        var collection = await _collectionRepository.GetByIdAsync(collectionEntryId);
        if (collection is null) throw new ArgumentException("Carta não encontrada");
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
            DateAdded = collection.DateAdded
        };
    }

    public async Task SelectCardAsync(CollectionCardRequestDto dto)
    {
        var entry = await _collectionRepository.GetByPokemonIdAsync(dto.PokemonId);
        var enumCondition = ParseEnum<ConditionCard>(dto.Condition);
        var enumLanguage = ParseEnum<LanguageCard>(dto.Language);
        var enumExtra = ParseEnum<ExtraInfoCard>(dto.Extra);

        if (entry == null)
        {
            entry = new CollectionEntry(dto.CardId, dto.PokemonId, enumCondition, enumLanguage, enumExtra);
            await _collectionRepository.AddAsync(entry);
        }
        else
        {
            entry.ChangeCard(dto.CardId);
            entry.UpdateInfo(enumCondition, enumLanguage, enumExtra);
        }
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateCardAsync(CollectionCardRequestDto dto)
    {
        var entry = await _collectionRepository.GetByPokemonIdAsync(dto.PokemonId);
        if (entry is null) throw new ArgumentException("Carta não encontrada.");

        var enumCondition = ParseEnum<ConditionCard>(dto.Condition);
        var enumLanguage = ParseEnum<LanguageCard>(dto.Language);
        var enumExtra = ParseEnum<ExtraInfoCard>(dto.Extra);

        entry.UpdateInfo(enumCondition, enumLanguage, enumExtra);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteCardAsync(int pokemonId)
    {
        var entry = await _collectionRepository.GetByPokemonIdAsync(pokemonId);
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
