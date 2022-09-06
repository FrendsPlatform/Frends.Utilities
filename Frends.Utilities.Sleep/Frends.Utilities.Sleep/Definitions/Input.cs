namespace Frends.Utilities.Sleep.Definitions;

/// <summary>
/// Input parameters.
/// </summary>
public class Input
{
    /// <summary>
    /// Milliseconds being waited.
    /// </summary>
    /// <example>0</example>
    public int Milliseconds { get; set; }

    /// <summary>
    /// Seconds being waited.
    /// </summary>
    /// <example>10</example>
    public int Seconds { get; set; }

    /// <summary>
    /// Minutes being waited
    /// </summary>
    /// <example>0</example>
    public int Minutes { get; set; }

    /// <summary>
    /// Hours being waited
    /// </summary>
    /// <example>0</example>
    public int Hours { get; set; }
}