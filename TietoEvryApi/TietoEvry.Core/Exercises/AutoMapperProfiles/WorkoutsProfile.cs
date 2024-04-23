using AutoMapper;
using TietoEvry.Core.Exercises.Dtos;
using TietoEvry.Data.Models;

namespace TietoEvry.Core.Exercises.AutoMapperProfiles;

public class WorkoutsProfile : Profile
{
    public WorkoutsProfile()
    {
        CreateMap<CreateExerciseRequestDto, Exercise>();
        CreateMap<Exercise, ExerciseDto>();
    }
}