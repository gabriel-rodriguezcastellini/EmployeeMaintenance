using EmployeeMaintenance.Application.Commands;
using EmployeeMaintenance.Application.DTOs;
using EmployeeMaintenance.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMaintenance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
            int newEmployeeId = await mediator.Send(command);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = newEmployeeId }, newEmployeeId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Mismatched Employee ID.");
            }

            bool updated = await mediator.Send(command);
            return !updated ? NotFound() : NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            DeleteEmployeeCommand command = new(id);
            bool deleted = await mediator.Send(command);
            return !deleted ? NotFound() : NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            GetAllEmployeesQuery query = new();
            IEnumerable<EmployeeDto> employees = await mediator.Send(query);
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            GetEmployeeByIdQuery query = new(id);
            EmployeeDto employee = await mediator.Send(query);
            return employee == null ? NotFound() : Ok(employee);
        }
    }
}
