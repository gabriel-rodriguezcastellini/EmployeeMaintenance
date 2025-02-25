using EmployeeMaintenance.Application.Contracts;
using EmployeeMaintenance.Application.DTOs;
using EmployeeMaintenance.Application.Handlers;
using EmployeeMaintenance.Application.Queries;
using EmployeeMaintenance.Domain.Entities;
using Moq;

namespace EmployeeMaintenance.Tests.Handlers.Employees
{
    public class GetAllEmployeesHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnDtos()
        {
            // Arrange
            Mock<IUnitOfWork> uowMock = new();
            Mock<IEmployeeRepository> repoMock = new();

            List<Employee> employees =
                    [
                        new Employee
                        {
                            Id = 1,
                            FirstName = "E1",
                            LastName = "L1",
                            Department = new Department { Name = "Dept1" },
                            Phone = "1234567890",
                            Address = "Address1"
                        },
                        new Employee
                        {
                            Id = 2,
                            FirstName = "E2",
                            LastName = "L2",
                            Department = new Department { Name = "Dept2" },
                            Phone = "0987654321",
                            Address = "Address2"
                        }
                    ];

            _ = repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(employees);
            _ = uowMock.SetupGet(u => u.Employees).Returns(repoMock.Object);

            GetAllEmployeesHandler handler = new(uowMock.Object);
            GetAllEmployeesQuery query = new();

            // Act
            IEnumerable<EmployeeDto> result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            List<EmployeeDto> list = [.. result];
            Assert.Equal(2, list.Count);
            Assert.Contains(list, e => e.Id == 1 && e.FirstName == "E1");
            Assert.Contains(list, e => e.Id == 2 && e.FirstName == "E2");
        }
    }
}
