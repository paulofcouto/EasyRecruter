<template>
  <el-container style="height: 100vh;">
    <!-- Menu Lateral -->
    <el-aside
      v-if="$route.meta.showMenu"
      :class="{ expanded: isSidebarExpanded }"
      style="color: var(--vt-c-white);"
    >
      <MenuLateral @toggle="toggleSidebar" />
    </el-aside>

    <!-- Conteúdo Principal -->
    <el-container style="flex-direction: column; flex: 1;">
      <!-- Cabeçalho -->
      <MenuSuperior v-if="$route.meta.showHeader" />

      <!-- Área de Conteúdo -->
      <el-main style="padding: 20px;">
        <router-view />
      </el-main>
    </el-container>
  </el-container>
</template>

<script setup>
import { ref } from "vue";
import "./assets/main.css";
import MenuLateral from "@/components/MenuLateral.vue";
import MenuSuperior from "@/components/MenuSuperior.vue";

// Estado do menu lateral
const isSidebarExpanded = ref(false);

// Função para alternar o estado do menu lateral
const toggleSidebar = () => {
  isSidebarExpanded.value = !isSidebarExpanded.value;
};
</script>


<style scoped>
/* Estilos do layout principal */
.el-container {
  display: flex; /* Define layout flex para ajustar os elementos */
  height: 100vh; /* Altura total */
}

.el-aside {
  width: 60px; /* Largura do menu contraído */
  transition: width 0.3s ease; /* Transição suave para expansão */
}

.el-aside.expanded {
  width: 240px; /* Largura do menu expandido */
}

.el-main {
  flex: 1; /* Faz o conteúdo principal ocupar o restante do espaço */
  padding: 20px;
  transition: margin-left 0.3s ease; /* Suaviza o ajuste */
}
</style>
