using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSKSuggestionManagement.Infrastructure.Data;
using MSKSuggestionManagement.Domain.Entities;

namespace MSKSuggestionManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployees()
        {
            var employees = await _context.Employees.ToListAsync();
            return Ok(employees);
        }
    }
}