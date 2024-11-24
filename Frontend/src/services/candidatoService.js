// src/services/candidatoService.js
import api from "./api";

export const getCandidatos = async () => {
  try {
    const response = await api.get("/api/v1/Candidato/candidatos");
    return response.data;
  } catch (error) {
    console.error("Erro ao buscar candidatos:", error);
    throw error;
  }
};
