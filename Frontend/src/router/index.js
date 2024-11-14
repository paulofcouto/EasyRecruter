// src/router/index.js
import { createRouter, createWebHistory } from 'vue-router';

import Login from '@/views/Login.vue';
import Recrutamento from '@/views/Recrutamento.vue';

const routes = [
  {
    path: '/',
    redirect: '/recrutamento',
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  },
  {
    path: '/recrutamento',
    name: 'Recrutamento',
    component: Recrutamento
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

router.beforeEach((to, from, next) => {
  const isAuthenticated = sessionStorage.getItem('authToken') !== null;

  if (to.path === '/recrutamento' && !isAuthenticated) {
    next('/login');
  } else if (to.path === '/login' && isAuthenticated) {
    next('/recrutamento');
  } else {
    next();
  }
});

export default router;
