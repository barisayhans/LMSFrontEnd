import axios from 'axios';

const API_URL = 'https://localhost:9001/api/Account';

export interface RegisterRequest {
    firstName: string;
    lastName: string;
    email: string;
    userName: string;
    password: string;
    confirmPassword: string;
}

export interface LoginRequest {
    email: string;
    password: string;
}

export const authService = {
    login: async (data: LoginRequest) => {
        const response = await axios.post(`${API_URL}/authenticate`, data);
        return response.data;
    },
    register: async (data: RegisterRequest) => {
        const response = await axios.post(`${API_URL}/register`, data);
        return response.data;
    },
    forgotPassword: async (email: string) => {
        const response = await axios.post(`${API_URL}/forgot-password`, { email });
        return response.data;
    },
    resetPassword: async (model: { token: string; password: string; confirmPassword: string }) => {
        const response = await axios.post(`${API_URL}/reset-password`, model);
        return response.data;
    },
    confirmEmail: async (userId: string, code: string) => {
        const response = await axios.get(`${API_URL}/confirm-email?userId=${userId}&code=${code}`);
        return response.data;
    }
}; 