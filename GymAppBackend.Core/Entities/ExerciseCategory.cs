namespace GymAppBackend.Core.Entities;

public class ExerciseCategory
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Exercise> Exercises { get; set; }
}