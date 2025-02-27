using EmployeeMaintenance.Application.Contracts;
using EmployeeMaintenance.Application.DTOs;
using EmployeeMaintenance.Application.Queries;
using MediatR;

namespace EmployeeMaintenance.Application.Handlers
{
    public class GetEmployeeByIdHandler(IUnitOfWork uow) : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto?>
    {
        public async Task<EmployeeDto?> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.Employee? employee = await uow.Employees.GetByIdAsync(request.Id);
            return employee == null || employee.Department == null
                ? null
                : new EmployeeDto
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    HireDate = employee.HireDate,
                    DepartmentId = employee.DepartmentId,
                    DepartmentName = employee.Department.Name,
                    Phone = employee.Phone,
                    Address = employee.Address
                };
        }
    }
}
