using MediatR;

namespace EmployeeMaintenance.Application.Commands
{
    public record CreateDepartmentCommand(string Name) : IRequest<int>;
}
