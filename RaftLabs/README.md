# RaftLabs User Service Solution

## Overview

This solution is a .NET 8 Web API for user management, integrating with an external user API and supporting in-memory caching. It follows clean architecture principles, CQRS with MediatR, and is designed for extensibility and testability.

---

## Solution Structure

- **RaftLabs.API**: Main Web API project (entry point)
- **RaftLabs.Application**: Application logic, MediatR handlers, CQRS
- **Proxy.Services**: Service implementations (external API client, cache)
- **Proxy.Services.Objects**: DTOs and shared objects for services
- **Shared.Common**: Shared types and settings

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 (recommended)

---

## Build Instructions

1. **Clone the repository:**

   git clone <your-repo-url>
   cd <repo-root>

2. **Restore dependencies:**
   dotnet restore

3. **Build the solution:**
   dotnet build

---

## Running the API

1. **Configure settings:**
   - Update `appsettings.Development.json` in `RaftLabs.API` with your `ExternalUserSettings` and `CacheSettings` (API base URL, API key, cache duration, etc.).

2. **Run the API:**
   dotnet run --project RaftLabs.API/RaftLabs.API.csproj
 
The API will be available at `https://localhost:5001` (or as configured).

3. **Swagger UI:**
   - Navigate to `/swagger` for API documentation and testing.

---

## Design Decisions

- **CQRS & MediatR:**  
  The solution uses MediatR to implement the Command Query Responsibility Segregation pattern, separating read and write operations for maintainability and testability.

- **External API Integration:**  
  `ExternalUserClient` encapsulates all communication with the external user API, handling pagination and error scenarios.

- **Caching:**  
  `CacheService` provides in-memory caching using `IMemoryCache`, with configurable expiration via `CacheSettings`. This reduces load on the external API and improves response times.

- **Exception Handling:**  
  A custom middleware (`ExceptionHandlingMiddleware`) provides consistent error responses and logs details for network, deserialization, and general errors.

- **Dependency Injection:**  
  All services are registered via DI in `Program.cs`, supporting easy mocking and testing.

- **Extensibility:**  
  Interfaces (`ICacheService`, `IExternalUserClient`) allow for alternative implementations (e.g., distributed cache, different user providers).

---

## Additional Notes

- **Configuration:**  
  - `ExternalUserSettings` and `CacheSettings` are loaded from configuration files or environment variables.
  - Ensure the external API is reachable and the API key is valid.

- **Testing:**  
  - For integration tests, mock or provide test instances for external dependencies.
  - Add test projects for each layer as needed.

- **.NET 8 & C# 12:**  
  - The solution targets .NET 8 and uses C# 12 features where appropriate.

---

## Contact

vishalvelloden@gmail.com
