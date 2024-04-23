using TietoEvry.Data.Base;

namespace TietoEvry.Data.Models;

public class Exercise : IdBaseEntity
{
    public string Name { get; set; }
    public int Sets { get; set; }
    public int Reps { get; set; }
    public int DurationInMinutes { get; set; }
    public int WorkoutId { get; set; }
    
    public Workout Workout { get; set; }
}