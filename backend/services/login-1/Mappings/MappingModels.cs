namespace login.Mapping;
using AutoMapper;
using login.Models.DTO.User;
using login.Models.Entities;

public class MappingModels : Profile
{
    public MappingModels()
    {
        CreateMap<Users, UserList>().ReverseMap();
        CreateMap<Users, UserDetails>().ReverseMap(); //655daf8baf2583e954e51a14
        CreateMap<Users, UpdateUser>().ReverseMap();
        CreateMap<Users, CreateUser>().ReverseMap();
    }
}
