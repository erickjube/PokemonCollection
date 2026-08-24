using PokemonCollection.Domain.ENUMs;

namespace PokemonCollection.Domain.Entities;

public class Card
{
    public int Id { get; private set; }
    public string ExternalId { get; private set; } = string.Empty;

    public int? PokemonId { get; private set; }
    public Pokemon? Pokemon { get; private set; }

    public string Name { get; private set; } = string.Empty;
    public string CardNumber { get; private set; } = string.Empty;
    public string Rarity { get; private set; } = string.Empty;
    public string ImageUrl { get; private set; } = string.Empty;
    public string SetName { get; private set; } = string.Empty; // nome da coleção da carta (151, Coroa Estelar, etc...)
    public string SetCode { get; private set; } = string.Empty; // sv3pt5
    public int SetPrintedTotal { get; private set; }
    public ICollection<CollectionEntry> CollectionEntries { get; private set; } = [];

    private Card() { }
    public Card(string externalId, int? pokemonId, string name, string cardNumber, string rarity, string imageUrl, string setName, string setCode, int setPrintedTotal)
    {
        if (string.IsNullOrWhiteSpace(externalId)) throw new ArgumentException("Id externo é obrigatório", nameof(externalId));
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Nome é obrigatório", nameof(name));
        if (string.IsNullOrWhiteSpace(cardNumber)) throw new ArgumentException("Número da Carta é obrigatório", nameof(cardNumber));
        if (string.IsNullOrWhiteSpace(imageUrl)) throw new ArgumentException("Imagem Url é obrigatória", nameof(imageUrl));
        if (string.IsNullOrWhiteSpace(setName)) throw new ArgumentException("Nome da Coleção é obrigatório", nameof(setName));
        if (string.IsNullOrWhiteSpace(setCode)) throw new ArgumentException("Código da Coleção é obrigatório", nameof(setCode));
        if (setPrintedTotal <= 0) throw new ArgumentException("Numero de cartas da Coleção deve ser positivo", nameof(setPrintedTotal));

        ExternalId = externalId;
        PokemonId = pokemonId;
        Name = name;
        CardNumber = cardNumber;
        Rarity = rarity;
        ImageUrl = imageUrl;
        SetName = setName;
        SetCode = setCode;
        SetPrintedTotal = setPrintedTotal;
    }

    public void Update(string name, string rarity, string imageUrl, string setName, int setPrintedTotal)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Nome é obrigatório", nameof(name));
        if (string.IsNullOrWhiteSpace(rarity)) throw new ArgumentException("Raridade é obrigatório", nameof(rarity));
        if (string.IsNullOrWhiteSpace(imageUrl)) throw new ArgumentException("Imagem Url é obrigatória", nameof(imageUrl));
        if (string.IsNullOrWhiteSpace(setName)) throw new ArgumentException("Nome da Coleção é obrigatório", nameof(setName));
        if (setPrintedTotal <= 0) throw new ArgumentException("Numero de cartas da Coleção deve ser positivo", nameof(setPrintedTotal));

        Name = name;
        Rarity = rarity;
        ImageUrl = imageUrl;
        SetName = setName;
        SetPrintedTotal = setPrintedTotal;
    }

}
