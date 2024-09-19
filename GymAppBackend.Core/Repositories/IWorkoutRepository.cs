using GymAppBackend.Core.Entities;

namespace GymAppBackend.Core.Repositories;

public interface IWorkoutRepository
{
    Task<IEnumerable<Workout>> GetWorkoutsAsync();
}