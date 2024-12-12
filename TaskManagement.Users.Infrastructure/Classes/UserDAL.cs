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
        private readonly SignInManager<User> _signInManager;

        public UserDAL(UserTaskContext context,
                       UserManager<User> userManager,
                       ILogger<UserDAL> logger,
                       IPasswordHasher<User> passwordHasher,
                       SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
            _passwordHasher = passwordHasher;
            _signInManager = signInManager;
        }

        public async Task<SignInResult> CheckUserPassword(User user, string password)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            return result;
        }

        public async Task<bool> CreateUser(User user, string password)
        {
            try
            {
                var response  = await _userManager.CreateAsync(user, password);
                return response.Succeeded;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user!;
        }

        public async Task<User> GetUserById(Guid Id)
        {
            var user =  await _context.Users.FirstOrDefaultAsync(x => x.Id == Id.ToString());

            return user;
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return user!;
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            return users;
        }

        public async Task<bool> UpdateUser(User user, string password)
        {
            var userFound = await _userManager.FindByNameAsync(user.UserName);
            userFound.FirstName = user.FirstName;
            userFound.LastName = user.LastName;
            userFound.PasswordHash = _passwordHasher.HashPassword(userFound, password);
            var resultadoUpdate = await _userManager.UpdateAsync(userFound);
            return resultadoUpdate.Succeeded;
        }

        

    }
}