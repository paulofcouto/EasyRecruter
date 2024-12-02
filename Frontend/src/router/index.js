// src/router/index.js
import { createRouter, createWebHistory } from 'vue-router';

import Login from '@/views/Login.vue';
import Recrutamento from '@/views/Recrutamento.vue';
import Sobre from '@/views/Sobre.vue';
import Sugestoes from '@/views/Sugestoes.vue';

const routes = [
  {
    path: '/',
    redirect: '/recrutamento',
  },
  {
    path: '/login',
    name: 'Login',
    component: Login,
    meta: { showMenu: false, showHeader: false  },
  },
  {
    path: '/recrutamento',
    name: 'Recrutamento',
    component: Recrutamento,
    meta: { showMenu: true, showHeader: true, title: 'Recrutamento' },
  },
  {
    path: "/sobre",
    name: "Sobre",
    component: Sobre,
    meta: { showMenu: true, showHeader: true, title: 'Sobre'  },
  },
  {
    path: "/sugestoes",
    name: "Sugestões",
    component: Sugestoes,
    meta: { showMenu: true, showHeader: true, title: 'Sugestões'   },
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

router.beforeEach((to, from, next) => {
  const isAuthenticated = sessionStorage.getItem('authToken') !== null;

  if (to.path != '/login' && !isAuthenticated) {
    next('/login');
  } else if (to.path === '/login' && isAuthenticated) {
    next('/recrutamento');
  } else {
    next();
  }
});

export default router;
