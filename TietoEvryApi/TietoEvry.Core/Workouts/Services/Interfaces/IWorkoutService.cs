using TietoEvry.Core.Workouts.Dtos;

namespace TietoEvry.Core.Workouts.Services.Interfaces;

public interface IWorkoutService
{
    Task<ICollection<WorkoutDto>> GetAll();
    Task<WorkoutDto> GetById(int id);
    Task<WorkoutDto> Create(CreateWorkoutRequestDto request);
    Task Delete(int id);
    Task<SummaryDto> GetSummary(int workoutId);
    Task Update(int workoutId, LinkDateToWorkoutRequestDto request);
    Task<WorkoutHistoryResponseDto> GetAllByDate(DateTime date);
}