using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SecurityDemo.Data
{
    public class DatabaseInitializer
    {
        public void Initialize(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            AddRoleIfNotExists(context, "Admin");
            AddRoleIfNotExists(context, "Manager");
            AddRoleIfNotExists(context, "RegularUser");
            AddIfNotExists(userManager, "abc@xyz.com", "Admin");
            AddIfNotExists(userManager, "abc2@xyz.com", "Manager");
            AddIfNotExists(userManager, "abc3@xyz.com", "RegularUser");

            AddHockey(context);
        }

        private void AddHockey(ApplicationDbContext context)
        {
            if (!context.Teams.Any(r => r.Name == "Djurgården"))
                context.Teams.Add(new Team { Name = "Djurgården" });
            if (!context.Teams.Any(r => r.Name == "AIK"))
                context.Teams.Add(new Team { Name = "AIK" });
            if (!context.Players.Any(r => r.Name == "Mats Sundin"))
                context.Players.Add(new Player { Name = "Mats Sundin", Age = 49, JerseyNumber = 13 });
            if (!context.Players.Any(r => r.Name == "Peter Forsberg"))
                context.Players.Add(new Player { Name = "Peter Forsberg", Age = 47, JerseyNumber = 21 });
            if (!context.Players.Any(r => r.Name == "Niklas Lidström"))
                context.Players.Add(new Player { Name = "Niklas Lidström", Age = 50, JerseyNumber = 5 });
            //Users - user -> role
            context.SaveChanges();
        }

        private void AddRoleIfNotExists(ApplicationDbContext context, string role)
        {
            if (context.Roles.Any(r => r.Name == role)) return;
            context.Roles.Add(new IdentityRole { Name = role, NormalizedName = role});
            context.SaveChanges();
        }

        private void AddIfNotExists(UserManager<IdentityUser> userManager, string user, string role)
        {
            if (userManager.FindByEmailAsync(user).Result == null)
            {
                var u = new IdentityUser
                {
                    UserName = user,
                    Email = user,
                    EmailConfirmed = true
                };

                var result = userManager.CreateAsync(u, "Hejsan123#").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(u, role).Wait();
                }
            }
        }
    }
}
