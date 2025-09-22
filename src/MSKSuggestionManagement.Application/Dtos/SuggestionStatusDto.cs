using MSKSuggestionManagement.Domain.Enums;

namespace MSKSuggestionManagement.Application.Dtos;

public class SuggestionStatusDto
{
    public Guid Id { get; set; }
    public SuggestionStatus Status { get; set; }
    public DateTime DateUpdated { get; set; }
    public DateTime? DateCompleted { get; set; }

}
