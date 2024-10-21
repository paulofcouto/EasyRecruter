import { createApp } from 'vue'
import App from './App.vue'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
//import router from './router' // se você estiver usando Vue Router
//import { createPinia } from 'pinia' // se você estiver usando Pinia

const app = createApp(App)
app.use(ElementPlus)
//app.use(router) // se você estiver usando Vue Router
//app.use(createPinia()) // se você estiver usando Pinia
app.mount('#app')
