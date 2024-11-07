using GymAppBackend.Core.Abstractions;
using GymAppBackend.Core.Exercises.Entities;
using GymAppBackend.Core.ValueObjects;
using GymAppBackend.Core.ValueObjects.Date;

namespace GymAppBackend.Core.Workouts.Entities;

public class Workout : Entity
{
    public Date Date { get; private set; }
    public IEnumerable<Exercise> Exercises => _exercises;

    private List<Exercise> _exercises = new();

    private Workout(Guid id, Date date)
    {
        Id = id;
        Date = date;
    }

    public static Workout Create(Guid id, Date date)
        => new Workout(id, date);

    public void AddExercise(Exercise exercises)
    {
        _exercises.Add(exercises);
    }
}