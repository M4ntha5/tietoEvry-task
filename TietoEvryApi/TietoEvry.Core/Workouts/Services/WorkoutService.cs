using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TietoEvry.Core.Exercises.Dtos;
using TietoEvry.Core.Workouts.Dtos;
using TietoEvry.Core.Workouts.Services.Interfaces;
using TietoEvry.Data.Contexts;
using TietoEvry.Data.Models;

namespace TietoEvry.Core.Workouts.Services;

//TODO test needed
public class WorkoutService : IWorkoutService
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;

    public WorkoutService(ApplicationDbContext applicationDbContext, IMapper mapper)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }
    
    public async Task<ICollection<WorkoutDto>> GetAll()
    {
        var workouts = await _applicationDbContext.Workouts
            .Include(w => w.Exercises)
            .ToListAsync();
        return _mapper.Map<ICollection<WorkoutDto>>(workouts);
    }

    public async Task<WorkoutDto> GetById(int id)
    {
        var workout = await _applicationDbContext.Workouts
            .Include(w => w.Exercises)
            .FirstAsync(w => w.Id == id);
        return _mapper.Map<WorkoutDto>(workout);
    }

    public async Task<WorkoutDto> Create(CreateWorkoutRequestDto request)
    {
        var workout = _mapper.Map<Workout>(request);
        await _applicationDbContext.AddAsync(workout);
        await _applicationDbContext.SaveChangesAsync();
        
        return await GetById(workout.Id);
    }

    public async Task Delete(int id)
    {
        var workout = await _applicationDbContext.Workouts
            .Include(w => w.Exercises)
            .FirstAsync(w => w.Id == id);
                
        _applicationDbContext.Remove(workout);
        await _applicationDbContext.SaveChangesAsync();
    }

    public async Task<SummaryDto> GetSummary(int workoutId)
    {
        var workout = await _applicationDbContext.Workouts
            .Include(w => w.Exercises)
            .FirstAsync(w => w.Id == workoutId);

        var (totalReps, totalSets, totalDurationInMinutes) = workout.GetSummary();
        return new SummaryDto(totalReps, totalSets, totalDurationInMinutes);
    }
    
    public async Task Update(int workoutId, LinkDateToWorkoutRequestDto request)
    {
        var workout = await _applicationDbContext.Workouts.FirstAsync(w => w.Id == workoutId);
            
        workout.LinkToDate(request.Date);
        await _applicationDbContext.SaveChangesAsync();
    }

    public async Task<WorkoutHistoryResponseDto> GetAllByDate(DateTime date)
    {
        var workouts = await _applicationDbContext.Workouts
            .Include(w => w.Exercises)
            .Where(w => w.ExerciseDate.HasValue && w.ExerciseDate.Value.Date == date.Date)
            .ToListAsync();

        var historyWorkouts = new List<HistoryWorkoutDto>();
        foreach (var workout in workouts)
        {
            var (totalReps, totalSets, totalDurationInMinutes) = workout.GetSummary();
            var summaryDto = new SummaryDto(totalReps, totalSets, totalDurationInMinutes);
            var exercisesDto = _mapper.Map<ICollection<ExerciseDto>>(workout.Exercises);
            historyWorkouts.Add(new HistoryWorkoutDto(workout.Title, workout.Description, summaryDto,
                exercisesDto));
        }

        return new WorkoutHistoryResponseDto(date.Date, historyWorkouts);
    }
}