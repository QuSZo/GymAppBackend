using GymAppBackend.Application.Abstractions;

namespace GymAppBackend.Application.Commands.CreateWorkout;

public class CreateWorkoutHandler : ICommandHandler<CreateWorkoutCommand>
{
    public async Task HandleAsync(CreateWorkoutCommand command)
    {
        throw new NotImplementedException();
    }
}