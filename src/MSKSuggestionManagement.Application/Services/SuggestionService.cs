using MSKSuggestionManagement.Application.Dtos;
using MSKSuggestionManagement.Application.Mappers;
using MSKSuggestionManagement.Domain.Interfaces;

namespace MSKSuggestionManagement.Application.Services
{
    public class SuggestionService(ISuggestionRepo suggestionRepo) : ISuggestionService
    {
        private ISuggestionRepo _SuggestionRepo { get; set; } = suggestionRepo ?? throw new ArgumentNullException(nameof(suggestionRepo));

        public async Task<IEnumerable<SuggestionDto>> GetSuggestions()
        {
            var suggestions = await _SuggestionRepo.GetSuggestions();
            return suggestions.Select(e => e.ToDto());
        }

        public async Task<SuggestionDto> AddSuggestion(SuggestionDto dto)
        {
            var suggestionEntity = dto.ToEntity();
            var savedEntity = await _SuggestionRepo.AddSuggestion(suggestionEntity);
            return savedEntity.ToDto();
        }
    }
}