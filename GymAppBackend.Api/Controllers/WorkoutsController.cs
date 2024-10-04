using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Workouts.Queries.DTO;
using GymAppBackend.Application.Workouts.Queries.GetWorkouts;
using Microsoft.AspNetCore.Mvc;

namespace GymAppBackend.Api.Controllers;

[ApiController]
[Route("workouts")]
public class WorkoutsController : ControllerBase
{
    private readonly IQueryHandler<GetWorkoutsQuery, IEnumerable<WorkoutsDto>> _getWorkoutsHandler;

    public WorkoutsController(IQueryHandler<GetWorkoutsQuery, IEnumerable<WorkoutsDto>> getWorkoutsHandler)
    {
        _getWorkoutsHandler = getWorkoutsHandler;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkoutsDto>>> Get([FromQuery] GetWorkoutsQuery query)
        => Ok(await _getWorkoutsHandler.HandleAsync(query));
}