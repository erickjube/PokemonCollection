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
    private readonly IImportStateRepository _importStateRepository;

    public CardImportService(IPokemonRepository pokemonRepository,
                             IPokemonTcgClient pokemonTcg,
                             ICardRepository cardRepository,
                             IUnitOfWork unitOfWork,
                             IImportStateRepository importStateRepository)
    {
        _pokemonRepository = pokemonRepository;
        _pokemonTcg = pokemonTcg;
        _cardRepository = cardRepository;
        _unitOfWork = unitOfWork;
        _importStateRepository = importStateRepository;
    }

    public async Task ImportAsync()
    {
        var state = await _importStateRepository.GetAsync();
        if (state == null)
        {
            state = new ImportState(110);
            await _importStateRepository.AddAsync(state);
            await _unitOfWork.SaveChangesAsync();
        }

        var pokemonDictionary = (await _pokemonRepository.GetAllAsync()).ToDictionary(p => p.PokedexNumber, p => p.Id);
        int page = state.LastImportedPage + 1;
        const int pageSize = 50;
        const int maxRetries = 3;
        var existingIds = await _cardRepository.GetAllExternalIdsAsync();
        var totalPages = int.MaxValue;

        while (page <= totalPages)
        {
            CardListResponseDto? response = null;
            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    Console.WriteLine($"Importando página {page}...");
                    response = await _pokemonTcg.GetCardsAsync(page, pageSize);
                    break;
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Erro na página {page}. Tentativa {attempt}/{maxRetries}");
                    if (attempt == maxRetries) throw;
                    await Task.Delay(5000);
                }
            }

            if (response == null || response.Data.Count == 0) break;

            totalPages = (int)Math.Ceiling((double)response.TotalCount / pageSize);

            var cards = new List<Card>();
            
            foreach (var cardDto in response.Data)
            {
                if (existingIds.Contains(cardDto.Id)) continue;

                var pokedexNumber = cardDto.NationalPokedexNumbers?.FirstOrDefault();
                if (pokedexNumber == null) continue;

                if (!pokemonDictionary.TryGetValue(pokedexNumber.Value, out var pokemonId)) continue;


                var newCard = CreateCard(cardDto, pokemonId);
                cards.Add(newCard);
                existingIds.Add(newCard.ExternalId);
            }

            await _cardRepository.AddRangeAsync(cards);
            state.Update(page);
            await _unitOfWork.SaveChangesAsync();
            await Task.Delay(500);
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

    private Card CreateCard(CardImportResponseDto dto, int? pokemonId)
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
