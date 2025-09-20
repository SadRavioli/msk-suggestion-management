using MSKSuggestionManagement.Domain.Enums;

namespace MSKSuggestionManagement.Domain.Entities
{
    public class Suggestion
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public SuggestionStatus Status { get; set; } = SuggestionStatus.Pending;
        public Priority Priority { get; set; } = Priority.Low; 
        public SuggestionSource Source { get; set; } = SuggestionSource.Admin;
        public string? CreatedBy { get; set; }
        public string? Notes { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateUpdated { get; set; } = DateTime.UtcNow;
        public DateTime? DateCompleted { get; set; }

        public Employee Employee { get; set; } = null!;
    }
}