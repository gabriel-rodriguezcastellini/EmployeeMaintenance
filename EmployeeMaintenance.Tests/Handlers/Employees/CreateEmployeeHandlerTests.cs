using EmployeeMaintenance.Application.Commands;
using EmployeeMaintenance.Application.Contracts;
using EmployeeMaintenance.Application.Handlers;
using EmployeeMaintenance.Domain.Entities;
using Moq;

namespace EmployeeMaintenance.Tests.Handlers.Employees
{
    public class CreateEmployeeHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnNewEmployeeId_WhenEmployeeIsCreated()
        {
            // Arrange
            Mock<IUnitOfWork> unitOfWorkMock = new();
            Mock<IEmployeeRepository> employeeRepoMock = new();
            Mock<IDepartmentRepository> departmentRepoMock = new();

            _ = unitOfWorkMock.SetupGet(u => u.Employees).Returns(employeeRepoMock.Object);
            _ = unitOfWorkMock.SetupGet(u => u.Departments).Returns(departmentRepoMock.Object);

            _ = unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            _ = employeeRepoMock.Setup(r => r.AddAsync(It.IsAny<Employee>()))
                                .Callback<Employee>(e => e.Id = 1);

            _ = departmentRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                                  .ReturnsAsync(new Department { Id = 1, Name = "HR" });

            CreateEmployeeHandler handler = new(unitOfWorkMock.Object);

            CreateEmployeeCommand command = new(
                "John", "Doe", DateTime.UtcNow, 1, "555-1234", "123 Main St");

            // Act
            int result = await handler.Handle(command, CancellationToken.None);

            // Assert            
            employeeRepoMock.Verify(r => r.AddAsync(It.IsAny<Employee>()), Times.Once);
            unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);

            Assert.Equal(1, result);
        }
    }
}
