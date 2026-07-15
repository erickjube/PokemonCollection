using PokemonCollection.Domain.ENUMs;

namespace PokemonCollection.Domain.Entities;

public class CollectionEntry
{
    public int Id { get; private set; }

    public int PokemonId { get; private set; }
    public Pokemon Pokemon { get; private set; }

    public int CardId { get; private set; }
    public Card Card { get; private set; } = null!;

    public ConditionCard Condition { get; private set; } = ConditionCard.New;
    public LanguageCard Language { get; private set; } = LanguageCard.Portuguese;
    public ExtraInfoCard Extra { get; private set; } = ExtraInfoCard.Normal;
    public DateTime DateAdded { get; private set; } = DateTime.UtcNow;

    private CollectionEntry() { }
    public CollectionEntry(int cardId, int pokemonId, ConditionCard condition, LanguageCard language, ExtraInfoCard extra)
    {
        if (cardId <= 0) throw new ArgumentException("CardId deve ser positivo", nameof(cardId));
        if (pokemonId <= 0) throw new ArgumentException("PokemonId deve ser positivo", nameof(pokemonId));

        CardId = cardId;
        PokemonId = pokemonId;
        Condition = condition;
        Language = language;
        Extra = extra;
        DateAdded = DateTime.UtcNow;
    }


    public void UpdateInfo(ConditionCard condition, LanguageCard language, ExtraInfoCard extra)
    {
        Condition = condition;
        Language = language;
        Extra = extra;
    }

    public void ChangeCard(int newCardId)
    {
        if (newCardId <= 0) throw new ArgumentException("CardId deve ser positivo", nameof(newCardId));
        CardId = newCardId;
    }
}
