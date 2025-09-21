using Microsoft.EntityFrameworkCore;
using MSKSuggestionManagement.Domain.Entities;
using MSKSuggestionManagement.Domain.Interfaces;
using MSKSuggestionManagement.Infrastructure.Data;

namespace MSKSuggestionManagement.Infrastructure.Repos
{
    public class EmployeeRepository : IEmployeeRepo
    {
        private ApplicationDbContext DbContext { get; set; }

        public EmployeeRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await DbContext.Employees.ToListAsync();
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            DbContext.Employees.Add(employee);
            await DbContext.SaveChangesAsync();

            return employee;
        }
    }
}