using Microsoft.Extensions.DependencyInjection;
using Pirate.Logger.Data;
using Pirate.Logger.File;
using Pirate.Logger.Interfaces;
using Pirate.Logger.Models;

namespace Pirate.Logger.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLogger(this IServiceCollection services)
    {
        services.AddSingleton<ILogger, Logger>();
        services.AddSingleton<LoggerConfiguration>();

        services.AddSingleton<Repository>();
        services.AddDbContext<AppDbContext>();

        services.AddSingleton<MessageFormatter>();
        services.AddSingleton<FileHandler>();

        return services;
    }
}