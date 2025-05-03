import React from 'react';
import { Formik, Form } from 'formik';
import * as Yup from 'yup';
import { TextField, Button, Box, Typography, Container } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { authService, LoginRequest } from '../services/authService';
import '../App.css';

const validationSchema = Yup.object({
    email: Yup.string().email('Geçerli bir email giriniz').required('Email gerekli'),
    password: Yup.string().min(6, 'En az 6 karakter olmalı').required('Şifre gerekli'),
});

const LoginForm: React.FC = () => {
    const navigate = useNavigate();

    const initialValues: LoginRequest = {
        email: '',
        password: '',
    };

    const handleSubmit = async (values: LoginRequest) => {
        try {
            const response = await authService.login(values);
            localStorage.setItem('token', response.token);
            window.location.href = '/dashboard';
        } catch (error) {
            console.error('Giriş hatası:', error);
        }
    };

    return (
        <Container maxWidth="sm">
            <Box sx={{ mt: 8, display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
                <Typography component="h1" variant="h4" sx={{ fontFamily: 'Merriweather', mb: 4 }}>
                    Giriş Yap
                </Typography>
                <Formik
                    initialValues={initialValues}
                    validationSchema={validationSchema}
                    onSubmit={handleSubmit}
                >
                    {({ errors, touched, handleChange, handleBlur }) => (
                        <Form className="content-area">
                            <TextField
                                fullWidth
                                margin="normal"
                                name="email"
                                label="Email"
                                onChange={handleChange}
                                onBlur={handleBlur}
                                error={touched.email && Boolean(errors.email)}
                                helperText={touched.email && errors.email}
                                sx={{ fontFamily: 'Merriweather' }}
                            />
                            <TextField
                                fullWidth
                                margin="normal"
                                name="password"
                                label="Şifre"
                                type="password"
                                onChange={handleChange}
                                onBlur={handleBlur}
                                error={touched.password && Boolean(errors.password)}
                                helperText={touched.password && errors.password}
                                sx={{ fontFamily: 'Merriweather' }}
                            />
                            <Button
                                type="submit"
                                fullWidth
                                className="btn-library"
                                sx={{ mt: 3, mb: 2 }}
                            >
                                Giriş Yap
                            </Button>
                        </Form>
                    )}
                </Formik>
            </Box>
        </Container>
    );
};

export default LoginForm;
