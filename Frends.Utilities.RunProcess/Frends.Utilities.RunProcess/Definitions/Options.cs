namespace Frends.Utilities.RunProcess.Definitions;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

    /// <summary>
    /// Whether to throw an error on failure.
    /// </summary>
    /// <example>true</example>
    [DefaultValue(true)]
    public bool ThrowErrorOnFailure { get; set; } = true;

    /// <summary>
    /// Overrides the error message on failure.
    /// </summary>
    /// <example>Running the process failed</example>
    [DisplayFormat(DataFormatString = "Text")]
    [DefaultValue("")]
    public string ErrorMessageOnFailure { get; set; } = string.Empty;
}
