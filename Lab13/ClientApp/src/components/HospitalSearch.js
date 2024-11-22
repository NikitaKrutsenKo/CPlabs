import axios from 'axios';
import React, { useState } from 'react';

const HospitalSearch = () => {
    const [hospitalId, setHospitalId] = useState('');
    const [hospital, setHospital] = useState(null);
    const [error, setError] = useState(null);

    const handleSearch = async () => {
        try {
            const response = await axios.get(`http://localhost:7106/api/Hospital/${hospitalId}`);
            setHospital(response.data);
            setError(null);
        } catch (error) {
            setError('Hospital not found!');
            setHospital(null);
        }
    };

    return (
        <div>
            <h1>Search Hospital</h1>
            <input
                type="text"
                value={hospitalId}
                onChange={(e) => setHospitalId(e.target.value)}
                placeholder="Enter Hospital ID"
            />
            <button onClick={handleSearch}>Search</button>
            {hospital && (
                <div>
                    <h2>{hospital.hospitalName}</h2>
                    <p>{hospital.address.city}, {hospital.address.stateProvince}</p>
                    <p>{hospital.otherDetails}</p>
                </div>
            )}
            {error && <div>{error}</div>}
        </div>
    );
};

export default HospitalSearch;
