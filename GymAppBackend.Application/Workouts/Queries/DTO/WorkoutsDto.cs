using GymAppBackend.Core.ValueObjects;

namespace GymAppBackend.Application.Workouts.Queries.DTO;

public class WorkoutsDto
{
    public Guid Id { get; set; }
    public DateTimeOffset Date { get; set; }
}