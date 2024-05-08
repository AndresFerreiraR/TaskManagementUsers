namespace TaskManagement.Users.Infrastructure.Context
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using TaskManagement.Users.Domain.Entities;

    public class UserTaskContext : IdentityDbContext<User>
    {
        public UserTaskContext(DbContextOptions<UserTaskContext> options)
          : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
    }
}