using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.ExerciseTypes.Queries.Dto;
using GymAppBackend.Application.ExerciseTypes.Queries.GetExerciseTypes;
using Microsoft.AspNetCore.Mvc;

namespace GymAppBackend.Api.Controllers;

[ApiController]
[Route("exercise-types")]
public class ExerciseTypesController : ControllerBase
{
    private readonly IQueryHandler<GetExerciseTypesQuery, IEnumerable<ExerciseTypeDto>> _exerciseTypeQueryHandler;

    public ExerciseTypesController(IQueryHandler<GetExerciseTypesQuery, IEnumerable<ExerciseTypeDto>> exerciseTypeQueryHandler)
    {
        _exerciseTypeQueryHandler = exerciseTypeQueryHandler;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExerciseTypeDto>>> GetAll()
    {
        return Ok(await _exerciseTypeQueryHandler.HandleAsync(new GetExerciseTypesQuery()));
    }
}