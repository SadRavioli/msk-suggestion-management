using MSKSuggestionManagement.Domain.Enums;

namespace MSKSuggestionManagement.Application.Dtos;

public class SuggestionDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public required EmployeeDto Employee { get; set; }
    public string EmployeeDetails { get; set; } = string.Empty;
    public SuggestionType Type { get; set; }
    public required string Description { get; set; }
    public SuggestionStatus Status { get; set; }
    public Priority Priority { get; set; }
    public SuggestionSource Source { get; set; }
    public string? Notes { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? DateCompleted { get; set; }

}
