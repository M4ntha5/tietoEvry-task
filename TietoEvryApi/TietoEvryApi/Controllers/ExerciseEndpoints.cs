using Microsoft.AspNetCore.Mvc;
using TietoEvry.Core.Exercises.Dtos;
using TietoEvry.Core.Exercises.Services.Interfaces;

namespace TietoEvryApi.Controllers;

public static class ExerciseEndpoints
{
    public static void AddExerciseEndpoints(this IEndpointRouteBuilder app)
    {
        var exerciseItems = app.MapGroup("/exercises").WithTags("Exercises");

        exerciseItems.MapGet("/", 
            async ([FromServices] IExerciseService exerciseService) => 
            Results.Ok(await exerciseService.GetAll()));

        exerciseItems.MapGet("/{id:int}",
            async (int id, [FromServices] IExerciseService exerciseService) =>
            Results.Ok(await exerciseService.GetById(id))
        );

        exerciseItems.MapPost("/", async (CreateExerciseRequestDto request, [FromServices]IExerciseService exerciseService) =>
        {
            var exercise = await exerciseService.Create(request);
            return Results.Created($"/{exercise.Id}", exercise);
        });

        exerciseItems.MapDelete("/{id:int}", async (int id, [FromServices]IExerciseService exerciseService) =>
            {
                await exerciseService.Delete(id);
                return Results.NoContent();
            }
        );
    }
}