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
                "Wyciskanie sztangi na ławce poziomej",
                new Guid("4bb407c0-3ec0-424a-80bb-70f6f2fa3aac")
            ),
            ExerciseType.Create(
                new Guid("7498f505-34d1-402f-a6bf-9f47fea5a66b"),
                "Wyciskanie sztangi na ławce skośnej",
                new Guid("4bb407c0-3ec0-424a-80bb-70f6f2fa3aac")
            ),
            ExerciseType.Create(
                new Guid("213c7324-0e2d-410b-bdeb-228a57e4bdcd"),
                "Wyciskanie hantli na ławce skośnej",
                new Guid("4bb407c0-3ec0-424a-80bb-70f6f2fa3aac")
            ),
            ExerciseType.Create(
                new Guid("80588b2c-cdec-4d94-ab9f-29dab9a070dc"),
                "Wyciskanie hantli na ławce poziomej",
                new Guid("4bb407c0-3ec0-424a-80bb-70f6f2fa3aac")
            ),
            ExerciseType.Create(
                new Guid("4718354f-4836-4dc0-8060-2a32c415c06c"),
                "Rozpiętki na ławce poziomej",
                new Guid("4bb407c0-3ec0-424a-80bb-70f6f2fa3aac")
            ),
            ExerciseType.Create(
                new Guid("9b55d46d-9286-473e-9b04-8513e0b0283e"),
                "Pompki na poręczach (dipy)",
                new Guid("4bb407c0-3ec0-424a-80bb-70f6f2fa3aac")
            ),
            ExerciseType.Create(
                new Guid("dfd3c644-6528-4eff-b699-64017c5dc107"),
                "Wyciskanie na maszynie hammer",
                new Guid("4bb407c0-3ec0-424a-80bb-70f6f2fa3aac")
            ),
            ExerciseType.Create(
                new Guid("9e13b35f-540c-43a0-a448-a81559581f67"),
                "Rozpiętki na maszynie",
                new Guid("4bb407c0-3ec0-424a-80bb-70f6f2fa3aac")
            ),
            ExerciseType.Create(
                new Guid("136e43f4-f36b-4585-9015-915736c30def"),
                "Prostowanie ramion z linkami wyciągu górnego",
                new Guid("79910dd8-a2d3-4211-9976-75561e10615f")
            ),
            ExerciseType.Create(
                new Guid("784664ce-549a-4e54-a60e-f13ec8ad7f65"),
                "Wyciskanie francuskie",
                new Guid("79910dd8-a2d3-4211-9976-75561e10615f")
            ),
            ExerciseType.Create(
                new Guid("09a2b02c-0d3c-42dd-8c7a-c5066a84b7f3"),
                "Wyciskanie sztangi wąskim chwytem",
                new Guid("79910dd8-a2d3-4211-9976-75561e10615f")
            ),
            ExerciseType.Create(
                new Guid("0a554be6-b7c9-442b-8cf6-1367ba5522df"),
                "Wyciskanie hantelki zza głowy",
                new Guid("79910dd8-a2d3-4211-9976-75561e10615f")
            ),
            ExerciseType.Create(
                new Guid("1bb2e953-7dcc-4682-b816-a0ec8e0ec6c7"),
                "Prostowanie ramienia w opadzie tułowia",
                new Guid("79910dd8-a2d3-4211-9976-75561e10615f")
            ),
            ExerciseType.Create(
                new Guid("7ee88f91-db55-46ae-89ec-dbf3b8903555"),
                "Pompki na poręczach (dipy)",
                new Guid("79910dd8-a2d3-4211-9976-75561e10615f")
            ),
            ExerciseType.Create(
                new Guid("bb34b742-cd44-4f5c-85ee-90b7fd0bf941"),
                "Prostowanie ramienia leżąc",
                new Guid("79910dd8-a2d3-4211-9976-75561e10615f")
            ),
            ExerciseType.Create(
                new Guid("469a9bd2-de39-415d-844f-f9a403e3fccb"),
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
                "Unoszenie hantli w przód",
                new Guid("6edd345a-4b17-4d0f-ba3e-9493c883f8c9")
            ),
            ExerciseType.Create(
                new Guid("c925f574-915b-4720-aea1-897a6c3d0b03"),
                "Unoszenie hantli w opadzie tułowia",
                new Guid("6edd345a-4b17-4d0f-ba3e-9493c883f8c9")
            ),
            ExerciseType.Create(
                new Guid("66cba7fc-1814-4dda-a70e-3eb18f789ce9"),
                "Wyciskanie żołnierskie",
                new Guid("6edd345a-4b17-4d0f-ba3e-9493c883f8c9")
            ),
            ExerciseType.Create(
                new Guid("054696ee-5f48-484a-8961-b0df3e1bc8fc"),
                "Wyciskanie hantli (Arnoldki)",
                new Guid("6edd345a-4b17-4d0f-ba3e-9493c883f8c9")
            ),
            ExerciseType.Create(
                new Guid("9b2c97c3-6e36-4d09-bd32-74794254cd60"),
                "Wyciskanie na maszynie hammer",
                new Guid("6edd345a-4b17-4d0f-ba3e-9493c883f8c9")
            ),
            ExerciseType.Create(
                new Guid("d03d2147-47d8-4d90-9f22-10a13225543a"),
                "Ściąganie linek od dołu na bramie",
                new Guid("6edd345a-4b17-4d0f-ba3e-9493c883f8c9")
            ),
            ExerciseType.Create(
                new Guid("216e1a22-a5e5-4a95-b5f1-15244ffd3cbd"),
                "Ściąganie linek w tył wyciągu stojąc",
                new Guid("6edd345a-4b17-4d0f-ba3e-9493c883f8c9")
            ),
            ExerciseType.Create(
                new Guid("d29f2623-c754-4332-ae6c-211271c1206d"),
                "Podciąganie na drążku",
                new Guid("5c75f320-63f7-4080-94fd-86047de1f3ec")
            ),
            ExerciseType.Create(
                new Guid("190ef1ad-12a0-4e07-b83b-12a4dd301194"),
                "Wiosłowanie na maszynie siedząc",
                new Guid("5c75f320-63f7-4080-94fd-86047de1f3ec")
            ),
            ExerciseType.Create(
                new Guid("5e4a32aa-60c5-4fe5-a49a-923e670501de"),
                "Ściąganie na wyciągu (narciarz)",
                new Guid("5c75f320-63f7-4080-94fd-86047de1f3ec")
            ),
            ExerciseType.Create(
                new Guid("e76986ba-0a2e-4745-9449-bb24b3ee5ba7"),
                "Półsztanga",
                new Guid("5c75f320-63f7-4080-94fd-86047de1f3ec")
            ),
            ExerciseType.Create(
                new Guid("96ffa9d5-82f6-4e07-8595-1bdf65f1b5b5"),
                "Podciąganie sztangi w opadzie tułowia",
                new Guid("5c75f320-63f7-4080-94fd-86047de1f3ec")
            ),
            ExerciseType.Create(
                new Guid("a134072e-3e45-4d1f-b615-47cd3c9dd010"),
                "Wiosłowanie hantelkami",
                new Guid("5c75f320-63f7-4080-94fd-86047de1f3ec")
            ),
            ExerciseType.Create(
                new Guid("d371f98a-903e-48ea-8c33-6dfaadcc3616"),
                "Ściąganie linki wyciągu górnego",
                new Guid("5c75f320-63f7-4080-94fd-86047de1f3ec")
            ),
            ExerciseType.Create(
                new Guid("88161ba5-4227-4924-9ba0-da0a74e5a29b"),
                "Ściąganie poziome szeroko na wyciągu",
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
            ExerciseType.Create(
                new Guid("1913274e-80be-4417-bd80-b6cfbdddc655"),
                "Uginanie ramion siedząc na ławce skośnej",
                new Guid("7b0aad59-0cec-42f4-83c5-4648b43e002a")
            ),
            ExerciseType.Create(
                new Guid("e28ff8bb-80ed-4156-8096-8b5f49708b24"),
                "Uginanie ramion na bramie",
                new Guid("7b0aad59-0cec-42f4-83c5-4648b43e002a")
            ),
            ExerciseType.Create(
                new Guid("cb24998f-f051-474e-896e-5aa731cdf3f9"),
                "Uginanie ramion z hantlami w oparciu o kolano",
                new Guid("7b0aad59-0cec-42f4-83c5-4648b43e002a")
            ),
            ExerciseType.Create(
                new Guid("2a675da1-ecd6-416e-b2d6-84449ed5349b"),
                "Uginanie ramion na wyciągu",
                new Guid("7b0aad59-0cec-42f4-83c5-4648b43e002a")
            ),
            ExerciseType.Create(
                new Guid("8f0d1326-1de6-4176-8167-94e067170c5e"),
                "Uginanie ramion na modlitewniku",
                new Guid("7b0aad59-0cec-42f4-83c5-4648b43e002a")
            ),
            ExerciseType.Create(
                new Guid("a39d7acd-63af-4d65-bb02-834fc4c46851"),
                "Unoszenie tułowia na ławce",
                new Guid("b1ba7768-d731-4294-8529-b430042ea8ee")
            ),
            ExerciseType.Create(
                new Guid("6923637e-6487-4f46-85b8-ba95376500b7"),
                "Unoszenie nóg",
                new Guid("b1ba7768-d731-4294-8529-b430042ea8ee")
            ),
            ExerciseType.Create(
                new Guid("4f1f7f1d-5c14-47c1-98c3-97f9b5ecfe4d"),
                "Allahy na wyciągu",
                new Guid("b1ba7768-d731-4294-8529-b430042ea8ee")
            ),
            ExerciseType.Create(
                new Guid("6b89e622-bf02-444e-bf66-26b70a64a2db"),
                "Spinanie brzucha na maszynie",
                new Guid("b1ba7768-d731-4294-8529-b430042ea8ee")
            ),
            ExerciseType.Create(
                new Guid("83ee043f-b4c7-4759-add8-b529d5426ba0"),
                "Plank",
                new Guid("b1ba7768-d731-4294-8529-b430042ea8ee")
            ),
            ExerciseType.Create(
                new Guid("0e9162cd-9790-48f3-bcc4-6e7ad53e97c5"),
                "Przysiady ze sztangą",
                new Guid("250950ff-6574-47cb-9a3f-2013d9a0ee6e")
            ),
            ExerciseType.Create(
                new Guid("edb5bf49-3bb7-42d3-bc86-4637567f2e1e"),
                "Martwy ciąg",
                new Guid("250950ff-6574-47cb-9a3f-2013d9a0ee6e")
            ),
            ExerciseType.Create(
                new Guid("b862db97-8e21-4521-a41b-64f399307ee4"),
                "Wykroki",
                new Guid("250950ff-6574-47cb-9a3f-2013d9a0ee6e")
            ),
            ExerciseType.Create(
                new Guid("0b0d4c96-efe6-480b-b188-1cc71b7da153"),
                "Wyciskanie na suwnicy",
                new Guid("250950ff-6574-47cb-9a3f-2013d9a0ee6e")
            ),
            ExerciseType.Create(
                new Guid("a5ff95c5-ce7c-4bd4-8b05-30629acdfb99"),
                "Prostowanie na maszynie",
                new Guid("250950ff-6574-47cb-9a3f-2013d9a0ee6e")
            ),
            ExerciseType.Create(
                new Guid("3aced380-01a2-41f4-a7e4-bd0743ab720f"),
                "Uginanie na maszynie",
                new Guid("250950ff-6574-47cb-9a3f-2013d9a0ee6e")
            ),
            ExerciseType.Create(
                new Guid("c0f3f154-3157-4077-bba2-a2f8555b8078"),
                "Spięcia łydek ma maszynie",
                new Guid("250950ff-6574-47cb-9a3f-2013d9a0ee6e")
            ),
            ExerciseType.Create(
                new Guid("2904cc4c-8a87-4d1d-8da8-f4bfd8701bba"),
                "Spięcia łydek z handelkami",
                new Guid("250950ff-6574-47cb-9a3f-2013d9a0ee6e")
            ),
        };

        return exerciseTypes;
    }
}