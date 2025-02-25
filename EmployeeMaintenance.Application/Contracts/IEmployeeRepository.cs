using EmployeeMaintenance.Domain.Entities;

namespace EmployeeMaintenance.Application.Contracts
{
    public interface IEmployeeRepository
    {
        Task AddAsync(Employee employee);
        void Remove(Employee employee);
        Task<Employee?> GetByIdAsync(int id);
        Task<List<Employee>> GetAllAsync();
    }
}
