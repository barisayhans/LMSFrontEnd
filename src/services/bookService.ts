import axios from 'axios';

const API_URL = 'https://localhost:9001/api/v1/book';

export const bookService = {
    getAllBooks: async () => {
        const response = await axios.get(API_URL);
        return response.data;
    },
    getBookById: async (id: string) => {
        const response = await axios.get(`${API_URL}/${id}`);
        return response.data;
    },
    addBook: async (book: any) => {
        const response = await axios.post(API_URL, book);
        return response.data;
    },
    updateBook: async (id: string, book: any) => {
        const response = await axios.put(`${API_URL}/${id}`, book);
        return response.data;
    },
    deleteBook: async (id: string) => {
        const response = await axios.delete(`${API_URL}/${id}`);
        return response.data;
    }
}; 