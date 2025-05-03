import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { Container, Typography, Box } from '@mui/material';

const Dashboard: React.FC = () => {
    const navigate = useNavigate();

    useEffect(() => {
        const token = localStorage.getItem('token');
        if (!token) {
            navigate('/login');
        }
    }, [navigate]);

    return (
        <Container maxWidth="lg">
            <Box sx={{ mt: 4 }}>
                <Typography variant="h4" component="h1" gutterBottom>
                    Hoş Geldiniz
                </Typography>
                <Typography variant="body1">
                    Kullanıcı paneline başarıyla giriş yaptınız.
                </Typography>
            </Box>
        </Container>
    );
};

export default Dashboard; 