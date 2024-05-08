using AutoMapper;
using TaskManagement.Users.Application.Dto;
using TaskManagement.Users.Application.Interfaces;
using TaskManagement.Users.Domain.Entities;
using TaskManagement.Users.Infrastructure.Interfaces;

namespace TaskManagement.Users.Application.Classes
{
  public class UserBL : IUserBL
  {

    private readonly IUserDAL _repoUser;
    private readonly IMapper _mapper;
    public UserBL(IUserDAL repoUser,
                  IMapper mapper)
    {
      _repoUser = repoUser;
      _mapper = mapper;
    }


    public async Task CreateUser(UserDto userDto)
    {
      var user = _mapper.Map<User>(userDto);
      await _repoUser.CreateUser(user);
    }

    public async Task UpdateUser(UserDto userDto)
    {
      var user = _mapper.Map<User>(userDto);
      await _repoUser.UpdateUser(user);
    }

    public async Task<UserDto> GetUserById(Guid Id)
    {
      var user = await _repoUser.GetUserById(Id);
      var userDto = _mapper.Map<UserDto>(user);
      return userDto;
    }

    public async Task<List<UserDto>> GetUsers()
    {
       var listUser = await _repoUser.GetUsers();
       var userDto = _mapper.Map<List<UserDto>>(listUser);
       return userDto;
    }
  }
}