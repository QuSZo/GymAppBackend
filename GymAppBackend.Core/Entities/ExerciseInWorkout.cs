using GymAppBackend.Core.Abstractions;

namespace GymAppBackend.Core.Entities;

public class ExerciseInWorkout : Entity
{
    public Exercise Exercise { get; set; }
    public List<ExerciseInWorkoutSet> ExerciseInWorkoutSets { get; set; }
}