using MediatR;

namespace EmployeeMaintenance.Application.Commands
{
    public record DeleteEmployeeCommand(int Id) : IRequest<bool>;
}
