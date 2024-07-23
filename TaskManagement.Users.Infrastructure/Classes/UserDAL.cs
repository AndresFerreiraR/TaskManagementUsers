using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskManagement.Users.Domain.Entities;
using TaskManagement.Users.Infrastructure.Context;
using TaskManagement.Users.Infrastructure.Interfaces;

namespace TaskManagement.Users.Infrastructure.Classes
{
    public class UserDAL : IUserDAL
    {
        private readonly UserTaskContext _context;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<UserDAL> _logger;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserDAL(UserTaskContext context,
                       UserManager<User> userManager,
                       ILogger<UserDAL> logger,
                       IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }


        public async Task CreateUser(User user)
        {
            try
            {
                await _userManager.CreateAsync(user, user.Password);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> GetUserById(Guid Id)
        {
            var user =  await _context.Users.FirstOrDefaultAsync(x => x.Id == Id.ToString());

            return user;
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            return users;
        }

        public async Task UpdateUser(User user)
        {
            var userFound = await _userManager.FindByNameAsync(user.UserName);
            userFound.FirstName = user.FirstName;
            userFound.LastName = user.LastName;
            userFound.PasswordHash = _passwordHasher.HashPassword(userFound, user.Password);
            var resultadoUpdate = await _userManager.UpdateAsync(userFound);
        }

    }
}