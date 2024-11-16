using GymAppBackend.Core.Abstractions;
using GymAppBackend.Core.ExerciseSets.Entities;
using GymAppBackend.Core.ExerciseTypes.Entities;
using GymAppBackend.Core.Workouts.Entities;

namespace GymAppBackend.Core.Exercises.Entities;

public class Exercise : Entity
{
    public int ExerciseNumber { get; private set; }

    public ExerciseType ExerciseType { get; private set; }
    public Workout Workout { get; private set; }
    public IEnumerable<ExerciseSet> ExerciseSets => _exerciseSets;

    private List<ExerciseSet> _exerciseSets = new();

    private Exercise()
    {
    }

    private Exercise(Guid id, int exerciseNumber, ExerciseType exerciseType, Workout workout)
    {
        Id = id;
        ExerciseNumber = exerciseNumber;
        ExerciseType = exerciseType;
        Workout = workout;
    }

    public static Exercise Create(Guid id, int exerciseNumber, ExerciseType exerciseType, Workout workout)
        => new(id, exerciseNumber, exerciseType, workout);

    public void AddExerciseSet(ExerciseSet exerciseSet)
    {
        _exerciseSets.Add(exerciseSet);
    }

    public void UpdateExerciseNumber(int exerciseNumber)
    {
        ExerciseNumber = exerciseNumber;
    }
}