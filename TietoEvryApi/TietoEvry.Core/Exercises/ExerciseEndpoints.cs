using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TietoEvry.Core.Exercises.Dtos;
using TietoEvry.Core.Exercises.Services.Interfaces;

namespace TietoEvry.Core.Exercises;

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