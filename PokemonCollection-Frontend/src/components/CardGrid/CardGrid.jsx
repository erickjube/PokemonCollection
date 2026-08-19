import "./CardGrid.css"

import { useState, useEffect } from "react";

import { useParams } from "react-router-dom";
import { useNavigate } from "react-router-dom";

import { addCardToCollection } from "../../services/CollectionService";


function CardGrid({ card }) {
    const { pokedexNumber } = useParams()

    const [selectDetails, setSelectDetails] = useState(false);
    const [condition, setCondition] = useState("New");
    const [language, setLanguage] = useState("Portuguese");
    const [extra, setExtra] = useState("Normal");
    const [error, setError] = useState("");
    const [loading, setLoading] = useState(false);

    const navigate = useNavigate();

    const handleCondition = (event) => {
        setCondition(event.target.value)
    };

    const handleLanguage = (event) => {
        setLanguage(event.target.value)
    };

    const handleExtra = (event) => {
        setExtra(event.target.value)
    };

    const handleRequest = async () => {
        try {
            setLoading(true);
            setError("");

            await addCardToCollection(pokedexNumber, {
                cardId: card.id,
                condition: condition,
                language: language,
                extra: extra
            });

            navigate(`/pokemon/${pokedexNumber}`);
        } 
        catch (error) {
            console.error(error);
            setError("Não foi possível salvar a carta na coleção.");
        } 
        finally {
            setLoading(false);
        }
    };

    return (
        <div className="card-grid-item" onClick={() => setSelectDetails(true)}>
            <div className="card-grid-image-container">

                <img className="card-grid-image" src={card.imageUrl} alt={card.name} />
                
                <div className="card-hover-overlay">
                    <button
                        className="add-card-button"
                        onClick={(e) => {e.stopPropagation(); setSelectDetails(true); }} >
                        +
                    </button>
                </div>

                {selectDetails && (
                    <div className="card-modal-overlay" onClick={() => setSelectDetails(false)} >
                        <div className="card-modal" onClick={(e) => e.stopPropagation()} >   

                            <button className="card-modal-close" onClick={() => setSelectDetails(false)} >
                                ×
                            </button>

                            <div className="card-modal-content">
                                <img src={card.imageUrl} alt={card.cardName} />
                                <div className="card-grid-modal-info">
                                    <h2>{card.name}  ({card.cardNumber}/{card.setPrintedTotal})</h2>

                                    <p>
                                        <strong>Condição: </strong>
                                        <select value={condition} onChange={handleCondition}>
                                            <option value="New">New</option>
                                            <option value="NearNew">Near New</option>
                                            <option value="LightlyUsed">Lightly Used</option>
                                            <option value="HeavilyUsed">Heavily Used</option>
                                            <option value="Damaged">Damaged</option>
                                        </select>
                                    </p>
                                    
                                    <p>
                                        <strong>Lingua: </strong>
                                        <select value={language} onChange={handleLanguage}>
                                            <option value="Portuguese">Portuguese</option>
                                            <option value="English">English</option>
                                            <option value="Japanese">Japanese</option>
                                            <option value="Korean">Korean</option>
                                            <option value="Chinese">Chinese</option>
                                            <option value="Spanish">Spanish</option>
                                            <option value="Italian">Italian</option>
                                        </select>
                                    </p>

                                    <p>
                                        <strong>Extra: </strong>
                                        <select value={extra} onChange={handleExtra}>
                                            <option value="Normal">Normal</option>
                                            <option value="ReverseFoil">Reverse Foil</option>
                                            <option value="Foil">Foil</option>
                                            <option value="PokeBall">PokeBall</option>
                                            <option value="MasterBall">MasterBall</option>
                                            <option value="Promo">Promo</option>
                                        </select>
                                    </p>

                                    <div className="card-grid-actions">
                                        <button className="btn-add-collection" 
                                            onClick={handleRequest}
                                            disabled={loading}>
                                                {loading ? "Adicionando..." : "Adicionar a Coleção"}
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                )}
            </div>

            <div className="card-grid-info">
                <p>{card.name}</p>
                <p>({card.cardNumber}/{card.setPrintedTotal})</p>
                <p>{card.setName}</p>
            </div>
        </div>
    );

}

export default CardGrid;