using EmployeeMaintenance.Application.Contracts;
using EmployeeMaintenance.Application.DTOs;
using EmployeeMaintenance.Application.Queries;
using MediatR;

namespace EmployeeMaintenance.Application.Handlers
{
    public class GetDepartmentByIdHandler(IUnitOfWork uow) : IRequestHandler<GetDepartmentByIdQuery, DepartmentDto?>
    {
        public async Task<DepartmentDto?> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.Department? dept = await uow.Departments.GetByIdAsync(request.Id);
            return dept == null
                ? null
                : new DepartmentDto
                {
                    Id = dept.Id,
                    Name = dept.Name
                };
        }
    }
}
