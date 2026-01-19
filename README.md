# BankNode – P2P Bank Node (C# Console Application)

This project implements a **bank node** based on the **P2P (peer-to-peer) architectural concept**.
Each node represents a single bank that allows clients to create bank accounts, deposit and withdraw money,
and retrieve information about the bank state.

The application is implemented as a **pure C# console application** without using any frameworks
(such as ASP.NET, Entity Framework, or MVC frameworks).
Communication is handled via **TCP sockets**, and persistent data storage is provided by a **MySQL database**.

> ⚠️ Peer-to-peer communication between multiple banks is not implemented yet.
> The current version represents a standalone bank node.

---

## Project Architecture

The project follows **MVC principles implemented manually** (without frameworks).

BankNode/
├── Config/ # Application configuration (config.json)
├── Controllers/ # Command parsing and routing
├── Services/ # Business logic (bank operations)
├── Data/ # Database access layer (MySQL)
├── Networking/ # TCP server
├── Program.cs # Application entry point
└── BankNode.csproj

### Layer responsibilities
- **Controller** – parses and validates incoming commands
- **Service** – contains business logic (bank rules)
- **Repository / Data layer** – database communication
- **Networking** – TCP communication with clients

---

## Database (MySQL)

### Used technology
- **MySQL**
- **MySqlConnector (ADO.NET driver)**

### Database schema
```sql
CREATE DATABASE banknode;
USE banknode;

CREATE TABLE accounts (
    account_id VARCHAR(64) PRIMARY KEY,
    balance INT NOT NULL,
    created_at DATETIME NOT NULL
);
