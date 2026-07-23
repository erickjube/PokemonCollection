import "./Home.css";
import Header from "../../components/Headers/Header";
import PokemonCard from "../../components/PokemonCards/PokemonCard";


const pokemons = [
    { id: 1, nome: "Bulbasaur"},
    { id: 2, nome: "Ivysaur"},
    { id: 3, nome: "Venusaur"},
    { id: 4, nome: "Charmander"},
    { id: 5, nome: "Charmeleon"},
    { id: 6, nome: "Charizard"},
    { id: 7, nome: "Squirtle"},
    { id: 8, nome: "Wartortle"},
    { id: 9, nome: "Blastoise"},
    { id: 10, nome: "Caterpie"},
    { id: 11, nome: "Metapod"},
    { id: 12, nome: "Butterfree"},
    { id: 13, nome: "Weedle"},
    { id: 14, nome: "Kakuna"},
    { id: 15, nome: "Beedrill"},
    { id: 16, nome: "Pidgey"},
    { id: 17, nome: "Pidgeotto"},
    { id: 18, nome: "Pidgeot"},
    { id: 19, nome: "Rattata"},
    { id: 20, nome: "Raticate"},
];

function Home() {
    return (
        <>
            <Header />

            <main className="home">

                <section className="search-section">

                    <input type="text" placeholder="Pesquisar Pokémon..."/>

                    <button>Buscar</button>

                    <select>
                        <option>Numeração Pokedex [0-9]</option>
                    </select>

                </section>

                <section className="filters">

                    <select>
                        <option>Tipo</option>
                    </select>

                    <select>
                        <option>Geração</option>
                    </select>

                    <select>
                        <option>Região</option>
                    </select>

                </section>

                <section className="pokemon-grid">

                    {pokemons.map((pokemon) => (
                        <PokemonCard key={pokemon.id} pokemon={pokemon} />
                    ))}

                </section>

            </main>
        </>
    );
}

export default Home;