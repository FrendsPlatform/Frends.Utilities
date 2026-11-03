namespace Frends.Utilities.ThrowException.Definitions;

/// <summary>
/// Input parameter.
/// </summary>
public class Input
{
    /// <summary>
    /// Exception message. If empty, no exception is thrown.
    /// </summary>
    /// <example>Order validation failed: missing customer ID</example>
    public string InputString { get; set; }
}
