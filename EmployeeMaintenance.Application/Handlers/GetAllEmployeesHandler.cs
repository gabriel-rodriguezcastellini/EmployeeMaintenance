using EmployeeMaintenance.Application.Contracts;
using EmployeeMaintenance.Application.DTOs;
using EmployeeMaintenance.Application.Queries;
using MediatR;

namespace EmployeeMaintenance.Application.Handlers
{
    public class GetAllEmployeesHandler(IUnitOfWork uow) : IRequestHandler<GetAllEmployeesQuery, IEnumerable<EmployeeDto>>
    {
        public async Task<IEnumerable<EmployeeDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            List<Domain.Entities.Employee> employees = await uow.Employees.GetAllAsync();

            IEnumerable<EmployeeDto> dtos = employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                HireDate = e.HireDate,
                DepartmentName = e.Department?.Name ?? string.Empty,
                Phone = e.Phone,
                Address = e.Address
            });

            return dtos;
        }
    }
}
