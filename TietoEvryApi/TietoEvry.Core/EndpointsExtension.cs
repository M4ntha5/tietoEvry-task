using Microsoft.AspNetCore.Routing;
using TietoEvry.Core.Exercises;
using TietoEvry.Core.Workouts;

namespace TietoEvry.Core;

public static class EndpointsExtension
{
    public static void AddEndpoints(this IEndpointRouteBuilder app)
    {
        app.AddExerciseEndpoints();
        app.AddWorkoutEndpoints();
    }
}