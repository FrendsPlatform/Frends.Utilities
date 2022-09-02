namespace Frends.Utilities.AlwaysTrue.Definitions;

/// <summary>
/// Result.
/// </summary>
public class Result
{
    /// <summary>
    /// Result will always be true.
    /// </summary>
    /// <example>true</example>
    public bool TaskResult { get; private set; }

    internal Result(bool taskResult)
    {
        TaskResult = taskResult;
    }
}
