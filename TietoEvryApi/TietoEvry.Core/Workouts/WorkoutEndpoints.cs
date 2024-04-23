using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TietoEvry.Core.Workouts.Dtos;
using TietoEvry.Core.Workouts.Services.Interfaces;

namespace TietoEvry.Core.Workouts;

public static class WorkoutEndpoints
{
    public static void AddWorkoutEndpoints(this IEndpointRouteBuilder app)
    {
        var workoutItems = app.MapGroup("/workouts").WithTags("Workouts");
        
        workoutItems.MapGet("/", async ([FromServices] IWorkoutService workoutService) =>
                Results.Ok(await workoutService.GetAll())
        );

        workoutItems.MapGet("/{id:int}", async (int id, [FromServices] IWorkoutService workoutService) =>
                Results.Ok(await workoutService.GetById(id))
        );

        workoutItems.MapPost("/", async (CreateWorkoutRequestDto request,[FromServices] IWorkoutService workoutService) =>
        {
            var workout = await workoutService.Create(request);

            return Results.Created($"/{workout.Id}", workout);
        });
        
        workoutItems.MapDelete("/{id:int}", async (int id, [FromServices] IWorkoutService workoutService) =>
            {
                await workoutService.Delete(id);
                
                return Results.NoContent();
            }
        );
        
        workoutItems.MapPut("/{id:int}", async (int id, LinkDateToWorkoutRequestDto request, [FromServices] IWorkoutService workoutService) =>
        {
            await workoutService.Update(id, request);
            return Results.NoContent();
        }).WithTags("Link To Date");
        
        
        workoutItems.MapGet("/{id:int}/summary", async (int id, [FromServices] IWorkoutService workoutService) =>
                Results.Ok(await workoutService.GetSummary(id))
        ).WithTags("Summary");
        
        workoutItems.MapGet("/history", async ([FromQuery(Name = "Date")] DateTime date, [FromServices] IWorkoutService workoutService) =>
            {
                var dateSummary = await workoutService.GetAllByDate(date);
                return Results.Ok(dateSummary);
            }
        ).WithTags("Calendar Feature");
        
    }
}