using Education.BL.Services.Abstractions;
using Education.BL.Services.Concretes;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Education.BL;

public static class ConfigurationServices
{
    public static void AddBLServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();

        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<INewsService, NewsService>();
        services.AddScoped<ICategoryService, CategoryService>();
    }
}
