using TietoEvry.Core.Exercises.Dtos;

namespace TietoEvry.Core.Workouts.Dtos;

public record WorkoutHistoryResponseDto(
    DateTime Date,
    ICollection<HistoryWorkoutDto> Workouts
);

public record HistoryWorkoutDto(
    string Title,
    string Description,
    SummaryDto Summary,
    ICollection<ExerciseDto> Exercises);