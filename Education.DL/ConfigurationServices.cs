using Education.Core.Models;
using Education.DL.Repository.Abstractions;
using Education.DL.Repository.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Education.DL;

public static class ConfigurationServices
{
    public static void AddDLServices(this IServiceCollection services)
    {
        services.AddScoped<IRepository<Category>, Repository<Category>>();
        services.AddScoped<IRepository<News>, Repository<News>>();
    }
}
