using AutoMapper;
using JonasDemoApi.Models;
using JonasDemoApi.Dto;

namespace JonasDemoApi.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<MergedUnique, MergedUniqueDto>();
            Mapper.CreateMap<MergedUniqueDto, MergedUnique>();
        }
    }
}