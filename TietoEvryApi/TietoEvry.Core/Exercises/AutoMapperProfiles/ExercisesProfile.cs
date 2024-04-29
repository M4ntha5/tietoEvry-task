using AutoMapper;
using TietoEvry.Core.Exercises.Dtos;
using TietoEvry.Data.Models;

namespace TietoEvry.Core.Exercises.AutoMapperProfiles;

public class ExercisesProfile : Profile
{
    public ExercisesProfile()
    {
        CreateMap<CreateExerciseRequestDto, Exercise>();
        CreateMap<Exercise, ExerciseDto>();
    }
}