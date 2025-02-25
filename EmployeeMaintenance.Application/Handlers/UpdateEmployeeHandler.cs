using EmployeeMaintenance.Application.Commands;
using EmployeeMaintenance.Application.Contracts;
using MediatR;

namespace EmployeeMaintenance.Application.Handlers
{
    public class UpdateEmployeeHandler(IUnitOfWork uow) : IRequestHandler<UpdateEmployeeCommand, bool>
    {
        public async Task<bool> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Employee? employee = await uow.Employees.GetByIdAsync(request.Id);
            if (employee == null)
            {
                return false;
            }

            employee.FirstName = request.FirstName;
            employee.LastName = request.LastName;
            employee.HireDate = request.HireDate;
            employee.DepartmentId = request.DepartmentId;
            employee.Phone = request.Phone;
            employee.Address = request.Address;

            _ = await uow.SaveChangesAsync();
            return true;
        }
    }
}
