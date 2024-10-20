using GymAppBackend.Core.ExerciseCategories.Entities;
using GymAppBackend.Core.ExerciseCategories.Repositories;

namespace GymAppBackend.Infrastructure.DAL.ExerciseCategories.Repositories;

public class InMemoryExerciseCategoryRepository : IExerciseCategoryRepository
{
    private readonly List<ExerciseCategory> _exerciseCategories;

    public InMemoryExerciseCategoryRepository()
    {
        _exerciseCategories = new List<ExerciseCategory>
        {
            ExerciseCategory.Create(new Guid("6edd345a-4b17-4d0f-ba3e-9493c883f8c9"), "Klata"),
            ExerciseCategory.Create(new Guid("4bb407c0-3ec0-424a-80bb-70f6f2fa3aac"), "Biceps")
        };
    }

    public async Task<ExerciseCategory> GetAsync(Guid id)
    {
        await Task.CompletedTask;
        return _exerciseCategories.FirstOrDefault(exerciseCategory => exerciseCategory.Id == id);
    }
}