using PokemonCollection.Application.DTOs.CardsDtos;
using PokemonCollection.Application.Interfaces.Repositories;
using PokemonCollection.Application.Interfaces.Services;
using PokemonCollection.Domain.Entities;
using PokemonCollection.Domain.ENUMs;

namespace PokemonCollection.Application.Services;

public class CardImportService : ICardImportService
{
    private readonly IPokemonRepository _pokemonRepository;
    private readonly IPokemonTcgClient _pokemonTcg;
    private readonly ICardRepository _cardRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CardImportService(IPokemonRepository pokemonRepository,
                             IPokemonTcgClient pokemonTcg,
                             ICardRepository cardRepository,
                             IUnitOfWork unitOfWork)
    {
        _pokemonRepository = pokemonRepository;
        _pokemonTcg = pokemonTcg;
        _cardRepository = cardRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ImportAsync()
    {
        var pokemonDictionary = (await _pokemonRepository.GetAllAsync()).ToDictionary(p => p.PokedexNumber, p => p.Id);
        int page = 1;
        const int pageSize = 100;

        while (true)
        {
            var response = await _pokemonTcg.GetCardsAsync(page, pageSize);
            if (response == null || response.Data.Count == 0) break;

            var cards = new List<Card>();

            foreach (var cardDto in response.Data)
            {
                if (await _cardRepository.ExistsByExternalIdAsync(cardDto.Id)) continue;

                var pokedexNumber = cardDto.NationalPokedexNumbers?.FirstOrDefault();
                if (pokedexNumber == null) continue;

                if (!pokemonDictionary.TryGetValue(pokedexNumber.Value, out var pokemonId)) continue;

                cards.Add(CreateCard(cardDto, pokemonId));
            }

            await _cardRepository.AddRangeAsync(cards);
            await _unitOfWork.SaveChangesAsync();
            page++;
        }
    }

    private RarityCard GetRarity(string rarity)
    {
        return rarity?.Trim() switch
        {
            "Common" => RarityCard.Common,
            "Uncommon" => RarityCard.Uncommon,
            "Rare" => RarityCard.Rare,
            "Rare Holo" => RarityCard.RareHolo,
            "Rare BREAK" => RarityCard.RareBREAK,
            "Rare Prism Star" => RarityCard.RarePrismStar,
            "Amazing Rare" => RarityCard.AmazingRare,
            "Rare ACE" => RarityCard.RareACE,
            "Rare Shiny" => RarityCard.RareShiny,
            "Rare Holo V" => RarityCard.RareHoloV,
            "Rare Holo EX" => RarityCard.RareHoloEX,
            "Rare Holo GX" => RarityCard.RareHoloGX,
            "Rare Holo VMAX" => RarityCard.RareHoloVMAX,
            "Rare Prime" => RarityCard.RarePrime,
            "Rare Ultra" => RarityCard.RareUltra,
            "Rare Shiny GX" => RarityCard.RareShinyGX,
            "Rare Holo LV.X" => RarityCard.RareHoloLVX,
            "Rare Shining" => RarityCard.RareShining,
            "LEGEND" => RarityCard.Legend,
            "Rare Rainbow" => RarityCard.RareRainbow,
            "Rare Secret" => RarityCard.RareSecret,
            "Rare Holo Star" => RarityCard.RareHoloStar,
            "Promo" => RarityCard.Promo,
            _ => RarityCard.Unknown
        };
    }

    private Card CreateCard(CardResponseDto dto, int? pokemonId)
    {
        return new Card(
            externalId: dto.Id,
            pokemonId: pokemonId,
            name: dto.Name,
            cardNumber: dto.Number,
            rarity: GetRarity(dto.Rarity),
            imageUrl: dto.Images.Small,
            setName: dto.Set.Name,
            setCode: dto.Set.Id
        );
    }
}
