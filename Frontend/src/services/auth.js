import axios from 'axios';

// Configura��o da base URL
const apiClient = axios.create({
  baseURL: 'https://localhost:8000',
  headers: {
    'Content-Type': 'application/json'
  }
});

// Fun��o de login
export const login = (email, senha) => {
    return apiClient.post('/api/v1/auth/login', {
        email,
        senha
    });
};

