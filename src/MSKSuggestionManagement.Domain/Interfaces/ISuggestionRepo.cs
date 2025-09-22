using MSKSuggestionManagement.Domain.Entities;
using MSKSuggestionManagement.Domain.Enums;

namespace MSKSuggestionManagement.Domain.Interfaces
{
    public interface ISuggestionRepo
    {
        Task<IEnumerable<Suggestion>> GetSuggestions();
        Task<Suggestion?> GetSuggestionById(Guid id);
        Task<Suggestion> AddSuggestion(Suggestion suggestion);
        Task<Suggestion> UpdateSuggestionStatus(Guid id, SuggestionStatus status);
        Task<Suggestion?> UpdateSuggestionNotes(Guid id, string notes);

    }
}