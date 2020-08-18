using IssueTracker.MVC.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.MVC.Data
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.ProjectManager.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Developer.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Submitter.ToString()));
        }

        public static async Task SeedDefaultUsers(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var DemoAdmin = new ApplicationUser
            {
                UserName = "DemoAdmin",
                Email = "DemoAdmin@gmail.com",
                FirstName = "Demo",
                LastName = "Admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var DemoPM = new ApplicationUser
            {
                UserName = "DemoPM",
                Email = "DemoPM@gmail.com",
                FirstName = "Demo",
                LastName = "ProjectManager",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var DemoDev = new ApplicationUser
            {
                UserName = "DemoDev",
                Email = "DemoDev@gmail.com",
                FirstName = "Demo",
                LastName = "Developer",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var DemoSubmitter = new ApplicationUser
            {
                UserName = "DemoSubmitter",
                Email = "DemoSubmitter@gmail.com",
                FirstName = "Demo",
                LastName = "Submitter",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (userManager.Users.All(u => u.Id != DemoAdmin.Id))
            {
                var user = await userManager.FindByEmailAsync(DemoAdmin.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(DemoAdmin, "123Pa$$word.");
                    await userManager.AddToRoleAsync(DemoAdmin, Enums.Roles.Admin.ToString());
                }
            }

            if (userManager.Users.All(u => u.Id != DemoPM.Id))
            {
                var user = await userManager.FindByEmailAsync(DemoPM.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(DemoPM, "123Pa$$word.");
                    await userManager.AddToRoleAsync(DemoPM, Enums.Roles.ProjectManager.ToString());
                }
            }

            if (userManager.Users.All(u => u.Id != DemoDev.Id))
            {
                var user = await userManager.FindByEmailAsync(DemoDev.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(DemoDev, "123Pa$$word.");
                    await userManager.AddToRoleAsync(DemoDev, Enums.Roles.Developer.ToString());
                }
            }

            if (userManager.Users.All(u => u.Id != DemoSubmitter.Id))
            {
                var user = await userManager.FindByEmailAsync(DemoSubmitter.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(DemoSubmitter, "123Pa$$word.");
                    await userManager.AddToRoleAsync(DemoSubmitter, Enums.Roles.Submitter.ToString());
                }
            }
        }
    }
}
