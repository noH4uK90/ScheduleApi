using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Schedule.Common;

namespace Schedule.Controllers;

[ApiController]
[EnableCors(Constants.CorsName)]
[Route("/api/[controller]")]
public class BaseController : ControllerBase
{
    protected IMediator Mediator => HttpContext.RequestServices.GetRequiredService<IMediator>();
}