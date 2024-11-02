using GymAppBackend.Core.Abstractions;
using GymAppBackend.Core.ExerciseTypes.Entities;

namespace GymAppBackend.Core.ExerciseCategories.Entities;

public class ExerciseCategory : Entity
{
    public string Name { get; private set; }
    public IEnumerable<ExerciseType> ExerciseTypes => _exerciseTypes;

    private readonly List<ExerciseType> _exerciseTypes = new();

    private ExerciseCategory(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public static ExerciseCategory Create(Guid id, string name) => new(id, name);
}