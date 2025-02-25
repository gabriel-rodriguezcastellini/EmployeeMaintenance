using EmployeeMaintenance.Application.Contracts;
using EmployeeMaintenance.Domain.Entities;
using EmployeeMaintenance.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMaintenance.Infrastructure.Repositories
{
    public class DepartmentRepository(AppDbContext context) : IDepartmentRepository
    {
        public async Task AddAsync(Department department)
        {
            _ = await context.Departments.AddAsync(department);
        }

        public void Remove(Department department)
        {
            _ = context.Departments.Remove(department);
        }

        public async Task<Department?> GetByIdAsync(int id)
        {
            Department? department = await context.Departments.FindAsync(id);
            return department;
        }

        public async Task<List<Department>> GetAllAsync()
        {
            return await context.Departments.ToListAsync();
        }
    }
}
