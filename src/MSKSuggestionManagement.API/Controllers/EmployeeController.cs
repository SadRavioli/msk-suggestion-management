using Microsoft.AspNetCore.Mvc;
using MSKSuggestionManagement.Application.Dtos;
using MSKSuggestionManagement.Application.Interfaces;

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

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeById(Guid id)
        {
            var employee = await _EmployeeService.GetEmployeeById(id);

            if (employee == null)
                return NotFound($"Employee with id {id} not found");

            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> AddEmployee(EmployeeDto dto)
        {
            var newEmployeeDto = await _EmployeeService.AddEmployee(dto);
            return Ok(newEmployeeDto);
        }
    }
}