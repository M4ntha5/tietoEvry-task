using Microsoft.Extensions.DependencyInjection;
using TietoEvry.Core.Exercises.Services;
using TietoEvry.Core.Exercises.Services.Interfaces;

namespace TietoEvry.Core.Exercises;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddExerciseServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IExerciseService, ExerciseService>();

        return serviceCollection;
    }
}