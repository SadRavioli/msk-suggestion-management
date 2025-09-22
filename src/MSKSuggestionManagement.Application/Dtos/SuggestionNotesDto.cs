using MSKSuggestionManagement.Domain.Enums;

namespace MSKSuggestionManagement.Application.Dtos;

public class SuggestionNotesDto
{
    public Guid Id { get; set; }
    public string Notes { get; set; } = string.Empty;
    public DateTime DateUpdated { get; set; }
}
