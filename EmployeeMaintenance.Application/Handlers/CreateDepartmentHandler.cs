using EmployeeMaintenance.Application.Commands;
using EmployeeMaintenance.Application.Contracts;
using EmployeeMaintenance.Domain.Entities;
using MediatR;

namespace EmployeeMaintenance.Application.Handlers
{
    public class CreateDepartmentHandler(IUnitOfWork uow) : IRequestHandler<CreateDepartmentCommand, int>
    {
        public async Task<int> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            Department department = new() { Name = request.Name };
            await uow.Departments.AddAsync(department);
            _ = await uow.SaveChangesAsync();
            return department.Id;
        }
    }
}
