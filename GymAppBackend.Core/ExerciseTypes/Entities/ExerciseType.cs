using GymAppBackend.Core.Abstractions;
using GymAppBackend.Core.ExerciseCategories.Entities;

namespace GymAppBackend.Core.ExerciseTypes.Entities;

public class ExerciseType : Entity
{
    public string Name { get; private set; }
    public Guid ExerciseCategoryId { get; private set; }
    public ExerciseCategory ExerciseCategory { get; private set; }

    public ExerciseType()
    {
    }

    private ExerciseType(Guid id, string name, Guid exerciseCategoryId)
    {
        Id = id;
        Name = name;
        ExerciseCategoryId = exerciseCategoryId;
    }

    public static ExerciseType Create(Guid id, string name, Guid exerciseCategoryId) => new(id, name, exerciseCategoryId);
}