using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Automapper
{
    public class AutoMapperProfile:Profile
    {

      public AutoMapperProfile() 
      { 
        CreateMap<Region,RegionDto>().ReverseMap();
            CreateMap<regionDto4postRequest, Region>().ReverseMap();
            CreateMap<regionDto4PutRequest, Region>().ReverseMap();
            CreateMap<WalksRequestDto, Walk>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<WalkUpdateDto, Walk>().ReverseMap();
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
      }
    }
}
