using GymAppBackend.Core.Abstractions;

namespace GymAppBackend.Core.Entities;

public class Exercise : Entity
{
    public string Name { get; set; }
    public ExerciseCategory ExerciseCategory { get; set; }
}