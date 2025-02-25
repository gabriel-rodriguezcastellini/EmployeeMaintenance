using EmployeeMaintenance.Application.Contracts;
using EmployeeMaintenance.Application.Handlers;
using EmployeeMaintenance.Application.Queries;
using EmployeeMaintenance.Domain.Entities;
using Moq;

namespace EmployeeMaintenance.Tests.Handlers.Departments
{
    public class GetDepartmentByIdHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnNull_WhenDepartmentNotFound()
        {
            // Arrange
            Mock<IUnitOfWork> uowMock = new();
            Mock<IDepartmentRepository> deptRepoMock = new();

            _ = deptRepoMock.Setup(r => r.GetByIdAsync(999))
                .ReturnsAsync((Department?)null);

            _ = uowMock.SetupGet(u => u.Departments).Returns(deptRepoMock.Object);

            GetDepartmentByIdHandler handler = new(uowMock.Object);
            GetDepartmentByIdQuery query = new(999);

            // Act
            Application.DTOs.DepartmentDto? result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Handle_ShouldReturnDepartmentDto_WhenFound()
        {
            // Arrange
            Mock<IUnitOfWork> uowMock = new();
            Mock<IDepartmentRepository> deptRepoMock = new();

            Department existingDept = new() { Id = 1, Name = "IT" };

            _ = deptRepoMock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(existingDept);

            _ = uowMock.SetupGet(u => u.Departments).Returns(deptRepoMock.Object);

            GetDepartmentByIdHandler handler = new(uowMock.Object);
            GetDepartmentByIdQuery query = new(1);

            // Act
            Application.DTOs.DepartmentDto? dto = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(dto);
            Assert.Equal(1, dto.Id);
            Assert.Equal("IT", dto.Name);
        }
    }
}
