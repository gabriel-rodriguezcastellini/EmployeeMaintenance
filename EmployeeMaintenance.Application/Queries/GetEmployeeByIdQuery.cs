using EmployeeMaintenance.Application.DTOs;
using MediatR;

namespace EmployeeMaintenance.Application.Queries
{
    public record GetEmployeeByIdQuery(int Id) : IRequest<EmployeeDto>;
}
