using GymAppBackend.Core.ValueObjects;

namespace GymAppBackend.Application.Queries.Workouts.DTO;

public class WorkoutsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTimeOffset Date { get; set; }
}