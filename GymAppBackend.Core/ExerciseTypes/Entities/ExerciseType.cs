using GymAppBackend.Core.Abstractions;
using GymAppBackend.Core.ExerciseCategories.Entities;

namespace GymAppBackend.Core.ExerciseTypes.Entities;

public class ExerciseType : Entity
{
    public string Name { get; set; }
    public ExerciseCategory ExerciseCategory { get; set; }

    private ExerciseType(Guid id, string name, ExerciseCategory exerciseCategory)
    {
        Id = id;
        Name = name;
        ExerciseCategory = exerciseCategory;
    }

    public static ExerciseType Create(Guid id, string name, ExerciseCategory exerciseCategory) => new(id, name, exerciseCategory);
}