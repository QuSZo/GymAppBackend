using GymAppBackend.Core.ExerciseTypes.Entities;

namespace GymAppBackend.Core.ExerciseTypes.Repositories;

public interface IExerciseTypeRepository
{
    Task<ExerciseType?> GetAsync(Guid id);
}