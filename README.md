# 🏦 Wallet Service

This is a simple RESTful API for managing customer wallets in a sports betting platform. It supports:

* Creating wallets
* Depositing and withdrawing funds
* Enforcing single wallet per currency per user
* Ensuring wallet balances never go negative
* Preventing duplicate transactions

---

## ✨ Features

* RESTful API built on ASP.NET Web API (.NET Framework 4.8)
* Entity Framework 6 with SQL Server
* Dependency Injection using Unity
* Repository + Unit of Work pattern
* Swagger UI for API documentation
* Dockerized SQL Server for local development

---

## 💪 Prerequisites

* [.NET Framework 4.8 Developer Pack](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48)
* [Visual Studio 2019 or 2022](https://visualstudio.microsoft.com/)
* [Docker Desktop](https://www.docker.com/products/docker-desktop/) (Windows containers enabled)
* [SQL Server Management Studio (optional)](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)

---

## 📆 Project Structure

```
WalletService/
├── Api/                        # Web API project
│   ├── Controllers/            # REST API endpoints
│   ├── DependencyInjection/    # Unity DI setup
│   ├── App_Start/              # Swagger and route configs
│   └── Global.asax.cs          # Application startup
│
├── Business/                   # Core domain logic
│   ├── Entities/               # Wallet and transaction models
│   ├── Interfaces/             # Service and repository contracts
│   └── Services/               # Business logic layer
│
├── Data/                       # Persistence layer
│   ├── Context/                # EF DbContext
│   └── Repository/             # Repositories + UnitOfWork
│
├── docker-compose.yml          # Docker for SQL Server only
├── WalletService.sln           # Visual Studio solution
└── README.md
```

---

## 🚣 Running SQL Server with Docker

To run the required SQL Server database locally:

> The `docker-compose.yml` file is included in the repository.

### Commands

```bash
docker-compose up -d       # Start SQL Server

docker-compose down        # Stop and clean up
```

---


## 📈 Swagger API Documentation

Once the API is running, navigate to:

```
http://localhost:<your-port>/swagger
```

---

## 📅 API Endpoints

| Endpoint                   | Method | Description           |
| -------------------------- | ------ | --------------------- |
| `/api/wallet`              | POST   | Create a new wallet   |
| `/api/wallet/{id}/add`     | POST   | Add funds to a wallet |
| `/api/wallet/{id}/balance` | PATCH  | Withdraw funds        |
| `/api/wallet/{id}`         | GET    | Get wallet balance    |


