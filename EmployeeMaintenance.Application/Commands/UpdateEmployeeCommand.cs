using MediatR;

namespace EmployeeMaintenance.Application.Commands
{
    public record UpdateEmployeeCommand(
        int Id,
        string FirstName,
        string LastName,
        DateTime HireDate,
        int DepartmentId,
        string Phone,
        string Address
    ) : IRequest<bool>;
}
