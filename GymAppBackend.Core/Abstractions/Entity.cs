namespace GymAppBackend.Core.Abstractions;

public abstract class Entity
{
    public Guid Id { get; protected set; }
}