using Microsoft.EntityFrameworkCore;
using TietoEvry.Data.Models;

namespace TietoEvry.Data.Contexts;

public class ApplicationDbContext : DbContext
{
    public virtual DbSet<Exercise> Exercises => Set<Exercise>();
    public virtual DbSet<Workout> Workouts => Set<Workout>();
}