using GymAppBackend.Application.Exercises.Queries.DTO;
using GymAppBackend.Core.ValueObjects;

namespace GymAppBackend.Application.Workouts.Queries.DTO;

public class WorkoutDetailsDto
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public IEnumerable<ExerciseDto> Exercises { get; set; }
}