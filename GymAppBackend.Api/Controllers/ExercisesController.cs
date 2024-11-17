using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Exercises.Commands.CreateExercise;
using GymAppBackend.Application.Exercises.Commands.DeleteExercise;
using GymAppBackend.Application.Exercises.Commands.UpdateExerciseNumber;
using GymAppBackend.Application.Exercises.Queries.DTO;
using GymAppBackend.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymAppBackend.Api.Controllers;

[ApiController]
[Route("exercises")]
[Authorize]
public class ExercisesController : ControllerBase
{
    private readonly ICommandHandler<CreateExerciseCommand, CreateOrUpdateResponse> _createExerciseHandler;
    private readonly ICommandHandler<DeleteExerciseCommand, CreateOrUpdateResponse> _deleteExerciseHandler;
    private readonly ICommandHandler<UpdateExerciseNumberCommand, CreateOrUpdateResponse> _updateExerciseNumberHandler;

    public ExercisesController(
        ICommandHandler<CreateExerciseCommand, CreateOrUpdateResponse> createExerciseHandler,
        ICommandHandler<DeleteExerciseCommand, CreateOrUpdateResponse> deleteExerciseHandler,
        ICommandHandler<UpdateExerciseNumberCommand, CreateOrUpdateResponse> updateExerciseNumberHandler)
    {
        _createExerciseHandler = createExerciseHandler;
        _deleteExerciseHandler = deleteExerciseHandler;
        _updateExerciseNumberHandler = updateExerciseNumberHandler;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ExerciseDto>> GetById([FromRoute] Guid id)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<ActionResult<CreateOrUpdateResponse>> Post([FromBody] CreateExerciseCommand command)
    {
        var resposne = await _createExerciseHandler.HandleAsync(command);
        return CreatedAtAction(nameof(GetById), new { id = resposne.Id }, resposne.Id);
    }

    [HttpPut("{id:guid}/exercise-number")]
    public async Task<ActionResult<CreateOrUpdateResponse>> Put([FromRoute] Guid id, [FromBody] UpdateExerciseNumberCommand command)
    {
        var response = await _updateExerciseNumberHandler.HandleAsync(command with { Id = id });
        return Ok(response);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _deleteExerciseHandler.HandleAsync(new DeleteExerciseCommand(id));
        return Ok();
    }
}