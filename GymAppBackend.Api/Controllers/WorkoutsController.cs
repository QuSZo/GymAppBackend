using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Workouts.Commands.CreateWorkout;
using GymAppBackend.Application.Workouts.Queries.DTO;
using GymAppBackend.Application.Workouts.Queries.GetWorkouts;
using Microsoft.AspNetCore.Mvc;

namespace GymAppBackend.Api.Controllers;

[ApiController]
[Route("workouts")]
public class WorkoutsController : ControllerBase
{
    private readonly IQueryHandler<GetWorkoutsQuery, IEnumerable<WorkoutsDto>> _getWorkoutsHandler;
    private readonly ICommandHandler<CreateWorkoutCommand> _createWorkoutHandler;

    public WorkoutsController(
        IQueryHandler<GetWorkoutsQuery, IEnumerable<WorkoutsDto>> getWorkoutsHandler,
        ICommandHandler<CreateWorkoutCommand> createWorkoutHandler)
    {
        _getWorkoutsHandler = getWorkoutsHandler;
        _createWorkoutHandler = createWorkoutHandler;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkoutsDto>>> Get([FromQuery] GetWorkoutsQuery query)
        => Ok(await _getWorkoutsHandler.HandleAsync(query));

    [HttpPost]
    public async Task<ActionResult> Post()
    {
        await _createWorkoutHandler.HandleAsync(new CreateWorkoutCommand());
        return NoContent();
    }
}