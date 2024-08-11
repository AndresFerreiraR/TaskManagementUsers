using AutoMapper;
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
    public UserBL(IUserDAL repoUser,
                  IMapper mapper,
                  LogDataUserValidator validatorLog)
    {
      _repoUser = repoUser;
      _mapper = mapper;
      _validatorLog = validatorLog;
    }


    public async Task CreateUser(UserDto userDto)
    {
      var user = _mapper.Map<User>(userDto);
      await _repoUser.CreateUser(user, userDto.Password);
    }

    public async Task UpdateUser(UserDto userDto)
    {
      var user = _mapper.Map<User>(userDto);
      await _repoUser.UpdateUser(user, userDto.Password);
    }

    public async Task<Response<UserDto>> GetUserById(Guid Id)
    {
      var response = new Response<UserDto>();
      var user = await _repoUser.GetUserById(Id);
      response.Data = _mapper.Map<UserDto>(user);

      return response;
    }

    public async Task<Response<List<UserDto>>> GetUsers()
    {
      var response = new Response<List<UserDto>>();
      var listUser = await _repoUser.GetUsers();
      var userDto = _mapper.Map<List<UserDto>>(listUser);
      response.Data = userDto;
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
            if (logData.UserEmail is null && logData.UserName is not null)
            {
                user = await _repoUser.GetUserByUserName(logData.UserName);
            }
            else if (logData.UserEmail is not null && logData.UserName is null)
            {
                user = await _repoUser.GetUserByEmail(logData.UserEmail);
            }

            return user;
        }
    }
}