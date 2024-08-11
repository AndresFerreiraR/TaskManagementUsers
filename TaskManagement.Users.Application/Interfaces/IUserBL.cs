using TaskManagement.Users.Application.Dto;
using TaskManagement.Users.Commons.Response;

namespace TaskManagement.Users.Application.Interfaces
{
    public interface IUserBL
    {
        public Task CreateUser(UserDto userDto);
        public Task UpdateUser(UserDto userDto);
        public Task<Response<UserDto>> GetUserById(Guid Id);
        public Task<Response<List<UserDto>>> GetUsers();
        public Task<Response<DataUserDto>> LoginUser(LogDataUserDto logData);
    }
}