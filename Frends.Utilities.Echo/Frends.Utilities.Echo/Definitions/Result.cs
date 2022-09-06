namespace Frends.Utilities.Echo.Definitions;

/// <summary>
/// Result.
/// </summary>
public class Result
{
    /// <summary>
    /// Returns given Input String.
    /// </summary>
    /// <example>Example Text</example>
    public string ResultString { get; private set; }

    internal Result(string resultString)
    {
        ResultString = resultString;
    }
}