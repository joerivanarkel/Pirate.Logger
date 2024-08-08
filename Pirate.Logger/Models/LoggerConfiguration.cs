using Microsoft.Extensions.Configuration;

namespace Pirate.Logger.Models;

/// <example>
///    "Pirate.Logger" :  {
///        "TargetDirectory": "logs",
///
///        "Target": {
///            "UseConsole": true,
///            "UseFile": true,
///            "UseDatabase": true
///        }
///    }
/// </example>
public class LoggerConfiguration
{
    public string TargetDirectory { get; set; } = $"{Directory.GetCurrentDirectory()}\\logs";
    public TargetConfiguration Target { get; set; } = new();


    public class TargetConfiguration
    {
        public bool UseConsole { get; set; } = false;
        public bool UseFile { get; set; } = true;
        public bool UseDatabase { get; set; } = true;
    }


    public LoggerConfiguration(IConfiguration configuration)
    {
        configuration
            .GetSection("Pirate.Logger")
            .Bind(this);
    }
}
