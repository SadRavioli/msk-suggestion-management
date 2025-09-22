using MSKSuggestionManagement.Domain.Entities;

namespace MSKSuggestionManagement.Domain.Interfaces
{
    public interface ISuggestionRepo
    {
        Task<IEnumerable<Suggestion>> GetSuggestions();
        Task<Suggestion> AddSuggestion(Suggestion suggestion);
    }
}