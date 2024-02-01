namespace Frends.Utilities.RunProcess.Definitions;

/// <summary>
/// Result class usually contains properties of the return object.
/// </summary>
public class Result
{
    internal Result(int exitCode, string output, string stdErr)
    {
        ExitCode = exitCode;
        Output = output;
        StdErr = stdErr;
    }

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
    /// <example> </example>
    public string StdErr { get; set; }
}
