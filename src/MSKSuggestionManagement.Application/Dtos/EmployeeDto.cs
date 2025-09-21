using MSKSuggestionManagement.Domain.Entities;
using MSKSuggestionManagement.Domain.Enums;

namespace MSKSuggestionManagement.Application.Dtos
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public required string FullName { get; set; }
        public required string Department { get; set; }
        public required RiskLevel RiskLevel { get; set; }

        public IEnumerable<Suggestion>? Suggestions { get; set; }
    }
}
