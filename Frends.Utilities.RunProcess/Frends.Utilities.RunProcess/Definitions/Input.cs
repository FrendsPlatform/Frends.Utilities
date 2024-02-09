namespace Frends.Utilities.RunProcess.Definitions;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Input class usually contains parameters that are required.
/// </summary>
public class Input
{
    /// <summary>
    /// Select which platform the Task is running.
    /// </summary>
    /// <example>Platform.Unix</example>
    [DefaultValue(Platform.Windows)]
    public Platform Platform { get; set; }

    /// <summary>
    /// An application or document with which to start a process. Use cmd.exe to execute command on "command line".
    /// </summary>
    /// <example>cmd.exe</example>
    [DefaultValue("cmd.exe")]
    [DisplayFormat(DataFormatString = "Text")]
    public string FileName { get; set; }

    /// <summary>
    /// Command-line arguments to use when starting the application.
    /// </summary>
    /// <example>[ "/C set" ]</example>
    public string[] Arguments { get; set; }
}