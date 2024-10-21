import { createApp } from 'vue'
import App from './App.vue'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
//import router from './router' // se voc� estiver usando Vue Router
//import { createPinia } from 'pinia' // se voc� estiver usando Pinia

const app = createApp(App)
app.use(ElementPlus)
//app.use(router) // se voc� estiver usando Vue Router
//app.use(createPinia()) // se voc� estiver usando Pinia
app.mount('#app')
