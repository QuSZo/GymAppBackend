using GymAppBackend.Core.ExerciseCategories.Repositories;
using GymAppBackend.Core.ExerciseTypes.Entities;
using GymAppBackend.Core.ExerciseTypes.Repositories;

namespace GymAppBackend.Infrastructure.DAL.ExerciseTypes.Repositories;

public class InMemoryExerciseTypeRepository : IExerciseTypeRepository
{
    private readonly List<ExerciseType> _exerciseTypes;

    public InMemoryExerciseTypeRepository()
    {
        _exerciseTypes = new List<ExerciseType>
        {
            ExerciseType.Create(
                new Guid("a96ca5d6-4e88-47cc-84f2-3c08e17924cb"),
                "Ławka prosta",
                new Guid("6edd345a-4b17-4d0f-ba3e-9493c883f8c9")
            ),
            ExerciseType.Create(
                new Guid("dcec8104-29a0-4684-9f60-15b7c63072fb"),
                "Hantelki",
                new Guid("6edd345a-4b17-4d0f-ba3e-9493c883f8c9")
            ),
        };
    }

    public async Task<ExerciseType?> GetAsync(Guid id)
    {
        await Task.CompletedTask;
        return _exerciseTypes.FirstOrDefault(exerciseType => exerciseType.Id == id);
    }
}