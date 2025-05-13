namespace PedaloWebApp.Tools.DataInitializer.Commands.Start
{
    using CommandLine;

    /// <summary>
    /// The options for the start command. This verb is marked as the default which means the start command will be executed when no verb was provided on launch.
    /// </summary>
    [Verb("start", isDefault: true, HelpText = "The main command for data initializer.")]
    public class StartOptions
    {
    }
}
