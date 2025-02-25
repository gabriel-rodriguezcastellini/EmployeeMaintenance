using EmployeeMaintenance.Application.Commands;
using EmployeeMaintenance.Application.Contracts;
using EmployeeMaintenance.Domain.Entities;
using MediatR;

namespace EmployeeMaintenance.Application.Handlers
{
    public class CreateEmployeeHandler(IUnitOfWork uow) : IRequestHandler<CreateEmployeeCommand, int>
    {
        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            Department department = await uow.Departments.GetByIdAsync(request.DepartmentId) ?? throw new ArgumentException("Invalid DepartmentId");
            Employee employee = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                HireDate = request.HireDate,
                DepartmentId = request.DepartmentId,
                Department = department,
                Phone = request.Phone,
                Address = request.Address
            };

            await uow.Employees.AddAsync(employee);
            _ = await uow.SaveChangesAsync();
            return employee.Id;
        }
    }
}
