using MSKSuggestionManagement.Domain.Enums;

namespace MSKSuggestionManagement.Domain.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public RiskLevel RiskLevel { get; set; } = RiskLevel.Low;

        public IEnumerable<Suggestion> Suggestions { get; set; } = [];
    }
}