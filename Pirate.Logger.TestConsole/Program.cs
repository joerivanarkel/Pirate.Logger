using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pirate.Logger.Extensions;
using Pirate.Logger.TestConsole;

var builder = new ServiceCollection();

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

builder.AddSingleton<IConfiguration>(configuration);
builder.AddSingleton<Application>();
builder.AddLogger();

var provider = builder.BuildServiceProvider();
var app = provider.GetRequiredService<Application>();

app.Run(args);