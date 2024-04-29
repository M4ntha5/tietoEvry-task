namespace TietoEvryApi.Controllers;

public static class EndpointsExtension
{
    public static void AddEndpoints(this IEndpointRouteBuilder app)
    {
        app.AddExerciseEndpoints();
        app.AddWorkoutEndpoints();
    }
}