using EmployeeMaintenance.Application.Commands;
using EmployeeMaintenance.Application.Contracts;
using EmployeeMaintenance.Application.Handlers;
using EmployeeMaintenance.Domain.Entities;
using Moq;

namespace EmployeeMaintenance.Tests.Handlers.Employees
{
    public class UpdateEmployeeHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnFalse_IfEmployeeNotFound()
        {
            // Arrange
            Mock<IUnitOfWork> uowMock = new();
            Mock<IEmployeeRepository> employeeRepoMock = new();

            _ = employeeRepoMock.Setup(r => r.GetByIdAsync(999))
                .ReturnsAsync((Employee?)null);

            _ = uowMock.SetupGet(u => u.Employees).Returns(employeeRepoMock.Object);

            UpdateEmployeeHandler handler = new(uowMock.Object);
            UpdateEmployeeCommand command = new(
                999, "Jane", "Doe", DateTime.UtcNow, 1, "555-9876", "Address");

            // Act
            bool result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
            uowMock.Verify(u => u.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnTrue_AndSave_WhenEmployeeFound()
        {
            // Arrange
            Mock<IUnitOfWork> uowMock = new();
            Mock<IEmployeeRepository> employeeRepoMock = new();

            Employee existingEmployee = new()
            {
                Id = 10,
                FirstName = "Old",
                LastName = "Name",
                Department = new Department { Name = "Old Department" },
                Phone = "555-0000",
                Address = "Old Address"
            };

            _ = employeeRepoMock.Setup(r => r.GetByIdAsync(10))
                .ReturnsAsync(existingEmployee);

            _ = uowMock.SetupGet(u => u.Employees).Returns(employeeRepoMock.Object);

            UpdateEmployeeHandler handler = new(uowMock.Object);
            UpdateEmployeeCommand command = new(
                10, "NewFirst", "NewLast", DateTime.UtcNow, 2, "555-1111", "New Address");

            // Act
            bool result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            Assert.Equal("NewFirst", existingEmployee.FirstName);
            Assert.Equal("NewLast", existingEmployee.LastName);

            uowMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}
