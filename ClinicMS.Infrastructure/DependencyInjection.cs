using ClinicMS.Domain.Entities;
using ClinicMS.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicMS.Infrastructure
{
  public static class DependencyInjection
  {

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
      services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));


      services.AddIdentity<utblApplicationUser, utblApplicationUserRoles>(options =>
      {
        options.Password.RequireDigit = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredLength = 8;

      })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        return services;
    }
  }
}
