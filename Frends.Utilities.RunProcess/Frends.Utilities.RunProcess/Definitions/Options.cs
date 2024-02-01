namespace Frends.Utilities.RunProcess.Definitions;

using System.ComponentModel;

/// <summary>
/// Options class usually contains parameters that are required.
/// </summary>
public class Options
{
    /// <summary>
    /// Timeout in full seconds
    /// </summary>
    /// <example>30</example>
    [DefaultValue(30)]
    public int TimeoutSeconds { get; set; }

    /// <summary>
    /// Kill the process after timeout
    /// </summary>
    /// <example>true</example>
    [DefaultValue(false)]
    public bool KillProcessAfterTimeout { get; set; }

    /// <summary>
    /// true if input should be read from StandardInput; otherwise, false
    /// </summary>
    /// <example>true</example>
    [DefaultValue(true)]
    public bool RedirectStandardInput { get; set; }

    /// <summary>
    /// true if the task should throw exception when return code is not 0.
    /// </summary>
    /// <example>true</example>
    [DefaultValue(false)]
    public bool ThrowExceptionOnErrorResponse { get; set; }
}