# DH Vehicle Inventory Management System

A RESTful Web API for managing a vehicle rental inventory, built using **Clean Architecture** and **Domain-Driven Design (DDD)** principles with ASP.NET Core 8.0, Entity Framework Core, and SQL Server.

---

## Architecture Overview

This solution follows **Clean Architecture** (Onion Architecture) with 4 layers. The key principle is that dependencies always point **inward** — outer layers depend on inner layers, never the reverse.

### Project References (Dependency Direction)

- Domain has no dependencies (innermost layer)
- Application depends on Domain only
- Infrastructure depends on Application
- WebAPI depends on Application and Infrastructure

---

## Explanation of Clean Architecture Layers

### 1. Domain Layer (DH_VehicleInventory.Domain)

The innermost layer — the heart of the application. It contains:

- **Vehicle Entity** (Vehicle.cs) — The core business object with encapsulated behavior. All properties have private setters, meaning status can only be changed through domain methods like MarkRented(), MarkReserved(), etc.
- **VehicleStatus Enum** (VehicleStatus.cs) — Defines the 4 possible states: Available (1), Reserved (2), Rented (3), Maintenance (4).
- **VehicleType Enum** (VehicleType.cs) — Defines vehicle categories: Sedan (1), SUV (2), Truck (10), Van (4).
- **DomainException** (DomainException.cs) — Custom exception thrown when a business rule is violated.

This layer has zero external dependencies — no NuGet packages, no framework references. It is pure C# and contains only business logic.

### 2. Application Layer (DH_VehicleInventory.Application)

The use case layer — coordinates operations between the WebAPI and the Domain. It contains:

- **DH_IVehicleRepository** — An interface that defines what data operations are available. The Infrastructure layer provides the actual implementation.
- **DH_VehicleService** — The main service class that implements all use cases: Create, Read, Update Status, and Delete.
- **DTOs** — DH_CreateVehicleDto (input for creating), DH_UpdateVehicleStatusDto (input for status change), DH_VehicleDto (output returned to user).
- **DH_CreateVehicleValidator** — Validates user input before it reaches the domain.

This layer depends only on the Domain layer. It has no knowledge of databases or HTTP.

### 3. Infrastructure Layer (DH_VehicleInventory.Infrastructure)

The data access layer — implements the interfaces defined in the Application layer using Entity Framework Core. It contains:

- **DH_InventoryDbContext** — The EF Core database context with a DbSet called DH_Vehicles.
- **DH_VehicleConfiguration** — Fluent API configuration that maps the Vehicle entity to the DH_Vehicles table.
- **DH_VehicleRepository** — Implements DH_IVehicleRepository using EF Core for all CRUD operations.

This layer depends on the Application layer and contains no business logic.

### 4. WebAPI Layer (DH_VehicleInventory.WebAPI)

The presentation layer — the entry point for HTTP requests. It contains:

- **Program.cs** — Configures dependency injection, EF Core, and Swagger.
- **DH_VehiclesController** — REST API controller with 5 endpoints. Delegates ALL logic to DH_VehicleService.
- **appsettings.json** — Contains the database connection string.

Controllers contain no business logic. They only receive requests, call the service, and return HTTP responses.

---



