using Microsoft.EntityFrameworkCore;
using MSKSuggestionManagement.Domain.Entities;
using MSKSuggestionManagement.Domain.Interfaces;
using MSKSuggestionManagement.Infrastructure.Data;

namespace MSKSuggestionManagement.Infrastructure.Repos
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private ApplicationDbContext _DbContext { get; set; }

        public EmployeeRepo(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _DbContext.Employees.AsNoTracking().ToListAsync();
        }

        public async Task<Employee?> GetEmployeeById(Guid id)
        {
            return await _DbContext.Employees.FirstOrDefaultAsync(s => s.Id == id);
        }


        public async Task<Employee> AddEmployee(Employee employee)
        {
            _DbContext.Employees.Add(employee);
            await _DbContext.SaveChangesAsync();

            return employee;
        }
    }
}