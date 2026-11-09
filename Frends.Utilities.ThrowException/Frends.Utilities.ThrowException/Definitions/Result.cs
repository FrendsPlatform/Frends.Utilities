namespace Frends.Utilities.ThrowException.Definitions;

/// <summary>
/// Result of the task.
/// </summary>
public class Result
{
    /// <summary>
    /// Indicates if the task completed successfully. False if an exception was thrown and handled.
    /// </summary>
    /// <example>true</example>
    public bool Success { get; set; } = true;
}
