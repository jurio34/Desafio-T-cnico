#  Sistema de Reserva de Salas

Aplicaçao Fullstack para gerenciamento e agendamento de salas de reunião, com validação de colisão de horários e agrupamento cronológico de reservas.

---

##  Tecnologias Utilizadas

**Backend:**
* C# / .NET 8
* Entity Framework Core
* SQLite

**Frontend:**
* React (TypeScript)
* Axios
* CSS Modules / CSS Nativo

---

##  Como Rodar o Projeto

### Pré-requisitos
* [.NET 8 SDK](https://dotnet.microsoft.com/)
* [Node.js](https://nodejs.org/) (versão 18 ou superior)

---

### 1. Configurando o Backend

```bash
# Entre na pasta do backend
cd backend

# Restaure as dependências e rode a API
dotnet restore
dotnet run
```
---
### 2. Configurando o Frontend

# Em outro terminal, entre na pasta do frontend
```bash 
cd frontend

# Instale as dependências
npm install

# Inicie o servidor de desenvolvimento
npm run dev 
```
---

### Por quê foi feita a escolha do Hard Delete(Exclusão do banco de Dados):
# - Como decisão arquitetural, essa escolha foi baseada no escopo do desafio. Como o minimundo do banco de dados é pequeno, isso acaba por não afetar a performance executando uma query de deleção, mas em cenários de produção o soft delete é indicado pela sua observabilidade, mantendo logs em caso de auditoria e etc.
