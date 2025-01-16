using FluentValidation;
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

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();

        services.AddScoped<IAccountService, AccountService>();
    }
}
