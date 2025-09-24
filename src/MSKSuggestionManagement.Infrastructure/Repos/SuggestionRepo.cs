using Microsoft.EntityFrameworkCore;
using MSKSuggestionManagement.Domain.Entities;
using MSKSuggestionManagement.Domain.Enums;
using MSKSuggestionManagement.Domain.Interfaces;
using MSKSuggestionManagement.Infrastructure.Data;

namespace MSKSuggestionManagement.Infrastructure.Repos
{
    public class SuggestionRepo : ISuggestionRepo
    {
        private ApplicationDbContext _DbContext { get; set; }

        public SuggestionRepo(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<Suggestion>> GetSuggestions()
        {
            return await _DbContext.Suggestions.AsNoTracking().Include(s => s.Employee).ToListAsync();
        }
        
        public async Task<Suggestion?> GetSuggestionById(Guid id)
        {
            return await _DbContext.Suggestions
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Suggestion> AddSuggestion(Suggestion suggestion)
        {
            _DbContext.Suggestions.Add(suggestion);
            await _DbContext.SaveChangesAsync();

            return await _DbContext.Suggestions
                .Include(s => s.Employee)
                .FirstAsync(s => s.Id == suggestion.Id);
        }

        public async Task<Suggestion> UpdateSuggestionStatus(Guid id, SuggestionStatus status)
        {
            var suggestion = await _DbContext.Suggestions.Include(s => s.Employee)
                                            .FirstOrDefaultAsync(s => s.Id == id)
                    ?? throw new ArgumentException($"No suggestion found for ID {id}");

            suggestion.Status = status;
            suggestion.DateUpdated = DateTime.UtcNow;

            if (status == SuggestionStatus.Completed)
                suggestion.DateCompleted = DateTime.UtcNow;

            await _DbContext.SaveChangesAsync();
            return suggestion;
        }

        public async Task<Suggestion?> UpdateSuggestionNotes(Guid id, string notes)
        {
            var suggestion = await _DbContext.Suggestions.FirstOrDefaultAsync(s => s.Id == id);
            if (suggestion == null)
                return null;

            suggestion.Notes = notes;
            suggestion.DateUpdated = DateTime.UtcNow;

            await _DbContext.SaveChangesAsync();
            return suggestion;
        }
    }
}