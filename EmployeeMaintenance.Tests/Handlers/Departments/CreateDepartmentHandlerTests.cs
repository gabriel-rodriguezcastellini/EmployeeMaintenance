using EmployeeMaintenance.Application.Commands;
using EmployeeMaintenance.Application.Contracts;
using EmployeeMaintenance.Application.Handlers;
using EmployeeMaintenance.Domain.Entities;
using Moq;

namespace EmployeeMaintenance.Tests.Handlers.Departments
{
    public class CreateDepartmentHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldCreateDepartment_AndReturnId()
        {
            // Arrange
            Mock<IUnitOfWork> uowMock = new();
            Mock<IDepartmentRepository> deptRepoMock = new();

            _ = uowMock.SetupGet(u => u.Departments).Returns(deptRepoMock.Object);

            _ = uowMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            CreateDepartmentHandler handler = new(uowMock.Object);
            CreateDepartmentCommand command = new("HR");

            // Act
            int result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(0, result);
            deptRepoMock.Verify(r => r.AddAsync(It.IsAny<Department>()), Times.Once);
            uowMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}
