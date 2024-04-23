namespace TietoEvry.Core.Exercises.Dtos;

public record CreateExerciseRequestDto(int WorkoutId, string Name, int Sets, int Reps, int DurationInMinutes);