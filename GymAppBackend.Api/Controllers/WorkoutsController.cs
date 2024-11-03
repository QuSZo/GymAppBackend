using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Responses;
using GymAppBackend.Application.Workouts.Commands.CreateWorkout;
using GymAppBackend.Application.Workouts.Queries.DTO;
using GymAppBackend.Application.Workouts.Queries.GetWorkout;
using GymAppBackend.Application.Workouts.Queries.GetWorkoutByDate;
using GymAppBackend.Application.Workouts.Queries.GetWorkouts;
using GymAppBackend.Core.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace GymAppBackend.Api.Controllers;

[ApiController]
[Route("workouts")]
public class WorkoutsController : ControllerBase
{
    private readonly IQueryHandler<GetWorkoutsQuery, IEnumerable<WorkoutsDto>> _getWorkoutsHandler;
    private readonly IQueryHandler<GetWorkoutQuery, WorkoutDetailsDto> _getWorkoutHandler;
    private readonly IQueryHandler<GetWorkoutByDateQuery, WorkoutDetailsDto> _getWorkoutByDateHandler;
    private readonly ICommandHandler<CreateWorkoutCommand, CreateOrUpdateResponse> _createWorkoutHandler;

    public WorkoutsController(
        IQueryHandler<GetWorkoutsQuery, IEnumerable<WorkoutsDto>> getWorkoutsHandler,
        IQueryHandler<GetWorkoutQuery, WorkoutDetailsDto> getWorkoutHandler,
        ICommandHandler<CreateWorkoutCommand, CreateOrUpdateResponse> createWorkoutHandler,
        IQueryHandler<GetWorkoutByDateQuery, WorkoutDetailsDto> getWorkoutByDateHandler)
    {
        _getWorkoutsHandler = getWorkoutsHandler;
        _getWorkoutHandler = getWorkoutHandler;
        _createWorkoutHandler = createWorkoutHandler;
        _getWorkoutByDateHandler = getWorkoutByDateHandler;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkoutsDto>>> GetAll()
        => Ok(await _getWorkoutsHandler.HandleAsync(new GetWorkoutsQuery()));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<WorkoutDetailsDto>> GetById([FromRoute] Guid id)
    {
        return await _getWorkoutHandler.HandleAsync(new GetWorkoutQuery{Id = id});
    }

    [HttpGet("{date:datetime}")]
    public async Task<ActionResult<WorkoutDetailsDto>> GetByDate([FromRoute] DateTime date)
    {
        return await _getWorkoutByDateHandler.HandleAsync(new GetWorkoutByDateQuery{Date = date});
    }

    [HttpPost]
    public async Task<ActionResult<CreateOrUpdateResponse>> Post([FromBody] CreateWorkoutCommand command)
    {
        var response = await _createWorkoutHandler.HandleAsync(command);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response.Id);
    }
}