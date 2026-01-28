# 🧱 Fas7nyProject – Clean Architecture Tourism Platform

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
│   │   ├── Ai              // AI service DTOs
│   │   ├── Payment         // Payment service DTOs
│   │   └── SearchLog       // Search & logging DTOs
│   ├── Options             // Configuration options (OpenAI, Paymob, Algolia)
│   ├── Services
│   │   ├── OpenAiService   // AI-powered services
│   │   ├── Payment         // Paymob payment integration
│   │   └── AlgoliaSearch   // Search service integration
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
└── Program.cs              // Application entry point & DI configuration
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
  * AI services integration
  * Payment processing
  * Search functionality
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

1. Client sends a request to the **Presentation Layer**
2. Controller forwards the request to the **Application Layer**
3. Application layer executes the corresponding **Command** or **Query**
4. Domain layer applies business rules and validations
5. Infrastructure layer accesses the database or external services
6. Result flows back through:
```
   Infrastructure → Application → Presentation
```
7. Response is returned to the client

---

## 🧩 Patterns & Practices Used

* **Clean Architecture**
* **CQRS** (Command Query Responsibility Segregation)
* **Repository Pattern**
* **Unit of Work**
* **Dependency Injection**
* **DTO Pattern**
* **Options Pattern** (for configuration)

---

## 🔌 Third-Party Integrations

### 🔍 Algolia Search Service
* **Purpose**: Fast and intelligent search for tourism data
* **Features**:
  * Search places, cities, attractions
  * Real-time indexing
  * Faceted search and filtering
  * Geo-search capabilities

**Configuration** (`appsettings.json`):
```json
{
  "Algolia": {
    "AppId": "your-app-id",
    "SearchApiKey": "your-search-api-key",
    "Indexes": {
      "places": "places_index",
      "hotels": "hotels_index",
      "restaurants": "restaurants_index"
    }
  }
}
```

### 💳 Paymob Payment Gateway
* **Purpose**: Secure payment processing for bookings
* **Features**:
  * Multiple payment methods
  * EGP currency support
  * Secure transaction handling
  * Payment webhooks

**Configuration** (`appsettings.json`):
```json
{
  "Paymob": {
    "ApiKey": "your-api-key",
    "IntegrationId": 123456,
    "Currency": "EGP",
    "BaseUrl": "https://accept.paymob.com/api"
  }
}
```

### 🤖 OpenAI Integration
* **Purpose**: AI-powered features for enhanced user experience
* **Model**: GPT-4 / GPT-3.5-turbo

**Configuration** (`appsettings.json`):
```json
{
  "OpenAI": {
    "ApiKey": "your-openai-api-key",
    "BaseUrl": "https://api.openai.com/v1",
    "Model": "gpt-4"
  }
}
```

---

## 🤖 AI Services

### 1️⃣ User Behavior Analysis
**Purpose**: Analyze user behavior patterns and generate personalized insights

**Features**:
* Track user search history
* Analyze clicked places and bookings
* Identify behavior patterns
* Detect risk factors
* Generate personalized recommendations

**Endpoint**: `POST /api/ai/analyze-behavior`

**Request**:
```json
{
  "userId": "user123",
  "searchHistory": ["Cairo", "Alexandria", "Luxor"],
  "clickedPlaces": ["pyramids", "museum"],
  "bookings": ["booking123"],
  "lastActivityDate": "2026-01-28"
}
```

**Response**:
```json
{
  "behaviorPatterns": [
    "Frequent historical site searches",
    "Prefers cultural experiences"
  ],
  "preferences": [
    "Ancient history",
    "Museums and monuments"
  ],
  "recommendations": [
    "Valley of the Kings tour",
    "Egyptian Museum visit"
  ],
  "riskFactors": []
}
```

---

### 2️⃣ AI Chat Assistant
**Purpose**: Conversational AI assistant for real-time customer support

**Features**:
* Natural language understanding
* Tourism-specific knowledge
* Context-aware responses
* Multi-turn conversations

**Endpoint**: `POST /api/ai/chat`

**Request**:
```json
{
  "message": "What are the best places to visit in Cairo?"
}
```

