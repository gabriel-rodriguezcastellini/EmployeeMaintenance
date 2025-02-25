using EmployeeMaintenance.Application.Commands;
using EmployeeMaintenance.Application.Contracts;
using EmployeeMaintenance.Application.Handlers;
using EmployeeMaintenance.Domain.Entities;
using Moq;

namespace EmployeeMaintenance.Tests.Handlers.Employees
{
    public class DeleteEmployeeHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnFalse_IfEmployeeNotFound()
        {
            // Arrange
            Mock<IUnitOfWork> uowMock = new();
            Mock<IEmployeeRepository> repoMock = new();

            _ = repoMock.Setup(r => r.GetByIdAsync(999))
                .ReturnsAsync((Employee?)null);

            _ = uowMock.SetupGet(u => u.Employees).Returns(repoMock.Object);

            DeleteEmployeeHandler handler = new(uowMock.Object);
            DeleteEmployeeCommand command = new(999);

            // Act
            bool result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
            uowMock.Verify(u => u.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldRemoveEmployeeAndReturnTrue()
        {
            // Arrange
            Mock<IUnitOfWork> uowMock = new();
            Mock<IEmployeeRepository> repoMock = new();

            Employee existing = new()
            {
                Id = 5,
                FirstName = "John",
                LastName = "Doe",
                HireDate = DateTime.Now,
                DepartmentId = 1,
                Department = new Department { Id = 1, Name = "HR" },
                Phone = "123-456-7890",
                Address = "123 Main St"
            };

            _ = repoMock.Setup(r => r.GetByIdAsync(5))
                .ReturnsAsync(existing);

            _ = uowMock.SetupGet(u => u.Employees).Returns(repoMock.Object);

            DeleteEmployeeHandler handler = new(uowMock.Object);
            DeleteEmployeeCommand command = new(5);

            // Act
            bool result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            repoMock.Verify(r => r.Remove(existing), Times.Once);
            uowMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}
