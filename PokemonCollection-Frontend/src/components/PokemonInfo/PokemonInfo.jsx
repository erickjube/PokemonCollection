import "./PokemonInfo.css";

function PokemonInfo({ pokemon }) {
    if (!pokemon) {
        return <p>Carregando...</p>;
    }

    return (
        <div className="pokemon-info">
            <div className="pokemon-image">
                <img src={pokemon.imageUrl} alt={pokemon.name} />
            </div>

            <div className="pokemon-details">
                <h2>{pokemon.name}</h2>
                <p>#{pokemon.PokedexNumber}</p>
                <p>Type: {pokemon.primaryType}</p>
                {pokemon.secondaryType && <p>Secondary Type: {pokemon.secondaryType}</p>}
                <p>Generation: {pokemon.generation}</p>
                <p>Region: {pokemon.region}</p>
            </div>

        </div>
    );
}

export default PokemonInfo;