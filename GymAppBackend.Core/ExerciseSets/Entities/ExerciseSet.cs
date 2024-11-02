using GymAppBackend.Core.Abstractions;
using GymAppBackend.Core.Exercises.Entities;

namespace GymAppBackend.Core.ExerciseSets.Entities;

public class ExerciseSet : Entity
{
    public int SetNumber { get; private set; }
    public int Quantity { get; private set; }
    public int Reps { get; private set; }

    public Exercise Exercise { get; private set; }

    private ExerciseSet()
    {
    }

    private ExerciseSet(Guid id, int setNumber, int quantity, int reps, Exercise exercise)
    {
        Id = id;
        SetNumber = setNumber;
        Quantity = quantity;
        Reps = reps;
        Exercise = exercise;
    }

    public static ExerciseSet Create(Guid id, int setNumber, int quantity, int reps, Exercise exercise)
        => new(id, setNumber, quantity, reps, exercise);

    public void Update(int quantity, int reps)
    {
        Quantity = quantity;
        Reps = reps;
    }
}