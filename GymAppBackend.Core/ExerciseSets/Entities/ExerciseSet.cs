using GymAppBackend.Core.Abstractions;

namespace GymAppBackend.Core.ExerciseSets.Entities;

public class ExerciseSet : Entity
{
    public int SetNumber { get; set; }
    public int Quantity { get; set; }
    public int Reps { get; set; }
}