import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import Header from './components/Header';

const App = () => {
    return (
        <>
            <Header />
            <Routes>
                {AppRoutes.map((route, index) => (
                    <Route key={index} path={route.path} element={route.element} index={route.index} />
                ))}
            </Routes>
        </>
    );
};

export default App;
