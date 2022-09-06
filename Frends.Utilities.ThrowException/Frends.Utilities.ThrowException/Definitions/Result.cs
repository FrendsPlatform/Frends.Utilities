namespace Frends.Utilities.ThrowException.Definitions;

/// <summary>
/// Result.
/// </summary>
public class Result
{
    /// <summary>
    /// Returns false if InputString was empty. 
    /// </summary>
    /// <example>false</example>
    public bool ExceptionThrown { get; private set; }

    internal Result(bool exceptionThrown)
    {
        ExceptionThrown = exceptionThrown;
    }
}