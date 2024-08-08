using Microsoft.Extensions.DependencyInjection;
using Pirate.Logger.Data;
using Pirate.Logger.Interfaces;
using Pirate.Logger.Models;

namespace Pirate.Logger.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLogger(this IServiceCollection services)
    {
        services.AddSingleton<Repository>();
        services.AddSingleton<ILogger, Logger>();
        services.AddSingleton<LoggerConfiguration>();

        services.AddDbContext<AppDbContext>();

        return services;
    }
}