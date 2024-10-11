using GymAppBackend.Core.Abstractions;
using GymAppBackend.Core.ExerciseCategories.Entities;

namespace GymAppBackend.Core.ExerciseTypes.Entities;

public class ExerciseType : Entity
{
    public string Name { get; set; }
    public ExerciseCategory ExerciseCategory { get; set; }
}