using GymAppBackend.Core.Abstractions;
using GymAppBackend.Core.ExerciseSets.Entities;
using GymAppBackend.Core.ExerciseTypes.Entities;
using GymAppBackend.Core.Workouts.Entities;

namespace GymAppBackend.Core.Exercises.Entities;

public class Exercise : Entity
{
    public int ExerciseNumber { get; set; }
    public ExerciseType ExerciseType { get; set; }

    public Workout Workout { get; set; }
    public IEnumerable<ExerciseSet> ExerciseSets => _exerciseSets;

    private List<ExerciseSet> _exerciseSets = new();

    private Exercise(Guid id, int exerciseNumber, ExerciseType exerciseType)
    {
        Id = id;
        ExerciseNumber = exerciseNumber;
        ExerciseType = exerciseType;
    }

    public static Exercise Create(Guid id, int exerciseNumber, ExerciseType exerciseType) => new(id, exerciseNumber, exerciseType);

    public void AddExerciseSet(ExerciseSet exerciseSet)
    {
        _exerciseSets.Add(exerciseSet);
    }
}