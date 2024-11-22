import axios from 'axios';
import React, { useEffect, useState } from 'react';

const HospitalList = () => {
    const [hospitals, setHospitals] = useState([]);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchHospitals = async () => {
            try {
                const response = await axios.get('https://localhost:7106/api/Hospital');
                setHospitals(response.data);
            } catch (error) {
                setError('There was an error fetching the hospitals!');
            }
        };

        fetchHospitals();
    }, []);

    if (error) {
        return <div>{error}</div>;
    }

    return (
        <div>
            <h1>Hospitals</h1>
            <ul>
                {hospitals.map(hospital => (
                    <li key={hospital.id}>
                        {hospital.hospitalName} - {hospital.address.city}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default HospitalList;
