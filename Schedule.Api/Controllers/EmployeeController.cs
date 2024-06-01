using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.Employees.Commands.Create;
using Schedule.Application.Features.Employees.Commands.Delete;
using Schedule.Application.Features.Employees.Commands.Restore;
using Schedule.Application.Features.Employees.Commands.Update;
using Schedule.Application.Features.Employees.Commands.UpdatePermissions;
using Schedule.Application.Features.Employees.Queries.GetEmployeeByAccount;
using Schedule.Application.Features.Employees.Queries.GetList;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Controllers;

public class EmployeeController : BaseController
{
    [Authorize]
    [HttpGet("Account/{id:int}")]
    public async Task<ActionResult<EmployeeViewModel>> GetEmployeeByAccount(int id)
    {
        var query = new GetEmployeeByAccountQuery(id);
        return Ok(await Mediator.Send(query));
    }
    
    [Authorize(Roles = "Employee")]
    [HttpGet]
    public async Task<ActionResult<PagedList<EmployeeViewModel>>> GetAll(
        [FromQuery] GetEmployeeListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [Authorize(Roles = "Employee")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateEmployeeCommand command)
    {
        var id = await Mediator.Send(command);
        return Created(string.Empty, id);
    }

    [Authorize(Roles = "Employee")]
    [HttpPost]
    [Route("Restore", Name = "RestoreEmployee")]
    public async Task<IActionResult> Post([FromBody] RestoreEmployeeCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [Authorize(Roles = "Employee")]
    [HttpPost]
    [Route("Permission", Name = "Permission")]
    public async Task<IActionResult> Post([FromBody] EmployeeUpdatePermissionsCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [Authorize(Roles = "Employee")]
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateEmployeeCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [Authorize(Roles = "Employee")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var query = new DeleteEmployeeCommand(id);
        await Mediator.Send(query);
        return NoContent();
    }
}