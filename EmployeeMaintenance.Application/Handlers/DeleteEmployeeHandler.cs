using EmployeeMaintenance.Application.Commands;
using EmployeeMaintenance.Application.Contracts;
using MediatR;

namespace EmployeeMaintenance.Application.Handlers
{
    public class DeleteEmployeeHandler(IUnitOfWork uow) : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Employee? employee = await uow.Employees.GetByIdAsync(request.Id);
            if (employee == null)
            {
                return false;
            }

            uow.Employees.Remove(employee);
            _ = await uow.SaveChangesAsync();
            return true;
        }
    }
}
