import { fileURLToPath, URL } from 'node:url'
import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

export default defineConfig({
  plugins: [
    vue(),
  ],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  },
  server: {
    proxy: {
      '/api': {
        target: 'https://localhost:8000', // URL do seu backend
        changeOrigin: true, // Necessário para CORS
        secure: false, // Se o backend usa HTTPS com certificado auto-assinado
      },
    },
  },
})
