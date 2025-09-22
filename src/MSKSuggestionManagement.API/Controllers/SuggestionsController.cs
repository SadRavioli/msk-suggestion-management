using Microsoft.AspNetCore.Mvc;
using MSKSuggestionManagement.Application.Dtos;
using MSKSuggestionManagement.Application.Services;

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
        public async Task<ActionResult<List<SuggestionDto>>> GetSuggestions()
        {
            var suggestions = await _SuggestionService.GetSuggestions();
            return Ok(suggestions);
        }

        [HttpPost]
        public async Task<ActionResult<SuggestionDto>> AddEmployee(SuggestionDto dto)
        {
            var newSuggestionDto = await _SuggestionService.AddSuggestion(dto);
            return Ok(newSuggestionDto);
        }
    }
}