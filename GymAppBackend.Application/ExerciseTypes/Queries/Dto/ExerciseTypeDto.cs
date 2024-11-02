namespace GymAppBackend.Application.ExerciseTypes.Queries.Dto;

public class ExerciseTypeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid ExerciseCategoryId { get; set; }
}