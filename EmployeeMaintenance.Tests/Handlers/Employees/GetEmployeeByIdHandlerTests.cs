using EmployeeMaintenance.Application.Contracts;
using EmployeeMaintenance.Application.Handlers;
using EmployeeMaintenance.Application.Queries;
using EmployeeMaintenance.Domain.Entities;
using Moq;

namespace EmployeeMaintenance.Tests.Handlers.Employees
{
    public class GetEmployeeByIdHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnNull_IfNotFound()
        {
            // Arrange
            Mock<IUnitOfWork> uowMock = new();
            Mock<IEmployeeRepository> repoMock = new();

            _ = repoMock.Setup(r => r.GetByIdAsync(123))
                .ReturnsAsync((Employee?)null);

            _ = uowMock.SetupGet(u => u.Employees).Returns(repoMock.Object);

            GetEmployeeByIdHandler handler = new(uowMock.Object);
            GetEmployeeByIdQuery query = new(123);

            // Act
            Application.DTOs.EmployeeDto? result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Handle_ShouldReturnDto_IfFound()
        {
            // Arrange
            Mock<IUnitOfWork> uowMock = new();
            Mock<IEmployeeRepository> repoMock = new();

            Employee emp = new()
            {
                Id = 1,
                FirstName = "Alice",
                LastName = "Smith",
                Department = new Department { Id = 1, Name = "HR" },
                Phone = "123-456-7890",
                Address = "123 Main St"
            };

            _ = repoMock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(emp);

            _ = uowMock.SetupGet(u => u.Employees).Returns(repoMock.Object);

            GetEmployeeByIdHandler handler = new(uowMock.Object);
            GetEmployeeByIdQuery query = new(1);

            // Act
            Application.DTOs.EmployeeDto? dto = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(dto);
            Assert.Equal(1, dto.Id);
            Assert.Equal("Alice", dto.FirstName);
            _ = repoMock.Setup(r => r.GetByIdAsync(123))
                .ReturnsAsync((Employee?)null);
        }
    }
}
