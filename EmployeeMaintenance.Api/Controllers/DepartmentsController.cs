using EmployeeMaintenance.Application.Commands;
using EmployeeMaintenance.Application.DTOs;
using EmployeeMaintenance.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMaintenance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentCommand command)
        {
            int newId = await mediator.Send(command);
            return CreatedAtAction(nameof(GetDepartmentById), new { id = newId }, newId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            DeleteDepartmentCommand command = new(id);
            bool deleted = await mediator.Send(command);
            return !deleted ? NotFound() : NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            GetDepartmentByIdQuery query = new(id);
            DepartmentDto department = await mediator.Send(query);
            return department == null ? NotFound() : Ok(department);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            GetAllDepartmentsQuery query = new();
            IEnumerable<DepartmentDto> departments = await mediator.Send(query);
            return Ok(departments);
        }
    }
}
