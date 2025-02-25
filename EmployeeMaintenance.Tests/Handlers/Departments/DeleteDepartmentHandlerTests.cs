using EmployeeMaintenance.Application.Commands;
using EmployeeMaintenance.Application.Contracts;
using EmployeeMaintenance.Application.Handlers;
using EmployeeMaintenance.Domain.Entities;
using Moq;

namespace EmployeeMaintenance.Tests.Handlers.Departments
{
    public class DeleteDepartmentHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenDepartmentNotFound()
        {
            // Arrange
            Mock<IUnitOfWork> uowMock = new();
            Mock<IDepartmentRepository> deptRepoMock = new();

            _ = deptRepoMock.Setup(r => r.GetByIdAsync(999))
                .ReturnsAsync((Department?)null);

            _ = uowMock.SetupGet(u => u.Departments).Returns(deptRepoMock.Object);

            DeleteDepartmentHandler handler = new(uowMock.Object);
            DeleteDepartmentCommand command = new(999);

            // Act
            bool result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
            uowMock.Verify(u => u.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldRemoveDepartment_AndReturnTrue_WhenFound()
        {
            // Arrange
            Mock<IUnitOfWork> uowMock = new();
            Mock<IDepartmentRepository> deptRepoMock = new();

            Department existingDept = new() { Id = 5, Name = "HR" };

            _ = deptRepoMock.Setup(r => r.GetByIdAsync(5))
                .ReturnsAsync(existingDept);

            _ = uowMock.SetupGet(u => u.Departments).Returns(deptRepoMock.Object);

            DeleteDepartmentHandler handler = new(uowMock.Object);
            DeleteDepartmentCommand command = new(5);

            // Act
            bool result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            deptRepoMock.Verify(r => r.Remove(existingDept), Times.Once);
            uowMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}
