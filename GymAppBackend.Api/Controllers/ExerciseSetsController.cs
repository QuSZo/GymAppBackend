using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.ExerciseSets.Commands;
using GymAppBackend.Application.ExerciseSets.Commands.CreateExerciseSet;
using GymAppBackend.Application.ExerciseSets.Commands.DeleteExerciseSet;
using GymAppBackend.Application.ExerciseSets.Commands.UpdateExerciseSet;
using GymAppBackend.Application.ExerciseSets.Queries.DTO;
using GymAppBackend.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GymAppBackend.Api.Controllers;

[ApiController]
[Route("exercise-sets")]
public class ExerciseSetsController : ControllerBase
{
    private readonly ICommandHandler<CreateExerciseSetCommand, CreateOrUpdateResponse> _createExerciseSetHandler;
    private readonly ICommandHandler<UpdateExerciseSetCommand, CreateOrUpdateResponse> _updateExerciseSetHandler;
    private readonly ICommandHandler<DeleteExerciseSetCommand, CreateOrUpdateResponse> _deleteExerciseSetHandler;

    public ExerciseSetsController(
        ICommandHandler<CreateExerciseSetCommand, CreateOrUpdateResponse> createExerciseSetHandler,
        ICommandHandler<UpdateExerciseSetCommand, CreateOrUpdateResponse> updateExerciseSetHandler,
        ICommandHandler<DeleteExerciseSetCommand, CreateOrUpdateResponse> deleteExerciseSetHandler)
    {
        _createExerciseSetHandler = createExerciseSetHandler;
        _updateExerciseSetHandler = updateExerciseSetHandler;
        _deleteExerciseSetHandler = deleteExerciseSetHandler;
    }

    [HttpPost]
    public async Task<ActionResult<CreateOrUpdateResponse>> Create([FromBody] CreateExerciseSetCommand command)
    {
        var response = await _createExerciseSetHandler.HandleAsync(command);
        return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ExerciseSetDto>> Get([FromRoute] Guid id)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateExerciseSetCommand command)
    {
        await _updateExerciseSetHandler.HandleAsync(command with{Id = id});
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _deleteExerciseSetHandler.HandleAsync(new DeleteExerciseSetCommand(id));
        return Ok();
    }
}