import React, { useState, useEffect } from 'react';
import { BrowserRouter as Router, Routes, Route, Link, useNavigate } from 'react-router-dom';
import { AppBar, Toolbar, Typography, Button, Container } from '@mui/material';
import LoginForm from './components/LoginForm';
import RegisterForm from './components/RegisterForm';
import ConfirmEmail from './components/ConfirmEmail';
import Dashboard from './components/Dashboard';
import SplashScreen from './components/SplashScreen';
import { authService } from './services/authService';
import './App.css';

const AppContent: React.FC = () => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const navigate = useNavigate();

    useEffect(() => {
        const token = localStorage.getItem('token');
        setIsAuthenticated(!!token);
    }, []);

    const handleLogout = () => {
        localStorage.removeItem('token');
        setIsAuthenticated(false);
        navigate('/login');
    };

    return (
        <div className="App">
            <div className="library-bg">
                <div className="wave-top"></div>
                <div className="wave-bottom"></div>
                <Typography className="logo">KOKB</Typography>
                <div className="nav-buttons">
                    {isAuthenticated ? (
                        <Button className="btn-library" onClick={handleLogout}>
                            Çıkış
                        </Button>
                    ) : (
                        <>
                            <Button className="btn-library" component={Link} to="/login">
                                Giriş
                            </Button>
                            <Button className="btn-library" component={Link} to="/register">
                                Kayıt Ol
                            </Button>
                        </>
                    )}
                </div>
                <Container>
                    <Routes>
                        <Route path="/login" element={<LoginForm />} />
                        <Route path="/register" element={<RegisterForm />} />
                        <Route path="/api/account/confirm-email" element={<ConfirmEmail />} />
                        <Route path="/dashboard" element={<Dashboard />} />
                        <Route path="/" element={<SplashScreen />} />
                    </Routes>
                </Container>
            </div>
        </div>
    );
};

function App() {
    return (
        <Router>
            <AppContent />
        </Router>
    );
}

export default App;