using MSKSuggestionManagement.Application.Dtos;
using MSKSuggestionManagement.Application.Interfaces;
using MSKSuggestionManagement.Application.Mappers;
using MSKSuggestionManagement.Domain.Interfaces;

namespace MSKSuggestionManagement.Application.Services
{
    public class EmployeeService(IEmployeeRepo employeeRepo) : IEmployeeService
    {
        private IEmployeeRepo _EmployeeRepo { get; set; } = employeeRepo ?? throw new ArgumentNullException(nameof(employeeRepo));

        public async Task<IEnumerable<EmployeeDto>> GetEmployees()
        {
            var employees = await _EmployeeRepo.GetEmployees();
            return employees.Select(e => e.ToDto());
        }

        public async Task<EmployeeDto?> GetEmployeeById(Guid id)
        {
            var employee = await _EmployeeRepo.GetEmployeeById(id);
            return employee?.ToDto();
        }

        public async Task<EmployeeDto> AddEmployee(EmployeeDto dto)
        {
            var employeeEntity = dto.ToEntity();
            var savedEntity = await _EmployeeRepo.AddEmployee(employeeEntity);
            return savedEntity.ToDto();
        }
    }
}