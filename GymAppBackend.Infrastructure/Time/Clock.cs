using GymAppBackend.Core.Abstractions;

namespace GymAppBackend.Infrastructure.Time;

internal sealed class Clock : IClock
{
    public DateTime Current() => DateTime.UtcNow;
}