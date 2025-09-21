using Microsoft.AspNetCore.Mvc;
using MSKSuggestionManagement.Application.Services;
using MSKSuggestionManagement.Application.Dtos;

namespace MSKSuggestionManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private IEmployeeService _EmployeeService { get; set; }

        public EmployeesController(IEmployeeService employeeService)
        {
            _EmployeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            var employeeDtos = await _EmployeeService.GetEmployees();
            return Ok(employeeDtos);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> AddEmployee(EmployeeDto dto)
        {
            var newEmployeeDto = await _EmployeeService.AddEmployee(dto);
            return Ok(newEmployeeDto);
        }
    }
}