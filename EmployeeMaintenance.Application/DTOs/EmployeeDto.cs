namespace EmployeeMaintenance.Application.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime HireDate { get; set; }
        public int DepartmentId { get; set; }
        public required string DepartmentName { get; set; }
        public required string Phone { get; set; }
        public required string Address { get; set; }
    }
}
