using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.Homework.Commands.Create;
using Schedule.Application.Features.Homework.Commands.Delete;
using Schedule.Application.Features.Homework.Queries.GetList;
using Schedule.Application.ViewModels;

namespace Schedule.Controllers;

public class HomeworkController : BaseController
{
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<ICollection<HomeworkViewModel>>> GetAll(
        [FromQuery] GetHomeworksListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
    
    [Authorize(Roles = "Teacher")]
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateHomeworkCommand command)
    {
        var id = await Mediator.Send(command);
        return Created(string.Empty, id);
    }

    [Authorize(Roles = "Teacher")]
    [HttpDelete]
    public async Task<IActionResult> Delete(
        [FromQuery] DeleteHomeworkCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }
}