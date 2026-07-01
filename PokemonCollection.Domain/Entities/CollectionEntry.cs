using PokemonCollection.Domain.ENUMs;

namespace PokemonCollection.Domain.Entities;

public class CollectionEntry
{
    public int Id { get; private set; }

    public int PokemonId { get; private set; }
    public Pokemon Pokemon { get; private set; } = null!;

    public int CardId { get; private set; }
    public Card Card { get; private set; } = null!;

    public ConditionCard Condition { get; private set; } = ConditionCard.New;
    public LanguageCard Language { get; private set; } = LanguageCard.Portuguese;
    public ExtraInfoCard Extra { get; private set; } = ExtraInfoCard.Normal;
    public DateTime DateAdded { get; private set; } = DateTime.UtcNow;

    public CollectionEntry() { }
    public CollectionEntry(int pokemonId, int cardId, ConditionCard condition, LanguageCard language, ExtraInfoCard extra)
    {
        if (pokemonId <= 0) throw new ArgumentException("PokemonId deve ser obrigatório", nameof(pokemonId));
        if (cardId <= 0) throw new ArgumentException("CardId deve ser obrigatório", nameof(cardId));

        PokemonId = pokemonId;
        CardId = cardId;
        Condition = condition;
        Language = language;
        Extra = extra;
        DateAdded = DateTime.UtcNow;
    }
}
