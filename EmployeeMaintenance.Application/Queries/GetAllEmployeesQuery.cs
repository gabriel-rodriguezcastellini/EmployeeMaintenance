using EmployeeMaintenance.Application.DTOs;
using MediatR;

namespace EmployeeMaintenance.Application.Queries
{
    public record GetAllEmployeesQuery() : IRequest<IEnumerable<EmployeeDto>>;
}
