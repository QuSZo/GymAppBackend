using GymAppBackend.Core.Abstractions;
using GymAppBackend.Core.ExerciseTypes.Entities;

namespace GymAppBackend.Core.ExerciseCategories.Entities;

public class ExerciseCategory : Entity
{
    public string Name { get; set; }
    public List<ExerciseType> ExerciseTypes { get; set; }
}