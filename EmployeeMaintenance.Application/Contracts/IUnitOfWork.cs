namespace EmployeeMaintenance.Application.Contracts
{
    public interface IUnitOfWork
    {
        IEmployeeRepository Employees { get; }
        IDepartmentRepository Departments { get; }

        Task<int> SaveChangesAsync();
    }
}
