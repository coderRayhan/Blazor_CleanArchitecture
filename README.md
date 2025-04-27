# 🚀 # Clean Architecture With .NET 9 Blazor Server - Enterprise-Grade Solution

[![.NET](https://img.shields.io/badge/.NET-9.0-blueviolet)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)
[![Architecture](https://img.shields.io/badge/architecture-Clean%20Architecture-brightgreen)](https://)
[![DDD](https://img.shields.io/badge/pattern-DDD%20%7C%20CQRS-blue)](https://)

**A production-ready Blazor Server application** designed using Clean Architecture principles, featuring a sophisticated user interface. The application is built on .NET 9, which brings enhanced performance, improved developer productivity, and new features that make the development process smoother. Blazor Server, now supported on .NET 9, combines the power of C# with a modern web development experience, eliminating the need to switch between languages like JavaScript and C#. This setup simplifies development and enables fast, responsive, and highly interactive web applications. Leveraging Blazor’s real-time communication capabilities and .NET’s robust ecosystem, developers can rapidly create feature-rich, scalable applications with a seamless user experience.

## 🌟 Key Features

### 🏗️ Foundation
- **Clean Architecture** with Domain/Application/Infrastructure/Web layers
- **Domain-Driven Design** with modular business capabilities
- **CQRS Pattern** (Dapper + EF Core)
- Transactional Outbox with Hangfire

### 🛡️ Security First
- Role & Permission-based Authorization

### ⚡ Modern Tooling
- MediatR Pipeline Behaviors:
  - Caching
  - Validation & Error Handling (RFC Standards)
  - Performance Tracking
- Real-time Notifications via SignalR

### Supported Databases

* PostgreSQL (Provider Name: `postgresql`)
* Microsoft SQL Server (Provider Name: `mssql`)
* SQLite (Provider Name: `sqlite`)

### How to select a specific Database?

1. Open the `appsettings.json` file located in the src directory of the `Server.UI` project.
2. Change the setting `DBProvider` to the desired provider name (See Supported Databases).
3. Change the `ConnectionString` to a connection string, which works for your selected database provider.

### 📈 Observability
- Structured logging throughout
- Monitoring-ready architecture

## 🛠️ Getting Started

### Prerequisites
- .NET 9 SDK
- PostgreSQL/MSSQL Server

🏭 Solution Structure

📁 src/ \
├─ 📁 Domain/ - Core business models \
├─ 📁 Application/ - Use cases & business logic \
├─ 📁 Infrastructure/ - External implementations \
└─ 📁 UI/ - Blazor Server & DI \

###🚦 Quality Assurance
- RFC-compliant error responses
- FluentValidation integration
- Transactional consistency guarantees
- Centralized package management
- CI/CD-ready configuration



