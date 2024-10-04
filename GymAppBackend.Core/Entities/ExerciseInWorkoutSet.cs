using GymAppBackend.Core.Abstractions;

namespace GymAppBackend.Core.Entities;

public class ExerciseInWorkoutSet : Entity
{
    public int SetNumber { get; set; }
    public int Quantity { get; set; }
    public int Reps { get; set; }
}