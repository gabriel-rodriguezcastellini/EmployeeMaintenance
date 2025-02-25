using MediatR;

namespace EmployeeMaintenance.Application.Commands
{
    public record CreateEmployeeCommand(
        string FirstName,
        string LastName,
        DateTime HireDate,
        int DepartmentId,
        string Phone,
        string Address
    ) : IRequest<int>;
}
