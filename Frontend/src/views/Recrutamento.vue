<template>
  <div class="recrutamento-container">
    <div class="search-box">
      <el-input v-model="searchQuery" placeholder="Buscar candidatos...">
        <template #prepend>
          <el-button :icon="Search" />
        </template>
      </el-input>
    </div>

    <el-row v-if="filteredCandidatos.length > 0" gutter="20">
      <el-col
        v-for="candidato in filteredCandidatos"
        :key="candidato.id"
        :span="24"
        class="card-col"
      >
        <el-card class="candidato-card" shadow="hover">
          <div class="card-content">
            <img
              src="https://via.placeholder.com/80"
              alt="Foto do candidato"
              class="candidato-foto"
            />
            <div class="candidato-info">
              <h2>{{ candidato.nome }}</h2>
              <p>
                <strong>Perfil: </strong>
                <a :href="candidato.urlPublica" target="_blank">{{ candidato.urlPublica }}</a>
              </p>
              <p><strong>Descrição: </strong>{{ candidato.descricaoProfissional }}</p>
              <p><strong>Resumo: </strong>{{ candidato.resumo }}</p>
            </div>
          </div>
        </el-card>
      </el-col>
    </el-row>

    <p v-else class="no-results">Nenhum candidato encontrado.</p>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from "vue";
import { Search } from "@element-plus/icons-vue";
import { getCandidatos } from "../services/candidatoService";

const searchQuery = ref("");
const candidatos = ref([]);

const fetchCandidatos = async () => {
  try {
    candidatos.value = await getCandidatos();
  } catch (error) {
    console.error("Erro ao carregar candidatos:", error);
  }
};

const filteredCandidatos = computed(() =>
  candidatos.value.filter((candidato) =>
    candidato.nome.toLowerCase().includes(searchQuery.value.toLowerCase()) ||
    candidato.descricaoProfissional?.toLowerCase().includes(searchQuery.value.toLowerCase()) ||
    candidato.resumo?.toLowerCase().includes(searchQuery.value.toLowerCase())
  )
);

onMounted(() => {
  fetchCandidatos();
});
</script>

<style scoped>
.search-box {
  margin-bottom: 20px;
}

.el-input {
  max-width: 600px;
  width: 100%;
  border: 2px solid #6c63ff;
  border-radius: 5px;
}

.el-input__inner {
  height: 40px;
}

.el-icon-search {
  color: #6c63ff;
  font-size: 18px;
}

.candidato-card {
  border-radius: 8px;
  display: flex;
  flex-direction: column;
  gap: 10px;
  height: auto;
}

.card-content {
  display: flex;
  align-items: center;
  gap: 20px;
}

.candidato-foto {
  width: 80px;
  height: 80px;
  border-radius: 50%;
}

.candidato-info {
  display: flex;
  flex-direction: column;
}

.candidato-info h3 {
  margin: 0;
  font-size: 16px;
}

.candidato-info p {
  font-size: 14px;
  margin: 1px 0;
}

.no-results {
  text-align: center;
  font-size: 16px;
  color: #666;
  margin-top: 20px;
}

.card-col {
  margin-bottom: 20px;
}
</style>
