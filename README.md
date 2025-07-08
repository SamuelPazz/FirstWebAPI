# ✅ To Do API

## 📌 Visão Geral
Uma API REST desenvolvida em C# com .NET, para gerenciamento de tarefas. Permite o cadastro de usuários, autenticação e criação de tarefas, com validações e testes automatizados.

---

## 📝 Funcionalidades

- ✅ Cadastro, login e gerenciamento de usuários.
- 🗂️ Criação, listagem, atualização e exclusão de tarefas.
- 🔒 Autenticação baseada em token JWT.
- 🧪 Testes unitários com xUnit

---

## ⚙️ Tecnologias Utilizadas

- **C# 13** – Linguagem principal.
- **.NET 9.0** – Framework da aplicação.
- **PostgreSQL** – Banco de dados.
- **Swagger** – Interface interativa da documentação da API.
- **JWT** – Autenticação baseada em token.

---

## 🛠️ Setup

### ✅ Pré-requisitos
- .NET 9.0
- PostgreSQL

### 📦 Instalação

1. Clone o repositório:
   ```bash
   git clone https://github.com/SamuelPazz/FirstWebAPI.git
   ```

2. Acesse o diretório do projeto:
   ```bash
   cd ToDoApp
   ```
   
---

## ⚙️ Configuração

Crie um banco de dados PostgreSQL e configure o arquivo appsettings.json com os dados do seu banco de dados PostgreSQL e as configurações de token JWT.

---

## 🚀 Execução

Execute o projeto com:

```bash
dotnet run
```

Ou inicie o projeto `ToDoApp` pela sua IDE.

---

## 🧪 Como Testar

### 🔹 1. Swagger UI
- Acesse: [https://localhost:7237/swagger/index.html](https://localhost:7237/swagger/index.html)
- Explore e envie requisições diretamente via navegador.

### 🔹 2. Postman ou Insomnia
- Apenas os endpoints de **cadastro de usuário** e **login** não exigem autenticação.
- Para os demais, gere um token JWT e envie no cabeçalho:

```http
Authorization: <seu_token>
```

> ⚠️ **Importante:** envie apenas o token, **sem** o prefixo `Bearer`.

### 🔹 3. Projeto de testes: ToDoAppTest
- Projeto com testes unitários das funcionalidades do projeto.