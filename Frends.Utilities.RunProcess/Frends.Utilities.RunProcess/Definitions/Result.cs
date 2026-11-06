namespace Frends.Utilities.RunProcess.Definitions;

/// <summary>
/// Result class usually contains properties of the return object.
/// </summary>
public class Result
{
    /// <summary>
    /// Indicates if the task completed successfully.
    /// </summary>
    /// <example>true</example>
    public bool Success { get; set; }

    /// <summary>
    /// The status that the process returned when it exited.
    /// </summary>
    /// <example>0</example>
    public int ExitCode { get; set; }

    /// <summary>
    /// The process normal output (STDOUT)
    /// </summary>
    /// <example>testvalue</example>
    public string Output { get; set; }

    /// <summary>
    /// The process error output (STDERR)
    /// </summary>
    /// <example>The system cannot find the file specified.</example>
    public string StdErr { get; set; }

    /// <summary>
    /// Error that occurred during task execution.
    /// </summary>
    /// <example>object { string Message, Exception AdditionalInfo }</example>
    public Error Error { get; set; }
}
