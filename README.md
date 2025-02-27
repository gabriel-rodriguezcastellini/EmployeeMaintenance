# EmployeeMaintenance

This solution follows a layered (Clean) architecture to promote separation of concerns, testability, and maintainability:

- **Domain**: Contains core business entities such as `Department` and `Employee`.
- **Application**: Implements business logic using CQRS with MediatR. It includes commands, queries, and DTOs alongside their handlers.
- **Infrastructure**: Handles data persistence with EF Core using repository and UnitOfWork patterns.
- **API**: Provides a RESTful API through ASP.NET Core controllers that orchestrate HTTP requests.
- **Tests**: Contains unit tests for validating the business logic in handlers.