**Response**:
```json
{
  "response": "Cairo offers amazing attractions including the Pyramids of Giza, the Egyptian Museum, Khan el-Khalili bazaar, and the Citadel of Saladin..."
}
```

---

### 3️⃣ Package Generator
**Purpose**: Generate complete tourism packages based on user preferences

**Features**:
* Budget-based planning
* Customized itineraries
* Activity recommendations
* Cost breakdown
* Duration-based planning

**Endpoint**: `POST /api/ai/generate-package`

**Request**:
```json
{
  "budget": 5000,
  "destination": "Cairo",
  "duration": 5,
  "preferences": ["historical", "cultural", "food"]
}
```

**Response**:
```json
{
  "packageName": "Cairo Historical & Cultural Experience",
  "description": "5-day immersive journey through Egypt's rich history...",
  "totalCost": 4850,
  "items": [
    {
      "name": "Hotel Accommodation",
      "description": "4-star hotel near Pyramids",
      "cost": 2000
    },
    {
      "name": "Guided Tours",
      "description": "Professional Egyptologist guide",
      "cost": 1500
    }
  ],
  "itinerary": [
    {
      "day": 1,
      "activities": ["Pyramids of Giza", "Sphinx", "Sound & Light Show"]
    },
    {
      "day": 2,
      "activities": ["Egyptian Museum", "Khan el-Khalili"]
    }
  ]
}
```

---

### 4️⃣ Smart Recommendations
**Purpose**: Provide personalized tourism recommendations

**Features**:
* Behavior-based recommendations
* Preference matching
* Relevance scoring
* Category-based filtering

**Endpoint**: `POST /api/ai/recommendations`

**Request**:
```json
{
  "preferences": ["beach", "adventure", "nightlife"],
  "location": "Red Sea",
  "budget": 3000
}
```

**Response**:
```json
{
  "recommendations": [
    {
      "title": "Hurghada Water Sports Package",
      "description": "Diving, snorkeling, and parasailing experience",
      "relevanceScore": 0.95,
      "category": "Adventure"
    },
    {
      "title": "Sharm El Sheikh Beach Resort",
      "description": "All-inclusive beach resort with nightlife",
      "relevanceScore": 0.89,
      "category": "Beach & Nightlife"
    }
  ]
}
```

---

## 📊 Database Relationships

### Relationship Diagram
```plaintext
ApplicationUser (1) ────────── (1) Cart
       │
       ├──── (1:N) ────── Bookings
       ├──── (1:N) ────── ChatMessages
       ├──── (1:N) ────── SearchLogs
       ├──── (1:N) ────── Recommendations
       ├──── (1:N) ────── Reviews
       ├──── (1:N) ────── UserInteractions
       └──── (1:N) ────── UserPreferences

Cart (1) ────────── (N) CartItems

CartItems (N) ────────── (1) Booking

Booking (1) ────────── (1) Payment

Package (1) ────────── (N) Reviews

Country (1) ────────── (N) Cities

City (1) ────────── (N) Hotels
     (1) ────────── (N) Restaurants
     (1) ────────── (N) TouristPlaces
     (1) ────────── (N) Packages

Hotel (1) ────────── (N) HotelRooms
      (1) ────────── (N) Packages

Package (1) ────────── (N) PackageDetails

TouristPlace (1) ────────── (N) PackageDetails
```

### Key Relationships:

#### 1️⃣ One-to-One:
* `User ←→ Cart` (Each user has one cart)
* `Booking ←→ Payment` (Each booking has one payment)

#### 2️⃣ One-to-Many:
* `User → Bookings, Reviews, Interactions, Preferences`
* `Cart → CartItems`
* `Package → Reviews, PackageDetails`
* `City → Hotels, Restaurants, TouristPlaces, Packages`
* `Country → Cities`

#### 3️⃣ Many-to-One:
* `CartItems → Booking` (Many cart items can reference same booking)
* `Reviews → User, Package`
* `PackageDetails → Package, TouristPlace`

