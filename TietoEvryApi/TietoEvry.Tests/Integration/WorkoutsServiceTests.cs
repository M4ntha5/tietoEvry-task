using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TietoEvry.Core.Workouts.Services;
using TietoEvry.Core.Workouts.Services.Interfaces;
using TietoEvry.Data.Contexts;
using TietoEvry.Data.Models;

namespace TietoEvry.Tests.Integration;

public class WorkoutsServiceTests
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IWorkoutService _workoutService;

    public WorkoutsServiceTests()
    {
        var serviceCollection = new ServiceCollection();
        
        serviceCollection.AddDbContext<ApplicationDbContext>(o =>
        {
            o.UseInMemoryDatabase("TestDB");
        });

        var serviceProvider = serviceCollection.BuildServiceProvider(); 
        
        _applicationDbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
        var mapper = new MapperConfiguration(m => { });
        
        _workoutService = new WorkoutService(_applicationDbContext, mapper.CreateMapper());
    }


    [Fact]
    public async Task GetSummaryShouldSuccessfullyReturnSummary()
    {
        var workout = new Workout()
        {
            Description = "test",
            Title = "test title",
            Exercises = new List<Exercise>()
            {
                new Exercise()
                {
                    Name = "Ex 1",
                    Reps = 10,
                    Sets = 3,
                    DurationInMinutes = 5
                },
                new Exercise()
                {
                    Name = "Ex 2",
                    Reps = 10,
                    Sets = 3,
                    DurationInMinutes = 5
                }
            }
        };
        

         await _applicationDbContext.Workouts.AddAsync(workout);
         await _applicationDbContext.SaveChangesAsync();
        
         var summary = await _workoutService.GetSummary(workout.Id);
        
         Assert.Equal(6, summary.TotalSets);
         Assert.Equal(60, summary.TotalReps);
         Assert.Equal(10, summary.TotalDurationInMinutes);
    }
}
