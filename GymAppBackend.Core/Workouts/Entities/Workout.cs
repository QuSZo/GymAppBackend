using GymAppBackend.Core.Abstractions;
using GymAppBackend.Core.Exercises.Entities;
using GymAppBackend.Core.Users.Entities;
using GymAppBackend.Core.ValueObjects;
using GymAppBackend.Core.ValueObjects.Date;

namespace GymAppBackend.Core.Workouts.Entities;

public class Workout : Entity
{
    public Date Date { get; private set; }
    public User User { get; private set; }
    public IEnumerable<Exercise> Exercises => _exercises;

    private List<Exercise> _exercises = new();

    private Workout()
    {
    }

    private Workout(Guid id, Date date, User user)
    {
        Id = id;
        Date = date;
        User = user;
    }

    public static Workout Create(Guid id, Date date, User user)
        => new Workout(id, date, user);

    public void AddExercise(Exercise exercises)
    {
        _exercises.Add(exercises);
    }
}