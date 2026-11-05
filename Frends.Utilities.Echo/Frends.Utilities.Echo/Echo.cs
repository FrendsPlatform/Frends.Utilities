using System.ComponentModel;
using Frends.Utilities.Echo.Definitions;

namespace Frends.Utilities.Echo;

/// <summary>
/// Utilities Echo task.
/// </summary>
public static class Utilities
{
    /// <summary>
    /// Returns given string.
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends.Utilities.Echo)
    /// </summary>
    /// <param name="input">Essential parameters.</param>
    /// <returns>object { bool Success, string ResultString, object Error { string Message, Exception AdditionalInfo } }</returns>
    public static Result Echo(
        [PropertyTab] Input input)
    {
        return new Result
        {
            Success = true,
            ResultString = input.InputString,
        };
    }
}
