import React, { useEffect } from 'react';
import { useSearchParams } from 'react-router-dom';
import { Container, Typography, Box } from '@mui/material';
import { authService } from '../services/authService';

const ConfirmEmail: React.FC = () => {
    const [searchParams] = useSearchParams();
    const userId = searchParams.get('userId');
    const code = searchParams.get('code');

    useEffect(() => {
        const confirmEmail = async () => {
            if (userId && code) {
                try {
                    await authService.confirmEmail(userId, code);
                } catch (error) {
                    console.error('Email onaylama hatası:', error);
                }
            }
        };

        confirmEmail();
    }, [userId, code]);

    return (
        <Container maxWidth="sm">
            <Box sx={{ mt: 8, textAlign: 'center' }}>
                <Typography variant="h5">
                    Email adresiniz onaylanıyor...
                </Typography>
            </Box>
        </Container>
    );
};

export default ConfirmEmail; 