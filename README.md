# aa-finance-tracker App

Hi Welcome to my project! I created this project after Mint shutdown and I just couldn't find an expense tracker application that I liked.

### Features

- Expense Logging
- Banking Integration
- Budgeting Tools
- Investment Tracking
- Financial Planning

# Tech Stack

## Server Side

- [.NET 8.0](https://github.com/dotnet/core)
- [Entity Framework Core](https://github.com/aspnet/EntityFrameworkCore) on SQL Server 2022.
- Repository Pattern

### Client Side

- Angular

## Development Plan

- Phase 1

  - Enitiy Frameowork Code First (EF Core) for data persistence.
  - .NET WEB API with basic CRUD
  - Implement Repository Pattern
  - Implement Unit Testing and increase code coverage.

- Phase 2
  - Implement CQRS
  - Implement Caching
  - Implement Client UI
  - Implement Banking Integration with Plaid API and Financial Data Providers API's.
  - Implement Budgeting Tools with Charts and Graphs.
  - Implement Financial Planning with Goal Setting and Budgeting.
  - Implement Security and Authentication.
  - Implement Dapper instead of EF Core (Possibly)

## AIP Topics and Challenges

### EF Core
  - Makes creating database and query database very easy if done right
  - CLI tool is not user friendly
  - Updating Database Schemas
  - Working with Forien Keys while saving entity with relationships
  - Considering switching to Dapper in Phase 2

### Repository Pattern
- It provides a layer of abstraction between the application's business logic and the underlying data layer, allowing for more flexibility in changing the underlying technology.
- With a Repository pattern in place, it becomes easier to test the application's business logic independently of the underlying data storage mechanism
- Can over-engineering things if not done correctly
- Data Retrieval Complexity: When implementing a Repository pattern, you may encounter difficulties in retrieving specific data or performing complex queries, which can lead to additional complexity and maintenance costs.

### Unit Test
- Test driven development
- Figuring out how to plan and what to implement each test case from Arrange, Act to Assert.

### Dependency Injection
- DI helps reduce coupling between components, making it easier to modify or replace individual parts without affecting other components.
- DI enables better testability by allowing you to isolate dependencies and mock them for testing purposes.
- High learning curve, Dependency inversion concept is hard to wrap your head around for new developers.
