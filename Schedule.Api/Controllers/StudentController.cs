using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.Students.Commands.Create;
using Schedule.Application.Features.Students.Commands.Delete;
using Schedule.Application.Features.Students.Commands.Restore;
using Schedule.Application.Features.Students.Commands.Update;
using Schedule.Application.Features.Students.Queries.GetByAccount;
using Schedule.Application.Features.Students.Queries.GetList;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Controllers;

public class StudentController : BaseController
{
    [Authorize]
    [HttpGet("Account/{id:int}")]
    public async Task<ActionResult<StudentViewModel>> GetStudentByAccount(int id)
    {
        var query = new GetStudentByAccountQuery(id);
        return Ok(await Mediator.Send(query));
    }
    
    [Authorize(Roles = "Employee")]
    [HttpGet]
    public async Task<ActionResult<PagedList<StudentViewModel>>> GetAll(
        [FromQuery] GetStudentListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [Authorize(Roles = "Employee")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateStudentCommand command)
    {
        var id = await Mediator.Send(command);
        return Created(string.Empty, id);
    }

    [Authorize(Roles = "Employee")]
    [HttpPost]
    [Route("Restore", Name = "RestoreStudent")]
    public async Task<IActionResult> Post([FromBody] RestoreStudentCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [Authorize(Roles = "Employee")]
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateStudentCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [Authorize(Roles = "Employee")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var query = new DeleteStudentCommand(id);
        await Mediator.Send(query);
        return NoContent();
    }
}