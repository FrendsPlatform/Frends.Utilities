namespace Frends.Utilities.Echo.Definitions;

/// <summary>
/// Result of the task.
/// </summary>
public class Result
{
    /// <summary>
    /// Indicates if the task completed successfully.
    /// </summary>
    /// <example>true</example>
    public bool Success { get; set; }

    /// <summary>
    /// Returns given Input String.
    /// </summary>
    /// <example>Example Text</example>
    public string ResultString { get; set; }
}
