using System.ComponentModel;
using Frends.Utilities.Echo.Definitions;
using Frends.Utilities.Echo.Helpers;

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
    /// <param name="options">Additional parameters.</param>
    /// <param name="cancellationToken">A cancellation token provided by Frends Platform.</param>
    /// <returns>object { bool Success, string ResultString, object Error { string Message, Exception AdditionalInfo } }</returns>
    public static Result Echo(
        [PropertyTab] Input input,
        [PropertyTab] Options options,
        CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            return new Result
            {
                Success = true,
                ResultString = input.InputString,
                Error = null,
            };
        }
        catch (Exception ex)
        {
            return ex.Handle(options);
        }
    }
}
