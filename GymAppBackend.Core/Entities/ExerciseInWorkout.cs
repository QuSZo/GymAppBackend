namespace GymAppBackend.Core.Entities;

public class ExerciseInWorkout
{
    public Guid Id { get; set; }
    public Exercise Exercise { get; set; }
    public List<ExerciseInWorkoutSet> ExerciseInWorkoutSets { get; set; }
}