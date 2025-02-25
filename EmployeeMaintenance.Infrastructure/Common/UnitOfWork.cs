using EmployeeMaintenance.Application.Contracts;
using EmployeeMaintenance.Infrastructure.Persistence;
using EmployeeMaintenance.Infrastructure.Repositories;

namespace EmployeeMaintenance.Infrastructure.Common
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        private IEmployeeRepository? _employeeRepo;
        private IDepartmentRepository? _departmentRepo;

        public IEmployeeRepository Employees => _employeeRepo ??= new EmployeeRepository(context);

        public IDepartmentRepository Departments => _departmentRepo ??= new DepartmentRepository(context);

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
