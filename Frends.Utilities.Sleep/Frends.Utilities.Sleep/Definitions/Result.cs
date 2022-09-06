namespace Frends.Utilities.Sleep.Definitions;

/// <summary>
/// Result.
/// </summary>
public class Result
{
    /// <summary>
    /// Sleep result.
    /// </summary>
    /// <example>true</example>
    public bool SleepResult { get; private set; }

    internal Result(bool sleepResult)
    {
        SleepResult = sleepResult;
    }
}