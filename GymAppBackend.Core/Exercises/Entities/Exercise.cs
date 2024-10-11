using GymAppBackend.Core.Abstractions;
using GymAppBackend.Core.ExerciseSets.Entities;
using GymAppBackend.Core.ExerciseTypes.Entities;

namespace GymAppBackend.Core.Exercises.Entities;

public class Exercise : Entity
{
    public ExerciseType ExerciseType { get; set; }
    public List<ExerciseSet> ExerciseSets { get; set; }
}