using System.ComponentModel;
using Frends.Utilities.ThrowException.Definitions;

namespace Frends.Utilities.ThrowException;

/// <summary>
/// Utilities ThrowException task.
/// </summary>
public static class Utilities
{
    /// <summary>
    /// Throws an exception with given message.
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends-Utilities-ThrowException)
    /// </summary>
    /// <param name="input">Essential parameters.</param>
    /// <returns>object { bool Success, object Error { string Message, Exception AdditionalInfo } }</returns>
    public static Result ThrowException(
        [PropertyTab] Input input)
    {
        return string.IsNullOrWhiteSpace(input.InputString)
            ? new Result { Success = false }
            : throw new Exception(input.InputString);
    }
}
