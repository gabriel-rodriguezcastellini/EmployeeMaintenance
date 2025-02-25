using EmployeeMaintenance.Application.Contracts;
using EmployeeMaintenance.Application.DTOs;
using EmployeeMaintenance.Application.Handlers;
using EmployeeMaintenance.Application.Queries;
using EmployeeMaintenance.Domain.Entities;
using Moq;

namespace EmployeeMaintenance.Tests.Handlers.Departments
{
    public class GetAllDepartmentsHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnDepartmentDtos()
        {
            // Arrange
            Mock<IUnitOfWork> uowMock = new();
            Mock<IDepartmentRepository> deptRepoMock = new();

            List<Department> departments =
            [
                new Department { Id = 1, Name = "HR" },
                new Department { Id = 2, Name = "IT" }
            ];

            _ = deptRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(departments);
            _ = uowMock.SetupGet(u => u.Departments).Returns(deptRepoMock.Object);

            GetAllDepartmentsHandler handler = new(uowMock.Object);
            GetAllDepartmentsQuery query = new();

            // Act
            IEnumerable<DepartmentDto> result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            List<DepartmentDto> list = [.. result];
            Assert.Equal(2, list.Count);
            Assert.Contains(list, d => d.Id == 1 && d.Name == "HR");
            Assert.Contains(list, d => d.Id == 2 && d.Name == "IT");
        }
    }
}
