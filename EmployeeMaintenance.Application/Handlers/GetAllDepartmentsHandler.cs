using EmployeeMaintenance.Application.Contracts;
using EmployeeMaintenance.Application.DTOs;
using EmployeeMaintenance.Application.Queries;
using MediatR;

namespace EmployeeMaintenance.Application.Handlers
{
    public class GetAllDepartmentsHandler(IUnitOfWork uow) : IRequestHandler<GetAllDepartmentsQuery, IEnumerable<DepartmentDto>>
    {
        public async Task<IEnumerable<DepartmentDto>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            List<Domain.Entities.Department> depts = await uow.Departments.GetAllAsync();
            return depts.Select(d => new DepartmentDto
            {
                Id = d.Id,
                Name = d.Name
            });
        }
    }
}
