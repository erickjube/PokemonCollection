import "./PokemonCard.css";

function PokemonCard() {
    return (
        <div className="pokemon-card">

            <img
                src="https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/25.png"
                alt="Pikachu"
            />

            <h2>Pikachu</h2>

            <p>#025</p>

        </div>
    );
}

export default PokemonCard;