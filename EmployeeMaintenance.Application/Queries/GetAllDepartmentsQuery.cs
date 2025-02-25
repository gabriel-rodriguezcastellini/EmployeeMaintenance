using EmployeeMaintenance.Application.DTOs;
using MediatR;

namespace EmployeeMaintenance.Application.Queries
{
    public record GetAllDepartmentsQuery() : IRequest<IEnumerable<DepartmentDto>>;
}
