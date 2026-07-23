import "./PokemonCard.css";

function PokemonCard({ pokemon }) {
    return (
        
        <div className="pokemon-card">

            <img src={`https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/${pokemon.id}.png`}/>
            <h2>{pokemon.nome}</h2>
            <p>#{pokemon.id}</p>

        </div>
    );
}

export default PokemonCard;