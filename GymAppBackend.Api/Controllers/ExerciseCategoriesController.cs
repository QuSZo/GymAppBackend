using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.ExerciseCategories.Queries.DTO;
using GymAppBackend.Application.ExerciseCategories.Queries.GetExerciseCategories;
using Microsoft.AspNetCore.Mvc;

namespace GymAppBackend.Api.Controllers;

[ApiController]
[Route("exercise-categories")]
public class ExerciseCategoriesController : ControllerBase
{
    private readonly IQueryHandler<GetExerciseCategoriesQuery, IEnumerable<ExerciseCategoryDto>> _exerciseCategoriesQueryHandler;

    public ExerciseCategoriesController(IQueryHandler<GetExerciseCategoriesQuery, IEnumerable<ExerciseCategoryDto>> exerciseCategoriesQueryHandler)
    {
        _exerciseCategoriesQueryHandler = exerciseCategoriesQueryHandler;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExerciseCategoryDto>>> GetAll()
    {
        return Ok(await _exerciseCategoriesQueryHandler.HandleAsync(new GetExerciseCategoriesQuery()));
    }
}