using GymAppBackend.Core.ExerciseSets.Entities;

namespace GymAppBackend.Core.ExerciseSets.Repositories;

public interface IExerciseSetRepository
{
    public Task<ExerciseSet?> GetAsync(Guid id);
    public Task AddAsync(ExerciseSet exerciseSet);
    public Task UpdateAsync(ExerciseSet exerciseSet);
    public Task DeleteAsync(ExerciseSet exerciseSet);
}