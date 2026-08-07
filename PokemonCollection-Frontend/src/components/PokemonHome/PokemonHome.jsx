import "./PokemonHome.css";

function PokemonHome({ pokemon }) {

    return (
        <div className="pokemon-home">

            <img
                src={pokemon.imageUrl}
                alt={pokemon.name}
            />

            <h2>{pokemon.name}</h2>
            <p>#{pokemon.pokedexNumber}</p>

        </div>
    );
}

export default PokemonHome;