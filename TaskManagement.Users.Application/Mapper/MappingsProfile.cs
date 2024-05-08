using AutoMapper;
using TaskManagement.Users.Application.Dto;
using TaskManagement.Users.Domain.Entities;

namespace TaskManagement.Users.Application.Mapper
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}