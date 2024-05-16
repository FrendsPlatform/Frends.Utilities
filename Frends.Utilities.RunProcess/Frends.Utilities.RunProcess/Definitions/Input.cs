namespace Frends.Utilities.RunProcess.Definitions;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Input class usually contains parameters that are required.
/// </summary>
public class Input
{
    /// <summary>
    /// An application or document with which to start a process. Use cmd.exe to execute command on "command line".
    /// </summary>
    /// <example>cmd.exe</example>
    [DefaultValue("cmd.exe")]
    [DisplayFormat(DataFormatString = "Text")]
    public string FileName { get; set; }

    /// <summary>
    /// Command-line arguments to use when starting the application.
    /// For Windows use /C start and on Unix use -c.
    /// </summary>
    /// <example>[ "/C", "echo Hello" ]</example>
    public string[] Arguments { get; set; }

    /// <summary>
    /// Working directory for the process.
    /// </summary>
    /// <example>/working/directory OR C:\working\directory</example>
    [DefaultValue("")]
    [DisplayFormat(DataFormatString = "Text")]
    public string WorkingDirectory { get; set; }
}