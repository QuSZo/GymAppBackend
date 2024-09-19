namespace GymAppBackend.Core.Entities;

public class Exercise
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ExerciseCategory ExerciseCategory { get; set; }
}