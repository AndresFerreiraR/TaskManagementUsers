using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TaskManagement.Users.Domain.Entities;
using TaskManagement.Users.Infrastructure.Context;

namespace TaskManagement.Users.Infrastructure.TestIdentity
{
    public class TestData
    {
        public static async Task InsertData(UserTaskContext context, 
                                        UserManager<User> userManager)
        {
            if(!userManager.Users.Any())
            {
                var usuario = new User
                {
                    FirstName = "Andres",
                    LastName = "Ferreira",
                    UserName = "aferreira",
                    Email = "ac.ferreira.r@hotmail.com"
                };
                await userManager.CreateAsync(usuario, "AF3rr31r4$");
            }
        }
    }
}