namespace Frends.Utilities.RunProcess.Definitions;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Class for Argument parameter.
/// </summary>
public class Argument
{
    /// <summary>
    /// Argument name. Use /C to provide command to cmd.
    /// </summary>
    /// <example>test1</example>
    public string Name { get; set; }

    /// <summary>
    /// Argument value. When using cmd and /C as argument name, write here actual command being executed.
    /// </summary>
    /// <example>testvalue</example>
    public string Value { get; set; }
}

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
    /// </summary>
    /// <example>[ { Name = "test1", Value = "testvalue" } ]</example>
    public Argument[] Arguments { get; set; }
}