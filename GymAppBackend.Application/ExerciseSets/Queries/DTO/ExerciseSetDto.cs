namespace GymAppBackend.Application.ExerciseSets.Queries.DTO;

public class ExerciseSetDto
{
    public Guid Id { get; set; }
    public int SetNumber { get; set; }
    public int Quantity { get; set; }
    public int Reps { get; set; }
}