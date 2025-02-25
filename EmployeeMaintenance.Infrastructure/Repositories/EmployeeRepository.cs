using EmployeeMaintenance.Application.Contracts;
using EmployeeMaintenance.Domain.Entities;
using EmployeeMaintenance.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMaintenance.Infrastructure.Repositories
{
    public class EmployeeRepository(AppDbContext context) : IEmployeeRepository
    {
        public async Task AddAsync(Employee employee)
        {
            _ = await context.Employees.AddAsync(employee);
        }

        public void Remove(Employee employee)
        {
            _ = context.Employees.Remove(employee);
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await context.Employees
                .Include(e => e.Department)
                .ToListAsync();
        }
    }
}
