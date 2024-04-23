using TietoEvry.Core.Exercises.Dtos;

namespace TietoEvry.Core.Exercises.Services.Interfaces;

public interface IExerciseService
{
    Task<ICollection<ExerciseDto>> GetAll();
    Task<ExerciseDto> GetById(int id);
    Task<ExerciseDto> Create(CreateExerciseRequestDto request);
    Task Delete(int id);
}