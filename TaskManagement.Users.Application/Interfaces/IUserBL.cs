using TaskManagement.Users.Application.Dto;
using TaskManagement.Users.Commons.Response;

namespace TaskManagement.Users.Application.Interfaces
{
    public interface IUserBL
    {
        public Task<Response<bool>> CreateUser(UserDto userDto);
        public Task<Response<bool>> UpdateUser(UserDto userDto);
        public Task<Response<UserDto>> GetUserById(Guid Id);
        public Task<Response<List<UserDto>>> GetUsers();
        public Task<Response<DataUserDto>> LoginUser(LogDataUserDto logData);
    }
}