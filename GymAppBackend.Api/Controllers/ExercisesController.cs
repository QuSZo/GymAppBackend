using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Exercises.Commands.CreateExercise;
using GymAppBackend.Application.Exercises.Commands.DeleteExercise;
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
    private readonly ICommandHandler<CreateExerciseCommand, CreateOrUpdateResponse> _createExerciseCommandHandler;
    private readonly ICommandHandler<DeleteExerciseCommand, CreateOrUpdateResponse> _deleteExerciseCommandHandler;

    public ExercisesController(
        ICommandHandler<CreateExerciseCommand, CreateOrUpdateResponse> createExerciseCommandHandler, ICommandHandler<DeleteExerciseCommand, CreateOrUpdateResponse> deleteExerciseCommandHandler)
    {
        _createExerciseCommandHandler = createExerciseCommandHandler;
        _deleteExerciseCommandHandler = deleteExerciseCommandHandler;
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
        var resposne = await _createExerciseCommandHandler.HandleAsync(command);
        return CreatedAtAction(nameof(GetById), new { id = resposne.Id }, resposne.Id);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _deleteExerciseCommandHandler.HandleAsync(new DeleteExerciseCommand(id));
        return Ok();
    }
}