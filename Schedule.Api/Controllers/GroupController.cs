using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Features.Groups.Queries.GetList;
using Schedule.Application.ViewModels;

namespace Schedule.Controllers;

public class GroupController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ICollection<GroupViewModel>>> GetAll(
        [FromQuery] GetGroupListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}