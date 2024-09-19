namespace GymAppBackend.Core.Entities;

public class Workout
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public IEnumerable<ExerciseInWorkout> ExercisesInWorkout => _reservation;

    private readonly HashSet<ExerciseInWorkout> _reservation = new();

    public Workout(Guid id, DateTime createdAt)
    {
        Id = Guid.NewGuid();
        CreatedAt = createdAt;
    }

    public static Workout Create(Guid id, DateTime createdAt)
        => new Workout(id, createdAt);

    internal void AddExerciseInWorkout(ExerciseInWorkout exercisesInWorkout)
    {
        _reservation.Add(exercisesInWorkout);
    }
}