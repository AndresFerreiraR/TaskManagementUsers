using TaskManagement.Users.Application.Dto;

namespace TaskManagement.Users.Application.Interfaces
{
    public interface IUserBL
    {
        public Task CreateUser(UserDto userDto);
        public Task UpdateUser(UserDto userDto);
        public Task<UserDto> GetUserById(Guid Id);
        public Task<List<UserDto>> GetUsers();
    }
}