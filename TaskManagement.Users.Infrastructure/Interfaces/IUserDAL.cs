using Microsoft.AspNetCore.Identity;
using TaskManagement.Users.Domain.Entities;

namespace TaskManagement.Users.Infrastructure.Interfaces
{
    public interface IUserDAL
    {
        public Task CreateUser(User user, string password);
        public Task UpdateUser(User user, string password);
        public Task<User> GetUserById(Guid Id);
        public Task<List<User>> GetUsers();
        public Task<User> GetUserByEmail(string email);
        public Task<User> GetUserByUserName(string userName);
        public Task<SignInResult> CheckUserPassword(User user, string password); 
    }
}