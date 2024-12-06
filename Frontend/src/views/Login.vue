<template>
    <div class="login-container">
        <div class="login-content">
            <div class="login-form">
                <img src="@/assets/logo.png" alt="Logo" class="login-logo" />
                <h1 class="sistema-title"><a style="color:#0076df">Easy</a> Recruter</h1>
                <h3 class="login-title">{{ isRegister ? "Crie sua conta" : "Seja bem-vindo" }}</h3>
                <el-form :model="loginForm" @submit.prevent="isRegister ? handleRegister : handleLogin">
                    <el-form-item>
                        <el-input v-model="loginForm.email" placeholder="Inserir seu email" prefix-icon="el-icon-user"></el-input>
                    </el-form-item>
                    <el-form-item>
                        <el-input type="password" v-model="loginForm.senha" placeholder="Insira sua senha" prefix-icon="el-icon-lock"></el-input>
                    </el-form-item>
                    <el-form-item v-if="isRegister">
                        <el-input type="password" v-model="loginForm.confirmSenha" placeholder="Confirme sua senha" prefix-icon="el-icon-lock"></el-input>
                    </el-form-item>
                    <el-form-item class="login-button-container">
                        <template v-if="isRegister">
                            <el-button type="primary" @click="handleRegister" class="login-button">
                                Cadastrar
                            </el-button>
                        </template>
                        <template v-else>
                            <el-button type="primary" @click="handleLogin" class="login-button">
                                Entrar
                            </el-button>
                        </template>
                    </el-form-item>
                </el-form>
                <el-link v-if="!isRegister" href="#" class="forgot-password">Esqueceu sua senha?</el-link>
            </div>

            <!-- Card de Cadastro -->
            <div class="register-card">
                <p>
                    {{ isRegister ? "Já possui uma conta?" : "Não tem uma conta?" }}
                    <el-link href="#" class="register-link" @click.prevent="toggleForm">
                        {{ isRegister ? "Faça login" : "Cadastre-se" }}
                    </el-link>
                </p>
            </div>
        </div>
    </div>
</template>

<script>
import { login, cadastrar } from '@/services/authServices.js';

export default {
    data() {
        return {
            isRegister: false,
            loginForm: {
                email: '',
                senha: '',
                confirmSenha: '',
                rememberMe: false,
            },
        };
    },
    methods: {
        async handleLogin() {
            try {
                const response = await login(this.loginForm.email, this.loginForm.senha);
                
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
                    
                    //4. Redirecionar ou fazer algo após o login bem-sucedido
                    this.$router.push('/recrutamento');
                }
            } catch (error) {
                alert('Usuário ou senha inválidos.');
            }
        },
        async handleRegister() {
            if (this.loginForm.senha !== this.loginForm.confirmSenha) {
                alert("As senhas não coincidem.");
                this.loginForm.senha = "";
                this.loginForm.confirmSenha = "";
                return;
            }

            try {
                const response = await cadastrar(this.loginForm.email, this.loginForm.senha);
                alert('Cadastro realizado com sucesso! Faça login para continuar.');
                this.toggleForm();
            } catch (error) {
                alert('Erro ao realizar o cadastro. Verifique os dados e tente novamente.');
            }
        },
        toggleForm() {
            this.isRegister = !this.isRegister;
            this.resetForm();
        },
        resetForm() {
            this.loginForm.email = "";
            this.loginForm.senha = "";
            this.loginForm.confirmSenha = "";
        },
    },
};
</script>

<style scoped>
.login-container {
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100vh;
    width: 100%;
    background-color: #f4f6f8;

    .login-content {
        display: flex;
        flex-direction: column;
        align-items: center;
        width: 100%;
        max-width: 400px;

        .login-form,
        .register-card {
            background: #fff;
            padding: 20px;
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

        .sistema-title {
            font-size: 24px;
        }

        .login-title {
            margin-bottom: 20px;
            color: #333;
        }

        .login-button-container {
            display: flex;
            justify-content: center;

            .login-button {
                width: 100%;
            }
        }

        .forgot-password,
        .change-password {
            display: block;
            margin-top: 10px;
            color: #007bff;
        }
    }

    .register-card {
        margin-top: 20px;
        background: #f9f9f9;

        p {
            margin: 0;
            font-size: 14px;
            color: #333;

            .register-link {
                color: #007bff;
                font-weight: bold;
                font-size: 14px;
            }
        }
    }
}
</style>
