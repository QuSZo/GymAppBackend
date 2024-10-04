using GymAppBackend.Core.Abstractions;
using GymAppBackend.Core.ValueObjects;

namespace GymAppBackend.Core.Entities;

public class Workout : Entity
{
    public string Name { get; private set; }
    public Date Date { get; private set; }
    public IEnumerable<ExerciseInWorkout> ExerciseInWorkouts => _exerciseInWorkouts;

    private List<ExerciseInWorkout> _exerciseInWorkouts = new();

    private Workout(Guid id, Date date)
    {
        Id = id;
        Name = "Workout " + date;
        Date = date;
    }

    public static Workout Create(Guid id, Date date)
        => new Workout(id, date);

    public void AddExerciseInWorkout(ExerciseInWorkout exercisesInWorkout)
    {
        _exerciseInWorkouts.Add(exercisesInWorkout);
    }

    public void UpdateName(string name)
    {
        Name = name;
    }
}