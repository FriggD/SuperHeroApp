# Aplicação CRUD de Super-heróis

Uma aplicação full-stack para gerenciamento de super-heróis e seus superpoderes.

## Sobre a Aplicação

O SuperheroApp é uma aplicação completa que permite aos usuários gerenciar um catálogo de super-heróis e seus respectivos superpoderes. A aplicação oferece funcionalidades CRUD (Criar, Ler, Atualizar e Deletar) para heróis e superpoderes, com uma interface de usuário intuitiva e uma API RESTful robusta.

## Tecnologias Utilizadas

### Backend
- **.NET 8**: Framework para desenvolvimento da API Web
- **ASP.NET Core Web API**: Para criação de endpoints RESTful
- **Entity Framework Core**: ORM (Object-Relational Mapping) para acesso ao banco de dados
- **Arquitetura em Camadas**: Separação em camadas API, Core e Infrastructure
- **Swagger/OpenAPI**: Documentação interativa da API

### Frontend
- **Angular 17**: Framework para desenvolvimento da interface de usuário
- **TypeScript**: Linguagem de programação tipada
- **Bootstrap 5**: Framework CSS para design responsivo
- **RxJS**: Biblioteca para programação reativa
- **ng-select**: Componente para seleções avançadas

### Banco de Dados
- **PostgreSQL 16**: Sistema de gerenciamento de banco de dados relacional

### DevOps e Containerização
- **Docker**: Plataforma de containerização
- **Docker Compose**: Orquestração de múltiplos containers

## Estrutura do Projeto

- **SuperheroApp.API**: Projeto da API Web com controllers e configurações
- **SuperheroApp.Core**: Modelos de domínio, DTOs e interfaces
- **SuperheroApp.Infrastructure**: Acesso a dados, repositórios e implementações
- **client**: Aplicação frontend em Angular

## Modelo de Dados

### Heróis (Heroes)
- **Id**: Identificador único
- **Name**: Nome real do herói
- **HeroName**: Nome de super-herói
- **DateOfBirth**: Data de nascimento
- **Height**: Altura
- **Weight**: Peso
- **Description**: Descrição

### Superpoderes (Superpowers)
- **Id**: Identificador único
- **Name**: Nome do superpoder
- **Description**: Descrição do superpoder

### Relação Herói-Superpoder (HeroSuperpowers)
- Tabela de junção para o relacionamento muitos-para-muitos entre Heróis e Superpoderes

## API RESTful

### Endpoints de Heróis

#### GET /api/heroes
- **Descrição**: Obtém todos os heróis
- **Resposta**: 200 OK (Lista de heróis) ou 204 No Content (Se não houver heróis)

#### GET /api/heroes/{id}
- **Descrição**: Obtém um herói específico pelo ID
- **Resposta**: 200 OK (Herói) ou 404 Not Found (Se o herói não for encontrado)

#### POST /api/heroes
- **Descrição**: Cria um novo herói
- **Corpo da Requisição**: Dados do herói
- **Resposta**: 201 Created (Herói criado) ou 400 Bad Request (Se um herói com o mesmo nome já existir)

#### PUT /api/heroes/{id}
- **Descrição**: Atualiza um herói existente
- **Corpo da Requisição**: Dados atualizados do herói
- **Resposta**: 204 No Content (Sucesso) ou 404 Not Found (Se o herói não for encontrado)

#### DELETE /api/heroes/{id}
- **Descrição**: Remove um herói
- **Resposta**: 204 No Content (Sucesso) ou 404 Not Found (Se o herói não for encontrado)

### Endpoints de Superpoderes

#### GET /api/superpowers
- **Descrição**: Obtém todos os superpoderes
- **Resposta**: 200 OK (Lista de superpoderes) ou 204 No Content (Se não houver superpoderes)

#### GET /api/superpowers/{id}
- **Descrição**: Obtém um superpoder específico pelo ID
- **Resposta**: 200 OK (Superpoder) ou 404 Not Found (Se o superpoder não for encontrado)

#### POST /api/superpowers
- **Descrição**: Cria um novo superpoder
- **Corpo da Requisição**: Dados do superpoder
- **Resposta**: 201 Created (Superpoder criado)

#### PUT /api/superpowers/{id}
- **Descrição**: Atualiza um superpoder existente
- **Corpo da Requisição**: Dados atualizados do superpoder
- **Resposta**: 204 No Content (Sucesso) ou 404 Not Found (Se o superpoder não for encontrado)

#### DELETE /api/superpowers/{id}
- **Descrição**: Remove um superpoder
- **Resposta**: 204 No Content (Sucesso) ou 404 Not Found (Se o superpoder não for encontrado)

## Como Iniciar a Aplicação

### Pré-requisitos

- Docker e Docker Compose
- .NET 8 SDK (para desenvolvimento sem Docker)
- Node.js e npm (para desenvolvimento sem Docker)

### Executando com Docker

```bash
docker-compose up
```

A aplicação estará disponível em:
- API: http://localhost:5000
- Frontend: http://localhost:4200
- PostgreSQL: localhost:5432

### Desenvolvimento Local

#### Backend (.NET)

```bash
cd server/SuperheroApp.API
dotnet run
```

#### Frontend (Angular)

```bash
cd client
npm install
ng serve
```

## Funcionalidades Principais

- Cadastro, visualização, edição e remoção de super-heróis
- Cadastro, visualização, edição e remoção de superpoderes
- Associação de múltiplos superpoderes a um herói
- Interface de usuário responsiva e amigável
- API RESTful documentada com Swagger