using EmployeeDbApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;



namespace EmployeeDbApp.Data
{
    public class SeedDefaultRolesAndUsers
    {

        public enum Roles
        {
            SuperAdmin,
            Admin,
            Moderator,
            Member,
            Visitor
        }




        public static async Task SeedRoles(UserManager<EmployeeDbAppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Moderator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Member.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Visitor.ToString()));
        }

        public static async Task SeedUsers(UserManager<EmployeeDbAppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser1 = new EmployeeDbAppUser
            {
                UserName = "haitham.abass49@gmail.com",
                Email = "haitham.abass49@gmail.com",
                FirstName = "Haitham",
                LastName = "Abass",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser1.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser1.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser1, "123456");
                    await userManager.AddToRoleAsync(defaultUser1, Roles.SuperAdmin.ToString());
                }
            }


            var defaultUser2 = new EmployeeDbAppUser
            {
                UserName = "Maged.sobhy50@gmail.com",
                Email = "Maged.sobhy50@gmail.com",
                FirstName = "Maged",
                LastName = "Sobhy",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser2.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser2.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser2, "123456");
                    await userManager.AddToRoleAsync(defaultUser2, Roles.SuperAdmin.ToString());
                }
            }


        }


    }
}
