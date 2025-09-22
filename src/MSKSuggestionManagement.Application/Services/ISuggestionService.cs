using MSKSuggestionManagement.Application.Dtos;

namespace MSKSuggestionManagement.Application.Services
{
    public interface ISuggestionService
    {
        Task<IEnumerable<SuggestionDto>> GetSuggestions();
        Task<SuggestionDto> AddSuggestion(SuggestionDto suggestionDto);
    }
}