### Delete Behaviors:
* **Cascade**: When parent deleted, children deleted (`User → Cart`, `Cart → CartItems`)
* **Restrict**: Prevents deletion if children exist (`City → Hotels`)
* **SetNull**: Sets FK to null when parent deleted

---

## ⚙️ Service Registration (Program.cs)
```csharp
// Configure Options
builder.Services.Configure<OpenAIOptions>(
    builder.Configuration.GetSection("OpenAI"));
builder.Services.Configure<PaymobOptions>(
    builder.Configuration.GetSection("Paymob"));
builder.Services.Configure<AlgoliaOptions>(
    builder.Configuration.GetSection("Algolia"));

// Register Services
builder.Services.AddScoped<IAiService, AiService>();
builder.Services.AddHttpClient<IPaymobService, PaymobService>();
builder.Services.AddScoped<IAlgoliaSearchRepository, AlgoliaService>();

// Register DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Unit of Work & Repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
```

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

### 🔹 AI-Powered
Intelligent features enhance user experience and personalization.

### 🔹 Secure Payments
Integrated payment gateway ensures secure transactions.

### 🔹 Fast Search
Algolia-powered search provides instant results.

---

## 🚀 Getting Started

### Prerequisites
* .NET 8.0 or higher
* SQL Server
* OpenAI API Key
* Paymob Account
* Algolia Account

### Setup Steps

1. **Clone the repository**
```bash
   git clone https://github.com/yourusername/Fas7nyProject.git
   cd Fas7nyProject
```

2. **Configure User Secrets** (Development)
```bash
   dotnet user-secrets init
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "your-connection-string"
   dotnet user-secrets set "OpenAI:ApiKey" "your-openai-key"
   dotnet user-secrets set "Paymob:ApiKey" "your-paymob-key"
   dotnet user-secrets set "Algolia:AppId" "your-algolia-app-id"
   dotnet user-secrets set "Algolia:SearchApiKey" "your-algolia-search-key"
```

3. **Update appsettings.json** (Production)
   * Configure database connection
   * Set API keys and integration IDs

4. **Run Migrations**
```bash
   dotnet ef database update
```

5. **Run the Application**
```bash
   dotnet run --project Presentation
```

6. **Access Swagger UI**
```
   https://localhost:5001/swagger
```

---

## 🔒 Security Best Practices

* ✅ Use **User Secrets** for development
* ✅ Use **Azure Key Vault** or **AWS Secrets Manager** for production
* ✅ Never commit sensitive data to source control
* ✅ Use environment variables for CI/CD pipelines
* ✅ Implement proper authentication and authorization
* ✅ Validate all user inputs
* ✅ Use HTTPS only

---

## 📚 API Documentation

All API endpoints are documented using **Swagger/OpenAPI**.

Access the interactive documentation at: `/swagger`

### Main Controllers:
* **Auth Controller** - User authentication & registration
* **Booking Controller** - Booking management
* **Package Controller** - Tourism packages
* **Payment Controller** - Payment processing
* **AI Controller** - AI-powered services
* **Search Controller** - Algolia search integration

---

## 📝 Testing
```bash
# Run unit tests
dotnet test

# Run with coverage
dotnet test /p:CollectCoverage=true
```

---

## 📌 Future Enhancements

* [ ] Domain Events
* [ ] Caching layer (Redis)
* [ ] Background jobs (Hangfire)
* [ ] Messaging systems (RabbitMQ/Kafka)
* [ ] Real-time notifications (SignalR)
* [ ] Mobile app API
* [ ] Admin dashboard
* [ ] Multi-language support
* [ ] Advanced analytics

---

## 📜 License

This project is **source-available**.

You are allowed to use and modify the code for personal or internal purposes.
**Redistribution or public publishing** of this project is **strictly prohibited**
without written permission from the author.

**Copyright © 2026 Yousef Walid**

All rights reserved.

---

## 👨‍💻 Author

**Yousef Walid**

For questions or collaboration inquiries, please contact via GitHub.

---

## 🙏 Acknowledgments

* ASP.NET Core Team
* OpenAI
* Paymob
* Algolia
* Clean Architecture Community

---

**Built with ❤️ using Clean Architecture principles**