using ClinicMS.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicMS.Infrastructure.Data.SeedData
{
  public static class SeedingAdminandRoles
  {

    public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
    {
      var userManager = serviceProvider.GetRequiredService<UserManager<utblApplicationUser>>();

      var roleManager = serviceProvider.GetRequiredService<RoleManager<utblApplicationUserRoles>>();


      string[] roles = ["Admin", "Doctor", "Reception", "Patient"];

      foreach (var role in roles)
      {
        if (!await roleManager.RoleExistsAsync(role))
        {
          // await roleManager.CreateAsync(new utblApplicationUserRoles(role));
          await roleManager.CreateAsync(new utblApplicationUserRoles { Name = role });
        }
      }

      string AdminEmail = "admin@clinic.com";
      string AdminPassword = "Pass@123";

      var ifUser = await userManager.FindByEmailAsync(AdminEmail);

      if (ifUser == null)
      {
        var newAdmin = new utblApplicationUser
        {
          Email = AdminEmail,
          UserName = AdminEmail,
          EmailConfirmed = true,
          PhoneNumber = "9874563210"
        };


        var task = await userManager.CreateAsync(newAdmin, AdminPassword);
        if (task.Succeeded)
        {
          await userManager.AddToRoleAsync(newAdmin, "Admin");
        }
        else
        {
          throw new Exception("Failed to create the new admin user:" + string.Join(", ", task.Errors));
        }
      }
    }
  }
}
