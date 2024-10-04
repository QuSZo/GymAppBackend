using GymAppBackend.Core.Abstractions;

namespace GymAppBackend.Core.Entities;

public class ExerciseCategory : Entity
{
    public string Name { get; set; }
    public List<Exercise> Exercises { get; set; }
}