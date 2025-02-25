using EmployeeMaintenance.Application.DTOs;
using MediatR;

namespace EmployeeMaintenance.Application.Queries
{
    public record GetDepartmentByIdQuery(int Id) : IRequest<DepartmentDto>;
}
