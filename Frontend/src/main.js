import { createApp } from 'vue';
import App from './App.vue';
import ElementPlus from 'element-plus';
import 'element-plus/dist/index.css';
import router from './router'; // Importa o roteador centralizado

const app = createApp(App);
app.use(ElementPlus);
app.use(router); // Usa o roteador centralizado
app.mount('#app');
