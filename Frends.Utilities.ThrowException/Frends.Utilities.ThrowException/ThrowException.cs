using Frends.Utilities.ThrowException.Definitions;
using System;

namespace Frends.Utilities.ThrowException;

/// <summary>
/// Utilities ThrowException task.
/// </summary>
public class Utilities
{
    /// <summary>
    /// Throws an exception with given message.
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends.Utilities.ThrowException)
    /// </summary>
    /// <param name="input">Input parameter.</param>
    /// <returns>Object { bool ExceptionThrown }. Only if InputString parameter was empty.</returns>
    public static Result ThrowException(Input input)
    {
        if (string.IsNullOrWhiteSpace(input.InputString))
            return new Result(false);
        else
            throw new Exception(input.InputString);
    }
}