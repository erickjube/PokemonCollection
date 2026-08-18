import Header from "../../components/Headers/Header";
import "./PokemonDetails.css";

import PokemonInfo from "../../components/PokemonInfo/PokemonInfo";
import CardInfo from "../../components/CardInfo/CardInfo";

import {useState, useEffect} from "react";

import { getPokemonByPokedexNumber } from "../../services/PokemonService";
import { getCardByPokedexNumber } from "../../services/CollectionService";

import { useParams } from "react-router-dom";
import { Link } from "react-router-dom";
import { useNavigate } from "react-router-dom";

function PokemonDetails() {
    const { pokedexNumber } = useParams();
    
    const [pokemon, setPokemon] = useState(null);
    const [card, setCard] = useState(null);
    
    const navigate = useNavigate();

    async function carregarDetalhesPokemon() {
        try {
            const pokemon = await getPokemonByPokedexNumber(pokedexNumber);
            setPokemon(pokemon);
        }
        catch (error) {
            console.error("Erro ao buscar os detalhes do pokémon:", error);
        }
    }

    async function carregarDetalhesCard() {
        try {
            const card = await getCardByPokedexNumber(pokedexNumber);
            setCard(card);
        }
        catch (error) {
            console.error("Erro ao buscar os detalhes do card:", error);
        }
    }

    useEffect(() => {
        carregarDetalhesPokemon();
        carregarDetalhesCard();
    }, [pokedexNumber]);

    function handleRemove() {
        console.log("Remover carta:", card);
    }

    function handleReplace() {
        navigate(`/cards/${pokedexNumber}`);
    }


    return (
        <>
            <Header />
            <div className="pokemon-details-page">
                <Link to="/">
                    <button className="btn-back">Voltar</button>
                </Link>
                
                <main className="pokemon-details-container">
                    <PokemonInfo pokemon={pokemon} />
                    <CardInfo card={card} onRemove={handleRemove} onReplace={handleReplace}/>
                </main>
            </div>
        </>
    );
}

export default PokemonDetails;