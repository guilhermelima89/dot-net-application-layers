using Api.Extensions;
using Api.Services;
using Core.Interfaces;
using Core.Services;
using Data.Context;
using Data.Repositories;

namespace Api.Configurations;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddScoped<ApplicationDbContext>();

        services.AddScoped<AuthenticationService>();

        services.AddScoped<INotificadorService, NotificadorService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserService, AspNetUser>();

        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IProdutoService, ProdutoService>();

        return services;
    }
}

