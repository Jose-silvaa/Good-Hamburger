# 🍔 Good Hamburger

API REST desenvolvida em **ASP.NET Core (C#)** para gerenciamento de uma hamburgueria — pedidos, hambúrgueres, bebidas e acompanhamentos. O projeto é containerizado com **Docker** e documentado via **Swagger**.

---

## 🚀 Demo / API Online

+A aplicação está publicada no Render e pode ser acessada nos links abaixo:

🔗 **Swagger:** [https://good-hamburger-5-0.onrender.com/swagger/index.html](https://good-hamburger-5-0.onrender.com/swagger/index.html)

 🔗 **Frontend (aplicação web):** [https://good-hamburger-5-0.onrender.com](https://good-hamburger-5-0.onrender.com)

> ⚠️ Por se tratar de uma hospedagem gratuita (Render), a primeira requisição pode demorar alguns segundos enquanto a aplicação "acorda".

---

## 🛠️ Tecnologias Utilizadas

- [.NET / ASP.NET Core](https://dotnet.microsoft.com/) — API REST
- **C#** — linguagem principal
- **Entity Framework Core** — ORM e migrations
- **Swagger / OpenAPI** — documentação da API
- **Docker** — containerização
- **Render** — deploy em nuvem

---

## 📁 Estrutura do Projeto

```
Good-Hamburger/
├── Components/      # Componentes reutilizáveis
├── Data/            # Contexto do banco e acesso a dados
├── Features/        # Módulos por funcionalidade
├── Migrations/      # Migrations do Entity Framework
├── Properties/      # Configurações do projeto
├── Shared/          # Recursos compartilhados
├── wwwroot/         # Arquivos estáticos
├── Program.cs       # Ponto de entrada da aplicação
├── appsettings.json # Configurações da aplicação
└── Dockerfile       # Imagem Docker
```

---

## 📋 Pré-requisitos

Escolha uma das opções abaixo:

**Opção A — Rodar localmente com .NET**
- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
- [Git](https://git-scm.com/)

**Opção B — Rodar via Docker**
- [Docker](https://www.docker.com/products/docker-desktop/)
- [Git](https://git-scm.com/)

---

## 💻 Como clonar e rodar o projeto

### 1. Clonar o repositório

```bash
git clone https://github.com/Jose-silvaa/Good-Hamburger.git
cd Good-Hamburger
```

---

### 2. Rodando localmente (sem Docker)

Restaure as dependências e execute:

```bash
dotnet restore
dotnet build
dotnet run
```

Acesse no navegador:

```
http://localhost:5000/swagger
```

> A porta pode variar conforme o `launchSettings.json`. Verifique a saída do console.

Para aplicar as migrations ao banco:

```bash
dotnet ef database update
```

---

### 3. Rodando com Docker (recomendado) 🐳

#### Baixar a imagem pronta do Docker Hub

```bash
docker pull vitor590/good-hamburger:tagname
```

#### Executar o container

```bash
docker run -d -p 8080:8080 --name good-hamburger vitor590/good-hamburger:tagname
```

Acesse:

```
http://localhost:8080/swagger
```

#### (Opcional) Fazer build da imagem local

Se preferir construir sua própria imagem a partir do código-fonte:

```bash
docker build -t good-hamburger:local .
docker run -d -p 8080:8080 good-hamburger:local
```

#### (Opcional) Publicar nova versão no Docker Hub

```bash
docker tag good-hamburger:local vitor590/good-hamburger:tagname
docker push vitor590/good-hamburger:tagname
```

---

## 📖 Documentação da API

Todas as rotas disponíveis podem ser exploradas e testadas diretamente pelo Swagger:

- **Local:** `http://localhost:8080/swagger`
- **Produção:** [https://good-hamburger-5-0.onrender.com/swagger/index.html](https://good-hamburger-5-0.onrender.com/swagger/index.html)

## 👨‍💻 Autor

Desenvolvido por [**José Silva**](https://github.com/Jose-silvaa)
