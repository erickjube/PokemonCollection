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
            state = new ImportState(1);
            await _importStateRepository.AddAsync(state);
            await _unitOfWork.SaveChangesAsync();
        }

        var pokemonDictionary = (await _pokemonRepository.GetAllForImportAsync()).ToDictionary(p => p.PokedexNumber, p => p.Id);
        int page = state.LastImportedPage + 1;
        const int pageSize = 250;
        const int maxRetries = 10;
        var existingIds = (await _cardRepository.GetAllExternalIdsAsync()).ToHashSet();
        var totalPages = int.MaxValue;

        while (page <= totalPages)
        {
            CardListResponseDto? response = null;
            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    Console.WriteLine($"Importando página {page} - tentativa {attempt}/{maxRetries}");
                    response = await _pokemonTcg.GetCardsAsync(page, pageSize);
                    if (response == null) throw new Exception("Resposta nula.");

                    if (response.Data == null || response.Data.Count == 0)
                        throw new Exception($"Página {page} retornou data vazia.");
                    
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro na página {page}: {ex.Message}");
                    if (attempt == maxRetries) throw;
                    var delaySeconds = Math.Min(Math.Pow(2, attempt), 32);
                    await Task.Delay(TimeSpan.FromSeconds(delaySeconds));
                }
            }

            if (response == null) throw new Exception($"Não foi possível obter a página {page}.");

            totalPages = (int)Math.Ceiling((double)response.TotalCount / pageSize);

            Console.WriteLine($"Página {page}/{totalPages} - " + $"{response.Count} cartas");

            var cards = new List<Card>();
            
            foreach (var cardDto in response.Data)
            {
                if (!existingIds.Add(cardDto.Id)) continue;

                int? pokemonId = null;

                var pokedexNumber = cardDto.NationalPokedexNumbers?.FirstOrDefault();
                if (pokedexNumber != null)
                {
                    if (pokemonDictionary.TryGetValue(pokedexNumber.Value, out var id))
                        pokemonId = id;
                }

                var newCard = CreateCard(cardDto, pokemonId);
                cards.Add(newCard);
            }

            await _cardRepository.AddRangeAsync(cards);
            state.Update(page);
            await _unitOfWork.SaveChangesAsync();
            Console.WriteLine($"Página {page}/{totalPages} - " + $"{response.Count} cartas");
            page++;
            await Task.Delay(500);
        }
    }

    private Card CreateCard(CardImportResponseDto dto, int? pokemonId)
    {
        return new Card(
            externalId: dto.Id,
            pokemonId: pokemonId,
            name: dto.Name,
            cardNumber: dto.Number,
            rarity: dto.Rarity ?? "Unknown",
            imageUrl: dto.Images.Small,
            setName: dto.Set.Name,
            setPrintedTotal: dto.Set.PrintedTotal,
            setCode: dto.Set.Id
        );
    }
}
