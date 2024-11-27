
import React, { useState, useEffect } from 'react';
import axios from 'axios';
import '../App.css'; // Import CSS

function App() {
    const [properties, setProperties] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    // Fetch data from API
    useEffect(() => {
        axios.get('https://localhost:7143/api/Properties') // Replace with the correct API URL
            .then(response => {
                setProperties(response.data); // Set data to state
                setLoading(false);
            })
            .catch(error => {
                setError("Error: " + error.message);
                setLoading(false);
            });
    }, []);

    if (loading) return <div className="loading">Loading...</div>;
    if (error) return <div className="error">{error}</div>;

    return (
        <div className="app">
            <h1>Property Listings</h1>
            <div className="properties">
                {properties.map((property) => (
                    <div key={property.propertyId} className="property">
                        <h2>{property.propertyName}</h2>

                        {/* Features Section */}
                        <div className="features">
                            <h3>Features</h3>
                            <ul>
                                {property.features.map((feature, index) => (
                                    <li key={index}>{feature}</li>
                                ))}
                            </ul>
                        </div>

                        {/* Highlights Section */}
                        <div className="highlights">
                            <h3>Highlights</h3>
                            <ul>
                                {property.highlights.map((highlight, index) => (
                                    <li key={index}>{highlight}</li>
                                ))}
                            </ul>
                        </div>

                        {/* Transportation Section */}
                        <div className="transportation">
                            <h3>Transportation</h3>
                            <ul>
                                {property.transportation.map((transit, index) => (
                                    <li key={index}>
                                        {transit.type}: {transit.line ? transit.line : "No line available"} - {transit.distance}
                                    </li>
                                ))}
                            </ul>
                        </div>

                        {/* Spaces Section */}
                        <div className="spaces">
                            <h3>Spaces</h3>
                            {property.spaces.map((space) => (
                                <div key={space.spaceId} className="space">
                                    <h4>{space.spaceName}</h4>
                                    <table>
                                        <thead>
                                            <tr>
                                                <th>Month</th>
                                                <th>Rent</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            {space.rentRoll.map((rent, index) => (
                                                <tr key={index}>
                                                    <td>{rent.month}</td>
                                                    <td>${rent.rent}</td>
                                                </tr>
                                            ))}
                                        </tbody>
                                    </table>
                                </div>
                            ))}
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
}

export default App;
