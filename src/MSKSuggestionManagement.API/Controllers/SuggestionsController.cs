using Microsoft.AspNetCore.Mvc;
using MSKSuggestionManagement.Application.Dtos;
using MSKSuggestionManagement.Application.Interfaces;

namespace MSKSuggestionManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuggestionsController : ControllerBase
    {
        private ISuggestionService _SuggestionService { get; set; }

        public SuggestionsController(ISuggestionService suggestionService)
        {
            _SuggestionService = suggestionService ?? throw new ArgumentNullException(nameof(suggestionService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SuggestionDto>>> GetSuggestions()
        {
            var suggestions = await _SuggestionService.GetSuggestions();
            return Ok(suggestions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuggestionDto>> GetSuggestionById(Guid id)
        {
            var suggestion = await _SuggestionService.GetSuggestionById(id);

            if (suggestion == null)
                return NotFound($"Suggestion with id {id} not found");

            return Ok(suggestion);
        }

        [HttpPost]
        public async Task<ActionResult<SuggestionDto>> AddSuggestion(SuggestionDto dto)
        {
            var newSuggestionDto = await _SuggestionService.AddSuggestion(dto);
            return Ok(newSuggestionDto);
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult<SuggestionDto>> UpdateSuggestionStatus(Guid id,
                    [FromBody] UpdateStatusDto dto)
        {
            var result = await _SuggestionService.UpdateSuggestionStatus(id, dto);
    
            if (result == null)
                return NotFound($"Suggestion with id {id} not found");

            return Ok(result);
        }

        [HttpPatch("{id}/notes")]
        public async Task<ActionResult<SuggestionDto>> UpdateSuggestionNotes(Guid id,
                    [FromBody] UpdateNotesDto dto)
        {
            var result = await _SuggestionService.UpdateSuggestionNotes(id, dto);
    
            if (result == null)
                return NotFound($"Suggestion with id {id} not found");

            return Ok(result);
        }
    }
}