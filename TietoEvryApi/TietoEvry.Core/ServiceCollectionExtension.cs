using Microsoft.Extensions.DependencyInjection;
using TietoEvry.Core.Exercises;
using TietoEvry.Core.Workouts;

namespace TietoEvry.Core;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddWorkoutServices();
        serviceCollection.AddExerciseServices();
        
        return serviceCollection;
    }
}