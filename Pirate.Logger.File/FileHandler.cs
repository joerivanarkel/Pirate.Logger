using Microsoft.Extensions.Configuration;
using Files = System.IO.File;

namespace Pirate.Logger.File;

public class FileHandler
{
    private string _targetDirectory;
    private string _fileName;

    public FileHandler(IConfiguration configuration)
    {
        _targetDirectory = configuration.GetSection("Pirate.Logger:TargetDirectory").Value ?? $"{Directory.GetCurrentDirectory()}\\logs";
    }

    public bool WriteLine(string message)
    {
        if (!Directory.Exists(_targetDirectory))
        {
            Directory.CreateDirectory(_targetDirectory);
        }
        if (!Files.Exists($"{_targetDirectory}/log.txt"))
        {
            Files.Create($"{_targetDirectory}/log.txt").Close();
        }



        return true;
    }
}
