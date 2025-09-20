using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSKSuggestionManagement.Infrastructure.Data;
using MSKSuggestionManagement.Domain.Entities;

namespace MSKSuggestionManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuggestionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SuggestionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Suggestion>>> GetSuggestions()
        {
            var suggestions = await _context.Suggestions.ToListAsync();
            return Ok(suggestions);
        }
    }
}