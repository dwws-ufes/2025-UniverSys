# 2025-UniverSys

Assignment for the 2025 edition of the "Web Development and the Semantic Web" course, by Pedro Antônio Rosa de Souza and Thyago Vieira Piske

## 📋 Sobre o Projeto

UniverSys é um sistema de gestão universitária desenvolvido com:
- **Backend**: .NET 8.0 Web API com Entity Framework Core
- **Frontend**: Angular 19 com ng-zorro-antd
- **Banco de dados**: MySQL 8.0

## 🚀 Como executar o projeto

### Pré-requisitos
- [Docker](https://www.docker.com/get-started) instalado
- [Docker Compose](https://docs.docker.com/compose/install/) instalado

### Executando com Docker

1. **Clone o repositório**:
   ```bash
   git clone https://github.com/dwws-ufes/2025-UniverSys.git
   cd 2025-UniverSys
   ```

2. **Execute a aplicação**:
   ```bash
   docker-compose up --build
   ```

3. **Acesse a aplicação**:
   - http://localhost:5000
   - entre com um dos seguintes logins: admin, professor ou aluno, todos com senha "teste". 
   

### Comandos úteis

- **Parar a aplicação**:
  ```bash
  docker-compose down
  ```

## 🛠️ Tecnologias utilizadas

### Backend
- .NET 8.0 Web API
- Entity Framework Core 8.0
- MySQL
- AutoMapper
- FluentValidation
- MediatR (CQRS pattern)
- JWT Authentication

### Frontend
- Angular 19
- ng-zorro-antd (Ant Design)
- TypeScript
- RxJS

### DevOps
- Docker & Docker Compose
- MySQL 8.0
- Nginx (para servir frontend)
