

# .NET Clean Architecture Web API – University Management Sample

An enterprise-grade ASP.NET Core Web API sample demonstrating **Clean Architecture**, **real-world business rules**, and **testable domain logic** using a university management domain.

This repository is designed as a **production-style reference project**, not a toy example.

---

## Why This Project Exists

Most Clean Architecture samples are overly simplified and fail to represent real business complexity.

This project focuses on:

* Realistic domain rules (enrollment capacity, scheduling conflicts, resits, progression)
* Clean separation of concerns
* High testability and maintainability
* Patterns commonly expected in enterprise .NET teams

Ideal for:

* Backend developers building scalable APIs
* Interview preparation & portfolio showcase
* Architecture reference for real projects

---

## Domain Overview

**Aurora University** is a modern, online-first institution.

The system manages:

* Student enrollment
* Seminar & session allocation
* Assessments, marks, and resits
* Credit-weighted averages
* Term progression decisions

All rules are enforced at the **domain layer**, not the controller level.

---

## API Capabilities (Selected Endpoints)

### Validate University Code

Validates structured university identifiers used for emails and exports.

```
POST /api/UniversityCode/validate
```

```json
{
  "UniversityCode": "AU-STF-20253-PSYCH-5555-01"
}
```

**Valid response:**

```json
{
  "type": "STF",
  "year": 2025,
  "term": 3,
  "code": "PSYCH",
  "serial": "5555",
  "checksum": 1
}
```

---

### Batch Enrollment with Conflict Detection

Enrolls students into modules while handling:

* Capacity limits
* Session time conflicts
* Group assignment rules

```
POST /api/Enrollment/batch
```

---

### Student Term Result & Progression

Calculates final term results including:

* Resits
* Weighted module averages
* Assessment date range
* Progression decision

```
GET /api/Grades/{studentId}
```

---

## Architecture & Technical Stack

* **ASP.NET Core Web API (.NET 8)**
* **Entity Framework Core 8 (Code-First)**
* **Clean Architecture**
* **Clean Code principles**
* **Domain-Driven Design (DDD-inspired)**
* **Repository & Service patterns**
* **CQRS-ready structure**
* **PostgreSQL 15**
* **Swagger / OpenAPI**
* **Docker & Docker Compose**
* **Testcontainers for integration tests**
* **xUnit + FluentAssertions**
* **Visual Studio 2022**

---

## Solution Structure

* **Domain**
  Core business logic, entities, value objects, and rules

* **Application**
  Use cases, services, orchestration logic

* **Infrastructure**
  EF Core, database access, external dependencies

* **API**
  Controllers, request/response contracts

* **Tests**
  Unit and integration tests with real database containers

---

## Database Model

Key entities:

* `Student`
* `TermModule`
* `Session`
* `Assessment`
* `Mark`

Relationships reflect real university constraints:

* Students enroll in multiple modules
* Modules have sessions with capacity and schedules
* Assessments produce marks
* Progression depends on weighted credits and resits

Database is generated via **EF Core migrations (code-first)**.

---

## Getting Started

### Clone the repository

```bash
git clone https://github.com/fatemehghasemi/dotnet-clean-architecture-university-api.git
```

### Run with Docker

```bash
docker-compose up -d
```

Services:

* API: [http://localhost:5050](http://localhost:5050)
* PostgreSQL: [http://localhost:5433](http://localhost:5433)

Rebuild:

```bash
docker-compose up -d --build
```

Stop:

```bash
docker-compose down
```

Database initialization:

```csharp
Database.EnsureCreated();
```

---

## Testing Strategy

* Unit tests for domain rules
* Integration tests using **Testcontainers**
* Coverage includes:

  * Enrollment conflicts
  * Capacity enforcement
  * University code validation
  * Marks & resits logic
  * Term progression decisions

---

## Status

This project is actively maintained and can be extended with:

* Authentication & authorization
* Event-driven messaging
* Full CQRS with read models
* Modular monolith or microservice split

---