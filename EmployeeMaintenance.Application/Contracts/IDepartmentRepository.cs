using EmployeeMaintenance.Domain.Entities;

namespace EmployeeMaintenance.Application.Contracts
{
    public interface IDepartmentRepository
    {
        Task AddAsync(Department department);
        void Remove(Department department);
        Task<Department?> GetByIdAsync(int id);
        Task<List<Department>> GetAllAsync();
    }
}
