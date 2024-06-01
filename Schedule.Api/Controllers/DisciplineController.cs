using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.Discipline.Queries.GetGroupDisciplines;
using Schedule.Application.Features.Discipline.Queries.GetList;
using Schedule.Application.ViewModels;

namespace Schedule.Controllers;

public class DisciplineController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ICollection<DisciplineViewModel>>> GetAll(
        [FromQuery] GetDisciplinesListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    [Route("Group")]
    public async Task<ActionResult<ICollection<DisciplineViewModel>>> GetGroupDisciplines(
        [FromQuery] GetGroupDisciplinesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}