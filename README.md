# CadastroDeClienteMuralis

API REST para cadastro e gerenciamento de clientes, com integração ViaCEP para busca de endereço e autenticação via JWT.

---

## 🏗️ Arquitetura do Projeto

- **Camadas:**

  - `CadastroDeClienteMuralis.Dominio` → Entidades, Regras de Negócio, Interfaces, Validações de Domínio.
  - `CadastroDeClienteMuralis.Aplicacao` → Serviços, DTOs, Mapeamento, Validators, Interfaces de Aplicação.
  - `CadastroDeClienteMuralis.Infra` → Persistência, Contexto, Repositórios, Consumo de APIs externas (ViaCEP).
  - `CadastroDeClienteMuralis.WebApi` → Camada de API, Controllers, Autenticação e Configurações.

- **Padrões Aplicados:**
  - Clean Architecture
  - Repository Pattern
  - CQRS com MediatR
  - FluentValidation
  - AutoMapper
  - SOLID

---

## 🔒 Autenticação

- Autenticação via **JWT Token**
- Existe um endpoint para gerar um token de acesso
- Token simulado (hardcoded), sem usuários no banco.

**Exemplo de configuração no `appsettings.json`:**

```json
"JwtSettings": {
  "SecretKey": "ChaveSuperSecretaMuralis123456789!",
  "Issuer": "CadastroDeClienteAPI",
  "Audience": "CadastroDeClienteAPIUsers"
}
```

> 🔥 **Importante:** A `SecretKey` precisa ter no mínimo 16 caracteres para o algoritmo HS256.

---

## 📦 Funcionalidades

- ✅ CRUD de Clientes
- ✅ Cadastro de Endereço com integração à API ViaCEP
- ✅ Validação de CEP
- ✅ Validação de duplicidade (não permite cadastrar cliente com mesmo Nome + CEP)
- ✅ CRUD de Contatos
- ✅ Autenticação com JWT
- ✅ Validação com FluentValidation
- ✅ Logs estruturados com ILogger
- ✅ Tratamento de erros centralizado
- ✅ Documentação Swagger

---

## ✅ Validações Implementadas

- ✔️ Nome do Cliente obrigatório
- ✔️ Id da Entidade Cliente em Guid para maior segurança
- ✔️ Endereço:
  - CEP obrigatório
  - Preenchimento automático de `Logradouro` e `Cidade` via ViaCEP
- ✔️ Não permite cadastrar clientes com mesmo `Nome` e `CEP`
- ✔️ Atualização de contatos garante que os IDs informados existam para aquele cliente
- ✔️ Tratamento elegante para erros no ViaCEP

---

## 🔧 Configuração do Banco

### 📄 `appsettings.json` (no projeto WebApi):

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=SEUSERVER;Database=CadastroDeClienteDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

> 🎯 Altere conforme seu ambiente (localhost, instância ou SQL Server remoto).

---

## 🚀 Executando o Projeto

### 🔗 Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- SQL Server Local ou remoto

---

### 🗄️ Aplicando as Migrations

1. No Visual Studio:  
   **Tools > NuGet Package Manager > Package Manager Console**

2. Com o projeto `CadastroDeClienteMuralis.Infra` selecionado como **Default Project**, execute:

```powershell
Update-Database
```

✅ Isso criará o banco e todas as tabelas necessárias.

---

### ▶️ Rodando a API

Via Visual Studio ou terminal:

```bash
dotnet run --project CadastroDeClienteMuralis.WebApi
```

---

## 🔐 Obtendo o Token

### 🔸 Endpoint:

```
POST /api/auth/login
```

> Não é necessário enviar body. O endpoint gera um token válido.

### 🔸 Retorno:

```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR..."
}
```

### 🔸 Usar no Swagger:

Clique em **Authorize** no Swagger UI e cole:

```
Bearer {seu_token}
```

---

## 📑 Endpoints Principais

| Método | Rota               | Descrição                |
| ------ | ------------------ | ------------------------ |
| POST   | /api/auth/login    | Obter Token              |
| POST   | /api/clientes      | Criar Cliente            |
| GET    | /api/clientes/{id} | Buscar Cliente por ID    |
| GET    | /api/clientes      | Listar Todos os Clientes |
| PUT    | /api/clientes      | Atualizar Cliente        |
| DELETE | /api/clientes/{id} | Deletar Cliente          |

---

## 🛠️ Tecnologias

- ASP.NET Core 8
- Entity Framework Core
- SQL Server
- AutoMapper
- MediatR
- FluentValidation
- Swagger (Swashbuckle)
- JWT Authentication
- ILogger (Logs)

---

## 🧠 Como Testar

1. Clone o projeto:

```bash
git clone https://github.com/....
```

2. Configure `appsettings.json` (conexão e chave JWT).

3. Execute o comando no **Package Manager Console**:

```powershell
Update-Database
```

4. Rode o projeto:

```bash
dotnet run --project CadastroDeClienteMuralis.WebApi
```

5. Acesse:

```
https://localhost:44391/swagger/index.html
```

6. Gere o token no endpoint `/api/auth/login` e clique em **Authorize** no Swagger.

7. Use os endpoints para testar CRUD, ViaCEP e autenticação.

---

## 👨‍💻 Autor

Feito por **Bruno Tomm**

---
