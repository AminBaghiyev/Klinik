using FluentValidation.AspNetCore;
using Klinik.BL.Services.Abstractions;
using Klinik.BL.Services.Concretes;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Klinik.BL;

public static class ConfigurationServices
{
    public static void AddBLServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IDoctorService, DoctorService>();
    }
}
