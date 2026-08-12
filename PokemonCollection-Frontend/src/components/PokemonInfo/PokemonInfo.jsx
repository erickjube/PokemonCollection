import "./PokemonInfo.css";

function PokemonInfo({ pokemon }) {
    if (!pokemon) {
        return <p>Carregando...</p>;
    }

    return (
        <div className="pokemon-info">
            <div className="pokemon-details">
                <div className="pokemon-title">
                    <h1>#{pokemon.pokedexNumber}</h1>
                    <h1>{pokemon.name}</h1>
                </div>

                <div className="pokemon-image">
                    <img src={pokemon.imageUrl} alt={pokemon.name} />
                </div>

                <div className="pokemon-infos">
                    <p>{pokemon.generation}</p>
                    <p>{pokemon.region}</p>
                    <p>{pokemon.primaryType}</p>
                    {pokemon.secondaryType && <p>{pokemon.secondaryType}</p>}
                </div>
            </div> 
        </div>
    );
}

export default PokemonInfo;