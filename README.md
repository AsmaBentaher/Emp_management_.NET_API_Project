# Employee Management API

A practice ASP.NET Core Web API built using Clean Architecture.

## Technologies

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger/OpenAPI
- C#


## Architecture

The solution is divided into four projects:

- Domain
	- Contains business entities.

- Application
	- Contains DTOs, interfaces, and application services.

- Infrastructure
	- Contains Entity Framework Core, database configuration, and repository implementation.

- API
	- Contains controllers and HTTP configuration.


## Features
- Create employee
- Get all employees
- Get employee by ID
- Update employee
- Delete employee


## Architecure Flow

Controller --> Application Service --> Repository Interface --> Repository Implementation --> Entity Framework Core --> SQL Server.