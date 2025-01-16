using Klinik.Core.Models;
using Klinik.DL.Repository.Abstractions;
using Klinik.DL.Repository.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Klinik.DL;

public static class ConfigurationServices
{
    public static void AddDLServices(this IServiceCollection services)
    {
        services.AddScoped<IRepository<Department>, Repository<Department>>();
        services.AddScoped<IRepository<Doctor>, Repository<Doctor>>();
    }
}
