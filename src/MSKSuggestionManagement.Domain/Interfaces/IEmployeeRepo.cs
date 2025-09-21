using MSKSuggestionManagement.Domain.Entities;

namespace MSKSuggestionManagement.Domain.Interfaces
{
    public interface IEmployeeRepo
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> AddEmployee(Employee employee);
    }
}