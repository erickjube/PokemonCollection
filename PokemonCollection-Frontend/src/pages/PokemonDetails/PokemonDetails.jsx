import Header from "../../components/Headers/Header";
import PokemonInfo from "../../components/PokemonInfo/PokemonInfo";
import {useState, useEffect} from "react";
import { getPokemonByPokedexNumber } from "../../services/PokemonService";
import { useParams } from "react-router-dom";

function PokemonDetails() {
    const { pokedexNumber } = useParams();
    
    const [pokemon, setPokemon] = useState(null);
    
    async function carregarDetalhesPokemon() {
        try {
            const pokemon = await getPokemonByPokedexNumber(pokedexNumber);
            setPokemon(pokemon);
        }
        catch (error) {
            console.error("Erro ao buscar os detalhes do pokémon:", error);
        }
    }

    useEffect(() => {
        carregarDetalhesPokemon();
    }, [pokedexNumber]);

    return (
        <>
            <Header />
            <PokemonInfo pokemon={pokemon} />
            
        </>
    );
}

export default PokemonDetails;