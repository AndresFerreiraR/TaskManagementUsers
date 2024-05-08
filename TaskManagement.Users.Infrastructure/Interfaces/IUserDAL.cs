using TaskManagement.Users.Domain.Entities;

namespace TaskManagement.Users.Infrastructure.Interfaces
{
    public interface IUserDAL
    {
        public Task CreateUser(User user);
        public Task UpdateUser(User user);
        public Task<User> GetUserById(Guid Id);
        public Task<List<User>> GetUsers();
    }
}