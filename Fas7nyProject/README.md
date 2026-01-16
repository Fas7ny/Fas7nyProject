# clean_Architecture Referacnses
	-Presentation
	 ├── Application
	 │     └── Domain
	 └── Infrastructure
	       └── Domain  

# Structure Of clean_Architecture in this Project
  # Application
    # Cors(Query (readonly data (GetAll ,GetById ,Search ,Filter)) , Command (write(modifactions)))
	# Services
	# ServicesInterfaces
	#Dtos


  # Domain
    # Entities
    # RepoInterfaces
    # Enum
	
  # Infrastructure
      # Data (DbContextClass , Migrations , SeedData)
      # ExternalApi
	  # Repo

  # Presentaion
    # Controller

  # MainProject
    # Program.cs

  # Benefits of Clean Architecture:
     # - Maintainability: The separation of concerns makes it easier to modify and maintain the codebase
     #   without affecting other layers.
     # - Testability: Each layer can be tested independently, allowing for better unit testing and
     #   integration testing.
     # - Flexibility: The architecture allows for easy replacement of external dependencies without
     #   affecting the core business logic.

# Crud Operations Flow:
    # 1. The Presentation layer receives a request from the user (e.g., via a controller).
    # 2. The request is forwarded to the Application layer, where the appropriate service or command
    #    is invoked.
    # 3. The Application layer interacts with the Domain layer to perform business logic and retrieve
    #    or manipulate data.
    # 4. The Domain layer may interact with the Infrastructure layer to access databases or external
    #    services.
    # 5. The results are returned back through the layers to the Presentation layer, which sends the
    #    response back to the user.
    # 6. IGenericRepository  , UnitOfWork patterns are used in the Infrastructure layer to abstract data access logic and
  

---

## 🧱 Layer Responsibilities

### 1️⃣ Presentation Layer
- Handles HTTP requests and responses
- Contains Controllers only
- Performs basic request validation
- Calls Application layer use cases
- Contains no business logic

---

### 2️⃣ Application Layer
- Contains application use cases
- Implements **CQRS (Commands & Queries)**
- Handles:
  - DTOs (Request / Response)
  - Validation rules
  - Application services
- Defines abstractions:
  - `IRepository`
  - `IUnitOfWork`
  - Service interfaces

🚫 No database or framework-specific code

---

### 3️⃣ Domain Layer (Core)
- Contains core business logic
- Includes:
  - Entities
  - Enums
  - Value Objects
  - Repository Interfaces
- Completely independent from infrastructure and frameworks

✅ Pure business rules only

---

### 4️⃣ Infrastructure Layer
- Implements repository and unit of work interfaces
- Handles:
  - Database access (EF Core)
  - Migrations
  - Seed data
  - External APIs
- Depends on Application and Domain layers

---

## 🔄 CRUD Operation Flow

1. Client sends a request to the **Presentation Layer**.
2. Controller forwards the request to the **Application Layer**.
3. Application layer executes the corresponding Command or Query.
4. Domain layer applies business rules and validations.
5. Infrastructure layer accesses the database or external services.
6. Result flows back through Application → Presentation.
7. Response is returned to the client.

---

## 🧩 Patterns Used

- Clean Architecture
- CQRS
- Repository Pattern
- Unit of Work
- Dependency Injection
- DTO Pattern

---

## ✅ Benefits

### 🔹 Maintainability
Clear separation of responsibilities allows safe modifications without breaking other layers.

### 🔹 Testability
Each layer can be tested independently using unit and integration tests.

### 🔹 Flexibility
Infrastructure details (database, external APIs) can be replaced without affecting business logic.

### 🔹 Scalability
The architecture supports large-scale applications and team collaboration.

---

## 📝 Notes

- This is a simplified Clean Architecture implementation.
- The structure can be extended with:
  - Domain Events
  - Caching Layer
  - Background Jobs
  - Messaging (RabbitMQ / Kafka)
- Designed to scale for real-world enterprise applications.

---

## 🚀 Getting Started

1. Configure database connection in `appsettings.json`
2. Run migrations
3. Start the API project

---
## 📚 Controllers and Endpoints
   - Controllers are located in the Presentation layer.
   - controllers don`t contain business logic , but (تستقبل الطلبات , ترسلها لل Application Layer , وترجع الرد للعميل).)
   - 
