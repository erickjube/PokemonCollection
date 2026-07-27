import "./PokemonCard.css";

function PokemonCard({ pokemon }) {

    return (
        <div className="pokemon-card">

            <img
                src={pokemon.imageUrl}
                alt={pokemon.name}
            />

            <h2>{pokemon.name}</h2>
            <p>#{pokemon.id}</p>

        </div>
    );
}

export default PokemonCard;