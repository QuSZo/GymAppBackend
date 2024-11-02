using GymAppBackend.Core.ExerciseCategories.Entities;
using GymAppBackend.Core.ExerciseTypes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GymAppBackend.Infrastructure.DAL;

internal sealed class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseInitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GymAppDbContext>();
        await dbContext.Database.MigrateAsync(cancellationToken);

        if (!await dbContext.ExerciseCategories.AnyAsync(cancellationToken))
        {
            var exerciseCategories = ExerciseCategoriesToSeed();
            await dbContext.ExerciseCategories.AddRangeAsync(exerciseCategories);
            await dbContext.SaveChangesAsync();
        }

        if (!await dbContext.ExerciseTypes.AnyAsync(cancellationToken))
        {
            var exerciseTypes = ExerciseTypesToSeed();
            await dbContext.ExerciseTypes.AddRangeAsync(exerciseTypes);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    private IEnumerable<ExerciseCategory> ExerciseCategoriesToSeed()
    {
        var exerciseCategories = new List<ExerciseCategory>
        {
            ExerciseCategory.Create(new Guid("6edd345a-4b17-4d0f-ba3e-9493c883f8c9"), "Barki"),
            ExerciseCategory.Create(new Guid("4bb407c0-3ec0-424a-80bb-70f6f2fa3aac"), "Klatka piersiowa"),
            ExerciseCategory.Create(new Guid("5c75f320-63f7-4080-94fd-86047de1f3ec"), "Plecy"),
            ExerciseCategory.Create(new Guid("7b0aad59-0cec-42f4-83c5-4648b43e002a"), "Biceps"),
            ExerciseCategory.Create(new Guid("79910dd8-a2d3-4211-9976-75561e10615f"), "Triceps"),
            ExerciseCategory.Create(new Guid("b1ba7768-d731-4294-8529-b430042ea8ee"), "Brzuch"),
            ExerciseCategory.Create(new Guid("250950ff-6574-47cb-9a3f-2013d9a0ee6e"), "Nogi"),
        };

        return exerciseCategories;
    }

    private IEnumerable<ExerciseType> ExerciseTypesToSeed()
    {
        var exerciseTypes = new List<ExerciseType>
        {
            ExerciseType.Create(
                new Guid("1417cbaa-eb75-4491-a62d-fe6b15059b09"),
                "Wyciskanie na ławce poziomej",
                new Guid("4bb407c0-3ec0-424a-80bb-70f6f2fa3aac")
            ),
            ExerciseType.Create(
                new Guid("213c7324-0e2d-410b-bdeb-228a57e4bdcd"),
                "Wyciskanie hantli na ławce dodatniej",
                new Guid("4bb407c0-3ec0-424a-80bb-70f6f2fa3aac")
            ),
            ExerciseType.Create(
                new Guid("136e43f4-f36b-4585-9015-915736c30def"),
                "Prostowanie ramion z linkami wyciągu górnego",
                new Guid("79910dd8-a2d3-4211-9976-75561e10615f")
            ),
            ExerciseType.Create(
                new Guid("072f88ff-f2c0-49cb-8bfb-771fd5bbd1ff"),
                "Unoszenie hantli bokiem",
                new Guid("6edd345a-4b17-4d0f-ba3e-9493c883f8c9")
            ),
            ExerciseType.Create(
                new Guid("16657081-5f2a-4664-b6f3-2968db301383"),
                "Unoszenie ramion w przód",
                new Guid("6edd345a-4b17-4d0f-ba3e-9493c883f8c9")
            ),
            ExerciseType.Create(
                new Guid("216e1a22-a5e5-4a95-b5f1-15244ffd3cbd"),
                "Odwodzenie linek w tył wyciągu stojąc",
                new Guid("6edd345a-4b17-4d0f-ba3e-9493c883f8c9")
            ),
            ExerciseType.Create(
                new Guid("d29f2623-c754-4332-ae6c-211271c1206d"),
                "Podciąganie na drążku trzymanym nachwytem",
                new Guid("5c75f320-63f7-4080-94fd-86047de1f3ec")
            ),
            ExerciseType.Create(
                new Guid("190ef1ad-12a0-4e07-b83b-12a4dd301194"),
                "Wiosłowanie na maszynie siedząc",
                new Guid("5c75f320-63f7-4080-94fd-86047de1f3ec")
            ),
            ExerciseType.Create(
                new Guid("5e4a32aa-60c5-4fe5-a49a-923e670501de"),
                "Narciarz na wyciągu",
                new Guid("5c75f320-63f7-4080-94fd-86047de1f3ec")
            ),
            ExerciseType.Create(
                new Guid("144e738e-057a-462b-b038-474e415d3d34"),
                "Uginanie ramion z hantlami",
                new Guid("7b0aad59-0cec-42f4-83c5-4648b43e002a")
            ),
            ExerciseType.Create(
                new Guid("09703975-7300-46cf-9f6c-13dc2ea5f7e6"),
                "Uginanie ramion ze sztangą łamaną",
                new Guid("7b0aad59-0cec-42f4-83c5-4648b43e002a")
            ),
        };

        return exerciseTypes;
    }
}