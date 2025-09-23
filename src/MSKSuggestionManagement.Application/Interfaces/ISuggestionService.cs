using MSKSuggestionManagement.Application.Dtos;

namespace MSKSuggestionManagement.Application.Interfaces
{
    public interface ISuggestionService
    {
        Task<IEnumerable<SuggestionDto>> GetSuggestions();
        Task<SuggestionDto?> GetSuggestionById(Guid id);
        Task<SuggestionDto> AddSuggestion(AddSuggestionDto dto);
        Task<SuggestionStatusDto?> UpdateSuggestionStatus(Guid id, UpdateStatusDto dto);
        Task<SuggestionNotesDto?> UpdateSuggestionNotes(Guid id, UpdateNotesDto dto);
    }
}