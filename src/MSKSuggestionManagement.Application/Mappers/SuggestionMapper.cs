using MSKSuggestionManagement.Application.Dtos;
using MSKSuggestionManagement.Domain.Entities;

namespace MSKSuggestionManagement.Application.Mappers
{
    public static class SuggestionMapper
    {
        public static Suggestion ToEntity(this SuggestionDto dto)
        {            
            return new Suggestion
            {
                Id = dto.Id,
                EmployeeId = dto.EmployeeId,
                Employee = new Employee
                {
                    Id = dto.Employee.Id,
                    Name = dto.Employee.FullName,
                    Department = dto.Employee.Department,
                    RiskLevel = dto.Employee.RiskLevel
                },
                Type = dto.Type,
                Description = dto.Description,
                Status = dto.Status,
                Priority = dto.Priority,
                Source = dto.Source,
                Notes = dto.Notes,
                DateCreated = dto.DateCreated,
                DateUpdated = dto.DateUpdated,
                CreatedBy = dto.CreatedBy,
                DateCompleted = dto.DateCompleted
            };
        }

        public static SuggestionDto ToDto(this Suggestion suggestion)
        {
            var employeeDetails = string.Join("<br>",
                         suggestion.Employee.Name,
                         suggestion.Employee.Department,
                         suggestion.Employee.RiskLevel);
                         
            return new SuggestionDto
            {
                Id = suggestion.Id,
                EmployeeId = suggestion.EmployeeId,
                Employee = suggestion.Employee.ToDto(),
                EmployeeDetails = employeeDetails,
                Type = suggestion.Type,
                Description = suggestion.Description,
                Status = suggestion.Status,
                Priority = suggestion.Priority,
                Source = suggestion.Source,
                Notes = suggestion.Notes,
                DateCreated = suggestion.DateCreated,
                DateUpdated = suggestion.DateUpdated,
                CreatedBy = suggestion.CreatedBy,
                DateCompleted = suggestion.DateCompleted
            };
        }
    }
}