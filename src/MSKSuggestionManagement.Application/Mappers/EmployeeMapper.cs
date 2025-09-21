using MSKSuggestionManagement.Application.Dtos;
using MSKSuggestionManagement.Domain.Entities;

namespace MSKSuggestionManagement.Application.Mappers
{
    public static class EmployeeMapper
    {
        public static Employee ToEntity(this EmployeeDto dto)
        {
            return new Employee
            {
                Id = dto.Id,
                Name = dto.FullName,
                Department = dto.Department,
                RiskLevel = dto.RiskLevel,
                // Suggestions = dto.Suggestions
            };
        }

        public static EmployeeDto ToDto(this Employee employee)
        {
            return new EmployeeDto
            {
                Id = employee.Id,
                FullName = employee.Name,
                Department = employee.Department,
                RiskLevel = employee.RiskLevel,
                // Suggestions = employee.Suggestions ?? new IEnumerable<SuggestionDto>{};
            };
        }
    }
}