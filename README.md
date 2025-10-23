# Aurora University Management System

Aurora University is a modern online-first institution. This application handles student enrollment, seminar allocation, marks and resits, and term progression calculation.

## Available Endpoints
The endpoints can be tested using Postman or Swagger.

### 1. Validate University Code
Validates university identifiers used for emails and exports.

**Endpoint:**
```
POST http://localhost:5050/api/UniversityCode/validate
```
**Input (JSON):**
```json
{
  "UniversityCode": "AU-STF-20253-PSYCH-5555-01"
}
```
**Output:**

**Example Response (Valid Code):**
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
**Example Response (Invalid Code):**
```json
{
  "error": "Checksum mismatch"
}

```

### 2. Enroll Students to Module
Enroll students to a given module while checking capacity and session conflicts.

**Endpoint:**
```
POST http://localhost:5050/api/Enrollment/batch
```
**Input (JSON):**
```json
{
  "ModuleId": "module-guid",
  "StudentIds": ["student-guid-1", "student-guid-2"]
}
```
**Output:**
```json
{
  "ModuleId": "module-guid",
  "SuccessfullyEnrolledStudents": [...],
  "FailedEnrollmentStudents": {...},
  "GroupAssignments": {...},
  "UnplacedStudents": [...]
}
```

### 3. Calculate Student Grades
Calculates the term result for a student including resits, weighted module marks, credit-weighted average, date range, and progression decision.

**Endpoint:**
```
GET http://localhost:5050/api/Grades/{studentId}
```
**Output (JSON):**
```json
{
  "StudentId": "student-guid",
  "FirstName": "Fatemeh",
  "LastName": "Ghasemi",
  "ModuleGrades": [...],
  "TermAverage": 72.5,
  "AssessmentStart": "2025-01-10T00:00:00Z",
  "AssessmentEnd": "2025-05-12T00:00:00Z",
  "ProgressionDecision": "Pass"
}
```

## Technical Details
- ASP.NET Core Web API - v8
- Entity Framework Core - v8 (Code-First)
- Clean Architecture
- Clean Code
- Repository & Service Pattern
- BDD (Behavior-Driven Development)
- PostgreSQL - v15
- Swagger for API documentation
- Testcontainers for integration testing
- Fluent Assertions
- Docker
- Visual Studio 2022 - v17

## Database Design
The core entities include:
- `Student`, `TermModule`, `Session`, `Assessment`, `Mark`
- Relationships:
  - Students enroll in multiple modules
  - Modules have multiple sessions and assessments
  - Marks link students to assessments
  - Seminar groups have capacity limits and session schedules

Database is generated using EF Core migrations (code-first approach).

## Get Started

### 1. Clone the repository
```bash
git clone https://github.com/fatemehghasemi/aurora-university.git
```

### 2. Start with Docker Compose
Make sure Docker is installed.

```bash
docker-compose up -d
```

Services included:
- Web API application: http://localhost:5050
- PostgreSQL database: http://localhost:5433

To rebuild after changes:

```bash
docker-compose up -d --build
```

To stop and remove all containers:

```bash
docker-compose down
```

The database schema will be automatically created inside the PostgreSQL container using:
```csharp
Database.EnsureCreated();
```

## Testing
- Unit tests written using xUnit
- Test coverage includes:
  - Seminar allocation (time conflicts, capacity limits)
  - University code validation (checksum, format)
  - Marks & resits handling
  - Term progression calculations