import "./CardInfo.css";

function CardInfo({}) {
    return (
        <div className="card-info">
            <div className="card-title">
                <h1>Team Rocket's Mewtwo ex</h1>
                <h1>(281/217)</h1>
            </div>

            <div className="card-image">
                <img src="https://images.scrydex.com/pokemon/me2pt5-281/large" alt="Mewtwo ex" />
            </div>

            <div className="card-infos">
                <p>Special Illustration Rare</p>
                <p>Ascended Heroes</p>
            </div>
        </div>  
    );
}

export default CardInfo;