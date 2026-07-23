import "./Home.css";
import Header from "../../components/Headers/Header";
import PokemonCard from "../../components/PokemonCards/PokemonCard";

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

                    <PokemonCard />
                    <PokemonCard />
                    <PokemonCard />
                    <PokemonCard />
                    <PokemonCard />
                    <PokemonCard />

                </section>

            </main>
        </>
    );
}

export default Home;