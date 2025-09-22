using Microsoft.EntityFrameworkCore;
using MSKSuggestionManagement.Domain.Entities;
using MSKSuggestionManagement.Domain.Interfaces;
using MSKSuggestionManagement.Infrastructure.Data;

namespace MSKSuggestionManagement.Infrastructure.Repos
{
    public class SuggestionRepo : ISuggestionRepo
    {
        private ApplicationDbContext DbContext { get; set; }

        public SuggestionRepo(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<Suggestion>> GetSuggestions()
        {
            return await DbContext.Suggestions.AsNoTracking().Include(s => s.Employee).ToListAsync();
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            DbContext.Employees.Add(employee);
            await DbContext.SaveChangesAsync();

            return employee;
        }

        public async Task<Suggestion> AddSuggestion(Suggestion suggestion)
        {
            DbContext.Suggestions.Add(suggestion);
            await DbContext.SaveChangesAsync();

            return suggestion;
        }
    }
}