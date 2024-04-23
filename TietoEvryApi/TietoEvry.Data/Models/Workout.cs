using TietoEvry.Data.Base;

namespace TietoEvry.Data.Models;

public class Workout : IdBaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? ExerciseDate { get; private set; }
    public ICollection<Exercise>? Exercises { get; set; }


    public void LinkToDate(DateTime dateTime)
    {
        ExerciseDate = dateTime;
    }

    public (int totalReps, int totalSets, int totalDuration) GetSummary()
    {
        if (Exercises == null)
        {
            throw new Exception("Workout exercises not loaded");
        }
            
        int totalReps = 0, totalSets = 0, totalDurationInMinutes = 0;
        foreach (var exercise in Exercises)
        {
            totalSets += exercise.Sets;
            totalReps += exercise.Reps * exercise.Sets;
            totalDurationInMinutes += exercise.DurationInMinutes;
        }

        return (totalReps, totalSets, totalDurationInMinutes);
    }
}