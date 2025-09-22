using MSKSuggestionManagement.Application.Dtos;

namespace MSKSuggestionManagement.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetEmployees();
        Task<EmployeeDto?> GetEmployeeById(Guid id);
        Task<EmployeeDto> AddEmployee(EmployeeDto employeeDto);
    }
}