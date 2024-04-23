using AutoMapper;
using TietoEvry.Core.Workouts.Dtos;
using TietoEvry.Data.Models;

namespace TietoEvry.Core.Workouts.AutoMapperProfiles;

public class WorkoutsProfile : Profile
{
    public WorkoutsProfile()
    {
        CreateMap<CreateWorkoutRequestDto, Workout>();
        CreateMap<Workout, WorkoutDto>();
    }
}