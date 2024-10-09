namespace Users.Mapping;
using AutoMapper;
using Users.Models.DTO.User;
using Users.Models.Entities;

public class MappingModels : Profile
{
    public MappingModels()
    {
        CreateMap<User, UserList>().ReverseMap();
        CreateMap<User, UserDetails>().ReverseMap(); //655daf8baf2583e954e51a14
        CreateMap<User, UpdateUser>().ReverseMap();
        CreateMap<User, CreateUser>().ReverseMap();
    }
}
