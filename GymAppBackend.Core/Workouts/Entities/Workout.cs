using GymAppBackend.Core.Abstractions;
using GymAppBackend.Core.Exercises.Entities;
using GymAppBackend.Core.ValueObjects;

namespace GymAppBackend.Core.Workouts.Entities;

public class Workout : Entity
{
    public string Name { get; private set; }
    public Date Date { get; private set; }
    public IEnumerable<Exercise> Exercises => _exercises;

    private List<Exercise> _exercises = new();

    private Workout(Guid id, Date date)
    {
        Id = id;
        Name = "Workout " + date;
        Date = date;
    }

    public static Workout Create(Guid id, Date date)
        => new Workout(id, date);

    public void AddExerciseInWorkout(Exercise exercises)
    {
        _exercises.Add(exercises);
    }

    public void UpdateName(string name)
    {
        Name = name;
    }
}