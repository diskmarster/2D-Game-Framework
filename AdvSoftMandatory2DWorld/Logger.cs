using System.Diagnostics;
/// <summary>
/// Centralized logger for game messages.
/// All output is routed through this static utility.
/// </summary>
public static class Logger
{
    /// <summary>
    /// Writes a log message with a [Logger] prefix.
    /// </summary>
    /// <param name="message">The message to log.</param>
    public static void Log(string message)
    {
        Trace.WriteLine($"[Logger] {message}");
    }
}
