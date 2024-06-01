using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.Teachers.Commands.Create;
using Schedule.Application.Features.Teachers.Commands.Delete;
using Schedule.Application.Features.Teachers.Commands.Restore;
using Schedule.Application.Features.Teachers.Commands.Update;
using Schedule.Application.Features.Teachers.Queries.GetByAccount;
using Schedule.Application.Features.Teachers.Queries.GetFullNameByAccount;
using Schedule.Application.Features.Teachers.Queries.GetFullNameList;
using Schedule.Application.Features.Teachers.Queries.GetList;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Controllers;

public class TeacherController : BaseController
{
    [HttpGet("Account/{id:int}")]
    public async Task<ActionResult<TeacherViewModel>> GetTeacherByAccount(int id)
    {
        var query = new GetTeacherByAccountQuery(id);
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("FullName/Account/{id:int}")]
    public async Task<ActionResult<TeacherFullNameViewModel>> GetTeacherFullNameByAccount(int id)
    {
        var query = new GetTeacherFullNameByAccountQuery(id);
        return Ok(await Mediator.Send(query));
    }
    
    [HttpGet]
    public async Task<ActionResult<PagedList<TeacherViewModel>>> GetAll(
        [FromQuery] GetTeacherListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("FullName")]
    public async Task<ActionResult<ICollection<TeacherFullNameViewModel>>> GetAll(
        [FromQuery] GetTeacherFullNameListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [Authorize(Roles = "Employee")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateTeacherCommand command)
    {
        var id = await Mediator.Send(command);
        return Created(string.Empty, id);
    }

    [Authorize(Roles = "Employee")]
    [HttpPost]
    [Route("Restore", Name = "RestoreTeacher")]
    public async Task<IActionResult> Post([FromBody] RestoreTeacherCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [Authorize(Roles = "Employee")]
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateTeacherCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [Authorize(Roles = "Employee")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var query = new DeleteTeacherCommand(id);
        await Mediator.Send(query);
        return NoContent();
    }
}