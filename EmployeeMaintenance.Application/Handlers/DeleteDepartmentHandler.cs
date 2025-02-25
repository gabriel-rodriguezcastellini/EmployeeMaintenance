using EmployeeMaintenance.Application.Commands;
using EmployeeMaintenance.Application.Contracts;
using MediatR;

namespace EmployeeMaintenance.Application.Handlers
{
    public class DeleteDepartmentHandler(IUnitOfWork uow) : IRequestHandler<DeleteDepartmentCommand, bool>
    {
        public async Task<bool> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Department? dept = await uow.Departments.GetByIdAsync(request.Id);
            if (dept == null)
            {
                return false;
            }

            uow.Departments.Remove(dept);
            _ = await uow.SaveChangesAsync();
            return true;
        }
    }
}
