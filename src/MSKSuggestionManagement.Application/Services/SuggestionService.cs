using MSKSuggestionManagement.Application.Dtos;
using MSKSuggestionManagement.Application.Interfaces;
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
        
        public async Task<SuggestionDto?> GetSuggestionById(Guid id)
        {
            var suggestion = await _SuggestionRepo.GetSuggestionById(id);
            return suggestion?.ToDto();
        }

        public async Task<SuggestionDto> AddSuggestion(AddSuggestionDto dto)
        {
            var suggestionEntity = dto.ToEntity();
            var savedEntity = await _SuggestionRepo.AddSuggestion(suggestionEntity);
            return savedEntity.ToDto();
        }

        public async Task<SuggestionStatusDto?> UpdateSuggestionStatus(Guid id, UpdateStatusDto dto)
        {
            var suggestion = await _SuggestionRepo.UpdateSuggestionStatus(id, dto.Status);
            return suggestion?.ToStatusDto();
        }

        public async Task<SuggestionNotesDto?> UpdateSuggestionNotes(Guid id, UpdateNotesDto dto)
        {
            var suggestion = await _SuggestionRepo.UpdateSuggestionNotes(id, dto.Notes);
            return suggestion?.ToNotesDto();
        }
    }
}