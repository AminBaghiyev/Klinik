using Klinik.Core.Models;
using Klinik.DL.Contexts;
using Klinik.DL.Repository.Abstractions;
using Klinik.DL.Repository.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Klinik.DL;

public static class ConfigurationServices
{
    public static void AddDLServices(this IServiceCollection services)
    {
        services.AddIdentity<IdentityUser, IdentityRole>(
            options =>
            {
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(10);
                options.Lockout.MaxFailedAccessAttempts = 10;
            }
        )
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<AppDbContext>();

        services.AddScoped<IRepository<Department>, Repository<Department>>();
        services.AddScoped<IRepository<Doctor>, Repository<Doctor>>();
    }
}
