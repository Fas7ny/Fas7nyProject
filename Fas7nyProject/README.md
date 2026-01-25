# 🧱 Fas7nyProject – Clean Architecture

This project is built using **Clean Architecture** principles to ensure a clear separation of concerns, high maintainability, testability, and scalability.
It follows modern backend best practices using **ASP.NET Core**, **CQRS**, **Repository Pattern**, and **Unit of Work**.

---

## 🏗️ Project Architecture Overview

The solution is organized into **four main layers**, each with a specific responsibility:

```
Presentation
 └── Application
      └── Domain
 └── Infrastructure
```

---

## 📁 Solution Structure

```
Fas7nyProject
│
├── Application
│   ├── CQRS
│   │   ├── Commands        // Write operations (Create, Update, Delete)
│   │   └── Queries         // Read-only operations (Get, Search, Filter)
│   ├── DTOs                // Request & Response DTOs
│   ├── Services
│   └── ServicesInterfaces
│
├── Domain
│   ├── Entities            // Core business entities
│   ├── Enums               // Domain enums
│   └── RepoInterfaces      // Repository & UnitOfWork interfaces
│
├── Infrastructure
│   ├── Data                // DbContext, Configurations, SeedData
│   ├── Migrations          // EF Core migrations
│   └── Repo                // Repository & UnitOfWork implementations
│
├── Presentation
│   └── Controllers         // API Controllers
│
└── Program.cs              // Application entry point
```

---

## 🧱 Layer Responsibilities

### 1️⃣ Presentation Layer

* Handles **HTTP requests & responses**
* Contains **Controllers only**
* Performs basic request validation
* Sends requests to the Application layer
* 🚫 Contains **no business logic**

---

### 2️⃣ Application Layer

* Contains **application use cases**
* Implements **CQRS (Commands & Queries)**
* Responsible for:

  * Request / Response DTOs
  * Application services
  * Validation rules
* Defines abstractions:

  * `IRepository`
  * `IUnitOfWork`
  * Service interfaces

🚫 No database access or framework-specific code

---

### 3️⃣ Domain Layer (Core)

* Represents the **core business logic**
* Contains:

  * Entities
  * Enums
  * Value Objects
  * Repository interfaces
* Completely **independent** of frameworks and infrastructure

✅ Pure business rules only

---

### 4️⃣ Infrastructure Layer

* Implements interfaces defined in the Domain layer
* Responsible for:

  * Database access (EF Core)
  * Migrations
  * Seed data
  * External API integrations
* Depends on **Domain** and **Application** layers

---

## 🔄 CRUD Operation Flow

1. Client sends a request to the **Presentation Layer**.
2. Controller forwards the request to the **Application Layer**.
3. Application layer executes the corresponding **Command** or **Query**.
4. Domain layer applies business rules and validations.
5. Infrastructure layer accesses the database or external services.
6. Result flows back through:

   ```
   Infrastructure → Application → Presentation
   ```
7. Response is returned to the client.

---

## 🧩 Patterns & Practices Used

* Clean Architecture
* CQRS (Command Query Responsibility Segregation)
* Repository Pattern
* Unit of Work
* Dependency Injection
* DTO Pattern

---

## ✅ Benefits

### 🔹 Maintainability

Clear separation of responsibilities allows changes without affecting other layers.

### 🔹 Testability

Each layer can be tested independently using unit and integration tests.

### 🔹 Flexibility

Infrastructure (database, external APIs) can be replaced with minimal impact.

### 🔹 Scalability

Designed to scale for large applications and team collaboration.

---

## 📝 Notes

* This is a **simplified Clean Architecture implementation**
* The structure can be extended with:

  * Domain Events
  * Caching layer
  * Background jobs
  * Messaging systems (RabbitMQ, Kafka)
* Suitable for **real-world enterprise applications**

---

## 🚀 Getting Started

1. Configure the database connection in `appsettings.json`
2. Run database migrations
3. Start the API project

---

## 📚 Controllers & Endpoints

* Controllers are located in the **Presentation layer**
* Controllers:

  * Receive requests from clients
  * Forward them to the Application layer
  * Return responses to the client
* 🚫 Controllers **must not contain business logic**

---

## 📌 Final Notes

This architecture is designed to keep the system:

* Clean
* Modular
* Easy to maintain
* Easy to test
* Ready for future growth

## 📜 License

This project is source-available.

You are allowed to use and modify the code for personal or internal purposes.
Redistribution or public publishing of this project is strictly prohibited
without written permission from the author.

Copyright © 2026 Yousef Walid

All rights reserved.

---
# Use UserSecrets for sensitive data in development in secrets.json:(DbConnectionString, JWT Secret, etc.)