import "./CardGrid.css"

function CardGrid({ card }) {
    
    return (
        <div className="card-grid-item">
            <img className="card-grid-image" src={card.imageUrl} alt={card.name} />

            <div className="card-grid-info">
                <p>{card.name}</p>
                <p>({card.cardNumber}/{card.setPrintedTotal})</p>
                <p>{card.setName}</p>
            </div>
            
        </div>
    );

}

export default CardGrid;