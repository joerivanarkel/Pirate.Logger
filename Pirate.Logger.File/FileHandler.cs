using Microsoft.Extensions.Configuration;
using Files = System.IO.File;

namespace Pirate.Logger.File;

public class FileHandler
{
    private string _targetDirectory;
    private string _fileName;

    public FileHandler(IConfiguration configuration)
    {
        _targetDirectory = configuration.GetSection("Pirate.Logger:TargetDirectory").Value ?? $"{Directory.GetCurrentDirectory()}\\logdumps";
        _fileName = $"{DateTime.Now.Day}.{DateTime.Now.Month}.{DateTime.Now.Year}.{DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}";
    }

    public bool WriteLine(string message)
    {
        if (!Directory.Exists(_targetDirectory))
        {
            Directory.CreateDirectory(_targetDirectory);
        }
        if (!Files.Exists($"{_targetDirectory}/{_fileName}.log"))
        {
            Files.Create($"{_targetDirectory}/{_fileName}.log").Close();
        }

        Files.AppendAllText($"{_targetDirectory}/{_fileName}.log", message + Environment.NewLine);

        return true;
    }
}
