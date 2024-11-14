import axios from 'axios';
import { BASE_URL } from '@/config';

const apiClient = axios.create({
  baseURL: BASE_URL,
  headers: {
    'Content-Type': 'application/json'
  }
});

export const login = (email, senha) => {
    return apiClient.post('/api/v1/auth/login', {
        email,
        senha
    });
};
