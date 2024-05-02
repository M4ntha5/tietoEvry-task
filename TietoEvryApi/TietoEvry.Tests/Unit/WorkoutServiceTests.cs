using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using TietoEvry.Core.Workouts.Services;
using TietoEvry.Core.Workouts.Services.Interfaces;
using TietoEvry.Data.Contexts;
using TietoEvry.Data.Models;

namespace TietoEvry.Tests.Unit;

public class WorkoutServiceTests
{
    private readonly Mock<ApplicationDbContext> _applicationDbContextMock;
    private readonly IWorkoutService _workoutService;

    public WorkoutServiceTests()
    {
        _applicationDbContextMock = new Mock<ApplicationDbContext>();
        var mapper = new MapperConfiguration(m => { });
        
        _workoutService = new WorkoutService(_applicationDbContextMock.Object, mapper.CreateMapper());
    }
    
    [Fact]
    public async Task MyTest()
    {
        var workouts = new List<Workout>()
        {
            new Workout()
            {
                Id = 1,
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
            }
        };
        
        _applicationDbContextMock.Setup<DbSet<Workout>>(x => x.Workouts).ReturnsDbSet(workouts);
        
        var summary  = await _workoutService.GetSummary(1);
        
        Assert.Equal(6, summary.TotalSets);
        Assert.Equal(60, summary.TotalReps);
        Assert.Equal(10, summary.TotalDurationInMinutes);
    }
}