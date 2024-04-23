using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TietoEvry.Core.Exercises.Dtos;
using TietoEvry.Core.Exercises.Services.Interfaces;
using TietoEvry.Data.Contexts;
using TietoEvry.Data.Models;

namespace TietoEvry.Core.Exercises.Services;

//TODO test needed
public class ExerciseService : IExerciseService
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;

    public ExerciseService(ApplicationDbContext applicationDbContext, IMapper mapper)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }
    
    public async Task<ICollection<ExerciseDto>> GetAll()
    {
        var exercises = await _applicationDbContext.Exercises.ToListAsync();
        return _mapper.Map<ICollection<ExerciseDto>>(exercises);
    }

    public async Task<ExerciseDto> GetById(int id)
    {
        var exercise = await _applicationDbContext.Exercises.FirstAsync(w => w.Id == id);
        return _mapper.Map<ExerciseDto>(exercise);
    }

    public async Task<ExerciseDto> Create(CreateExerciseRequestDto request)
    {
        var workout = await _applicationDbContext.Workouts.FirstOrDefaultAsync(w => w.Id == request.WorkoutId);
        if (workout == null)
        {
            throw new Exception("Workout with provided Id not found");
        }
            
        var exercise = _mapper.Map<Exercise>(request);
        await _applicationDbContext.AddAsync(exercise);
        await _applicationDbContext.SaveChangesAsync();

        return await GetById(exercise.Id);
    }

    public async Task Delete(int id)
    {
        var exercise = await _applicationDbContext.Exercises.FirstAsync(w => w.Id == id);
                
        _applicationDbContext.Remove(exercise);
        await _applicationDbContext.SaveChangesAsync();
    }
}