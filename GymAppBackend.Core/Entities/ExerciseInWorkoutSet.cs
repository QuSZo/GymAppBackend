namespace GymAppBackend.Core.Entities;

public class ExerciseInWorkoutSet
{
    public Guid Id { get; set; }
    public int SetNumber { get; set; }
    public int Quantity { get; set; }
    public int Reps { get; set; }
}