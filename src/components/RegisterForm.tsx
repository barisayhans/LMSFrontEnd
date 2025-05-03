import React from 'react';
import { Formik, Form } from 'formik';
import * as Yup from 'yup';
import { TextField, Button, Box, Typography, Container } from '@mui/material';
import { authService, RegisterRequest } from '../services/authService';
import '../App.css';

const validationSchema = Yup.object({
    firstName: Yup.string().required('İsim gerekli'),
    lastName: Yup.string().required('Soyisim gerekli'),
    email: Yup.string().email('Geçerli bir email giriniz').required('Email gerekli'),
    userName: Yup.string().min(6, 'En az 6 karakter olmalı').required('Kullanıcı adı gerekli'),
    password: Yup.string().min(6, 'En az 6 karakter olmalı').required('Şifre gerekli'),
    confirmPassword: Yup.string()
        .oneOf([Yup.ref('password')], 'Şifreler eşleşmiyor')
        .required('Şifre tekrarı gerekli'),
});

const RegisterForm: React.FC = () => {
    const initialValues: RegisterRequest = {
        firstName: '',
        lastName: '',
        email: '',
        userName: '',
        password: '',
        confirmPassword: '',
    };

    const handleSubmit = async (values: RegisterRequest) => {
        try {
            const response = await authService.register(values);
            console.log('Kayıt başarılı:', response);
        } catch (error) {
            console.error('Kayıt hatası:', error);
        }
    };

    return (
        <Container maxWidth="sm">
            <Box sx={{ mt: 3, display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
                <Typography component="h1" variant="h4" sx={{ fontFamily: 'Merriweather', mb: 4 }}>
                    Kayıt Ol
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
                                name="firstName"
                                label="İsim"
                                onChange={handleChange}
                                onBlur={handleBlur}
                                error={touched.firstName && Boolean(errors.firstName)}
                                helperText={touched.firstName && errors.firstName}
                                sx={{ fontFamily: 'Merriweather' }}
                            />
                            <TextField
                                fullWidth
                                margin="normal"
                                name="lastName"
                                label="Soyisim"
                                onChange={handleChange}
                                onBlur={handleBlur}
                                error={touched.lastName && Boolean(errors.lastName)}
                                helperText={touched.lastName && errors.lastName}
                                sx={{ fontFamily: 'Merriweather' }}
                            />
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
                                name="userName"
                                label="Kullanıcı Adı"
                                onChange={handleChange}
                                onBlur={handleBlur}
                                error={touched.userName && Boolean(errors.userName)}
                                helperText={touched.userName && errors.userName}
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
                            <TextField
                                fullWidth
                                margin="normal"
                                name="confirmPassword"
                                label="Şifre Tekrarı"
                                type="password"
                                onChange={handleChange}
                                onBlur={handleBlur}
                                error={touched.confirmPassword && Boolean(errors.confirmPassword)}
                                helperText={touched.confirmPassword && errors.confirmPassword}
                                sx={{ fontFamily: 'Merriweather' }}
                            />
                            <Button
                                type="submit"
                                fullWidth
                                className="btn-library"
                                sx={{ mt: 3, mb: 2 }}
                            >
                                Kayıt Ol
                            </Button>
                        </Form>
                    )}
                </Formik>
            </Box>
        </Container>
    );
};

export default RegisterForm; 