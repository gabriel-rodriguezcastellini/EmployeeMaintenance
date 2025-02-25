using MediatR;

namespace EmployeeMaintenance.Application.Commands
{
    public record DeleteDepartmentCommand(int Id) : IRequest<bool>;
}
