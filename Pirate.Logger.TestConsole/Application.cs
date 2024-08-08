
using Pirate.Logger.Interfaces;

namespace Pirate.Logger.TestConsole;

public class Application
{

    public ILogger Logger { get; set; }

    public Application(ILogger logger)
    {
        Logger = logger;
    }
    public void Run(string[] args)
    {
        Logger.Info("Hello World", new()
            {
                { "UserId", 12345 },
                { "Action", "Login" },
                { "Success", true }
            }
        );

        Logger.Error("Something went wrong",
            new Exception("This is a test exception"),
            new()
            {
                { "UserId", 12345 },
                { "Action", "Login" },
                { "Success", false }
            }
        );
    }
}
