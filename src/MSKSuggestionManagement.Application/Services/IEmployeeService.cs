using MSKSuggestionManagement.Application.Dtos;

namespace MSKSuggestionManagement.Application.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetEmployees();
        Task<EmployeeDto> AddEmployee(EmployeeDto employeeDto);
    }
}