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

const isSidebarExpanded = ref(false);

const toggleSidebar = () => {
  isSidebarExpanded.value = !isSidebarExpanded.value;
};
</script>


<style scoped>
.el-container {
  display: flex; 
  height: 100vh;
}

.el-aside {
  width: 60px;
  transition: width 0.3s ease;
}

.el-aside.expanded {
  width: 240px;
}

.el-main {
  flex: 1;
  padding: 20px;
  transition: margin-left 0.3s ease;
}
</style>
