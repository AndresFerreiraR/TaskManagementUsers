using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using TaskManagement.Users.Application.Common.Errors;
using TaskManagement.Users.Application.Common.Validations;
using TaskManagement.Users.Application.Dto;
using TaskManagement.Users.Application.Interfaces;
using TaskManagement.Users.Commons.Response;
using TaskManagement.Users.Domain.Entities;
using TaskManagement.Users.Infrastructure.Interfaces;

namespace TaskManagement.Users.Application.Classes
{
  public class UserBL : IUserBL
  {

    private readonly IUserDAL _repoUser;
    private readonly IMapper _mapper;
    private readonly LogDataUserValidator _validatorLog;
    private readonly UserDtoValidator _validatorUser;
    public UserBL(IUserDAL repoUser,
                  IMapper mapper,
                  LogDataUserValidator validatorLog,
                  UserDtoValidator validatorUser)
    {
      _repoUser = repoUser;
      _mapper = mapper;
      _validatorLog = validatorLog;
      _validatorUser = validatorUser;
    }


    public async Task<Response<bool>> CreateUser(UserDto userDto)
    {
      var response = new Response<bool>();
      var errors = _validatorUser.ValidateAndCollectErrors(userDto);
      if (errors.Any())
      {
        throw new ValidationExceptionCustom(errors);
      }
      var user = _mapper.Map<User>(userDto);
      response.Data = await _repoUser.CreateUser(user, userDto.Password);
      response.IsSuccess = true;
      return response;
    }

    public async Task<Response<bool>> UpdateUser(UserDto userDto)
    {
      var response = new Response<bool>();
      var user = _mapper.Map<User>(userDto);
      response.Data = await _repoUser.UpdateUser(user, userDto.Password);
      response.IsSuccess= true;
      return response;
    }

    public async Task<Response<UserDto>> GetUserById(Guid Id)
    {
      var response = new Response<UserDto>();
      var user = await _repoUser.GetUserById(Id);
      response.Data = _mapper.Map<UserDto>(user);
      response.IsSuccess = true;
      return response;
    }

    public async Task<Response<List<UserDto>>> GetUsers()
    {
      var response = new Response<List<UserDto>>();
      var listUser = await _repoUser.GetUsers();
      var userDto = _mapper.Map<List<UserDto>>(listUser);
      response.Data = userDto;
      response.IsSuccess = true;
      return response;
    }

    public async Task<Response<DataUserDto>> LoginUser(LogDataUserDto logData)
    {
      var response = new Response<DataUserDto>();
      User user = null;

      var errors = _validatorLog.ValidateAndCollectErrors(logData);
      if (errors.Any())
      {
        throw new ValidationExceptionCustom(errors);
      }

      user = await FindUserByEmailOrUserName(logData, user);

      var checkPassword = await _repoUser.CheckUserPassword(user, logData.Password);

      if (!checkPassword.Succeeded)
      {
        throw new Exception("User or password");
      }

      response.Data = new DataUserDto()
      {
        Id = user.Id,
        UserName = user.UserName,
        FullName = $"{user.FirstName} {user.LastName}",
        Token = "asdasdads",
        Email = user.Email,
      };

      response.IsSuccess = true;
      return response;
    }

    private async Task<User> FindUserByEmailOrUserName(LogDataUserDto logData, User user)
    {
      if (logData.UserEmail.IsNullOrEmpty() && logData.UserName is not null)
      {
        user = await _repoUser.GetUserByUserName(logData.UserName);
      }
      else if (logData.UserEmail.IsNullOrEmpty() && logData.UserName is null)
      {
        user = await _repoUser.GetUserByEmail(logData.UserEmail);
      }

      return user;
    }
  }
}