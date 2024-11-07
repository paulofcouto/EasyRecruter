<template>
    <div class="login-container">
        <div class="login-form">
            <img src="@/assets/logo.png" alt="Logo" class="login-logo" />
            <h1><a style="color:#0076df">Easy</a> Recruter</h1>
            <h3 class="login-title">Seja bem-vindo</h3>
            <el-form :model="loginForm" @submit.prevent="handleLogin">
                <el-form-item>
                    <el-input v-model="loginForm.email" placeholder="Inserir seu email" prefix-icon="el-icon-user"></el-input>
                </el-form-item>
                <el-form-item>
                    <el-input type="password" v-model="loginForm.senha" placeholder="Insira sua senha" prefix-icon="el-icon-lock"></el-input>
                </el-form-item>
                <el-form-item class="login-button-container">
                    <el-button type="primary" @click="handleLogin" class="login-button">Entrar</el-button>
                </el-form-item>
            </el-form>
            <el-link href="#" class="forgot-password">Esqueceu sua senha?</el-link>
        </div>
    </div>
</template>

<script>
    import { login } from '@/services/auth.js';

    export default {
        data() {
            return {
                loginForm: {
                    email: '',
                    senha: '',
                    rememberMe: false,
                },
            };
        },
        methods: {
            async handleLogin() {
                try {
                    const response = await login(this.loginForm.email, this.loginForm.senha);
                    console.log(response);
                    
                    if (response.data.token) {
                        // 1. Armazena o token na sessionStorage
                        sessionStorage.setItem('authToken', response.data.token);
                        
                        // 2. Envia o token para a extensão salvar no chrome.storage.local
                        //console.log("Tentando enviar mensagem para salvar o token na extensão");
                        //chrome.runtime.sendMessage(
                        //    {
                        //        action: 'saveToken',
                        //        token: response.data.token,
                        //    },
                        //    (response) => {
                        //        if (response && response.status === 'sucesso') {
                        //            console.log("Token salvo no chrome.storage.local pela extensão");
                        //        } else {
                        //            console.error("Erro ao salvar token na extensão");
                        //        }
                        //    }
                        //);
                        
                        // 3. Feedback ao usuário
                        alert('Login realizado com sucesso!');
                        
                        // Redirecionar ou fazer algo após o login bem-sucedido
                        // Exemplo: this.$router.push('/dashboard');
                    }
                } catch (error) {
                    console.error('Erro ao fazer login:', error);
                    alert('Usuário ou senha inválidos.');
                }
            },
        }


    };
</script>

<style>
    body {
        margin: 0;
        background: #f0f2f5;
        display: flex;
        justify-content: center;
        align-items: center;
        height: 100vh;
    }

    .login-container {
        display: flex;
        justify-content: center;
        align-items: center;
        height: 100vh;
        width: 100%;
    }

    .login-form {
        background: #fff;
        padding: 40px 20px;
        border-radius: 10px;
        box-shadow: 0 2px 12px rgba(0, 0, 0, 0.1);
        text-align: center;
        width: 100%;
        max-width: 400px;
    }

    .login-logo {
        width: 150px;
        margin-bottom: 20px;
    }

    .login-title {
        margin-bottom: 20px;
        color: #333;
        font-size: 24px;
    }

    .login-button-container {
        display: flex;
        justify-content: center;
    }

    .login-button {
        width: 100%;
    }

    .forgot-password,
    .change-password {
        display: block;
        margin-top: 10px;
    }

    .forgot-password {
        color: #007bff;
    }

    .change-password {
        color: #007bff;
    }
</style>
