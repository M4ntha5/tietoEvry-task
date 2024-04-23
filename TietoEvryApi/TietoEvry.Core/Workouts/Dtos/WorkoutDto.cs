using TietoEvry.Core.Exercises.Dtos;

namespace TietoEvry.Core.Workouts.Dtos;

public record WorkoutDto(
    int Id,
    string Title,
    string Description,
    ICollection<ExerciseDto> Exercises);
 
