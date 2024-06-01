using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.Timetables.Query.GetForGroup;
using Schedule.Application.Features.Timetables.Query.GetForTeacher;
using Schedule.Application.ViewModels;

namespace Schedule.Controllers;

public class TimetableController : BaseController
{
    [HttpGet]
    [Route("Group")]
    public async Task<ActionResult<ICollection<TimetableViewModel>>> GetForGroup(
        [FromQuery] GetTimetableForGroupQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
    
    [HttpGet]
    [Route("Teacher")]
    public async Task<ActionResult<ICollection<TimetableViewModel>>> GetForTeacher(
        [FromQuery] GetTimetableForTeacherQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}