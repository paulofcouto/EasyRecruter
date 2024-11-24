<template>
  <div :class="['sidebar', { expanded: isExpanded }]">
    <div class="logo-container">
      <img src="@/assets/logo.png" alt="Logo" class="logo" />
      <span v-if="isExpanded" class="tituloSistema">Easy Recruter</span>
    </div>
    <div class="divider"></div>
    <ul>
      <li v-for="item in menuItems" :key="item.id" @click="navigate(item.route)">
        <el-icon>
          <component :is="item.icon" />
        </el-icon>
        <span v-if="isExpanded">{{ item.label }}</span>
      </li>
    </ul>
    <div class="divider"></div>
    <button class="toggle-btn" @click="toggleSidebar">
      <component :is="isExpanded ? 'Fold' : 'Expand'" />
    </button>
  </div>
</template>

<script setup>
import { onMounted, ref } from "vue";
import { useRouter } from "vue-router";
import { Suitcase, InfoFilled, Lightning, Expand, Fold } from "@element-plus/icons-vue";

const isExpanded = ref(false);

const menuItems = [
  { id: 1, label: "Recrutamento", icon: Suitcase, route: "/recrutamento" },
  { id: 2, label: "Sobre", icon: InfoFilled, route: "/sobre" },
  { id: 3, label: "Sugestões", icon: Lightning, route: "/sugestoes" },
];

const router = useRouter();

const emit = defineEmits(["toggle"]);

const toggleSidebar = () => {
  isExpanded.value = !isExpanded.value;
  emit("toggle", isExpanded.value); 
};

const navigate = (route) => {
  router.push(route);
};

</script>

<style scoped>
.sidebar {
  width: 60px;
  background-color: #2c3e50;
  color: white;
  height: 100vh;
  overflow: hidden;
  position: fixed;
  display: flex;
  flex-direction: column;
  align-items: flex-end;
  transition: width 0.3s ease;
}

.sidebar .el-icon {
  font-size: 20px;
  width: 20px;
  height: 20px;
  color: white;
  display: inline-block;
  vertical-align: middle;
}

.sidebar.expanded {
  width: 240px;
}

.sidebar.expanded .el-icon {
  margin-right: 10px;
}

.sidebar ul {
  list-style: none;
  padding: 0;
  margin: 0;
  width: 100%;
}

.sidebar li {
  display: flex;
  align-items: center;
  padding: 10px 20px;
  cursor: pointer;
  transition: background-color 0.3s;
  height: 35px;
}

.sidebar li:hover {
  background-color: #34495e;
}

.sidebar span {
  display: none;
  transition: width 0.3s ease;
  white-space: nowrap;
}

.sidebar.expanded span {
  display: inline;
}

.toggle-btn {
  margin: 0 10px 0 0;
  padding: 10px;
  background: none;
  border: none;
  color: white;
  cursor: pointer;
  font-size: 20px;
  display: flex;
  justify-content: center;
  align-items: center;
  width: 40px;
  height: 40px;
  transition: color 0.3s, margin 0.3s;
}

.sidebar.expanded .toggle-btn {
  color: #409eff;
  justify-content: flex-end;
}

.divider {
  width: 50%;
  height: 1px;
  background-color: #ffffff;
  margin: 10px auto;
  transition: width 0.3s ease;
}

.logo-container {
  display: flex;
  justify-content: left;
  align-items: left;
  height: 55px;
  padding: 10px;
  width: 100%;
}

.logo {
  transition: transform 0.3s ease;
  width: 40px;
}

.tituloSistema {
  display: flex;
  font-weight: bold;
  font-size: 1.5rem;
  padding-left: 10px;
}
</style>
