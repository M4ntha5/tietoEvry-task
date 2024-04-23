using Microsoft.Extensions.DependencyInjection;
using TietoEvry.Core.Workouts.Services;
using TietoEvry.Core.Workouts.Services.Interfaces;

namespace TietoEvry.Core.Workouts;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddWorkoutServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IWorkoutService, WorkoutService>();

        return serviceCollection;
    }
}