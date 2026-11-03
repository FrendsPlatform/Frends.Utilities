using System.ComponentModel;
using Frends.Utilities.ThrowException.Definitions;
using Frends.Utilities.ThrowException.Helpers;

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
    /// <param name="options">Additional parameters.</param>
    /// <param name="cancellationToken">A cancellation token provided by Frends Platform.</param>
    /// <returns>object { bool Success, object Error { string Message, Exception AdditionalInfo } }</returns>
    public static Result ThrowException(
        [PropertyTab] Input input,
        [PropertyTab] Options options,
        CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            return string.IsNullOrWhiteSpace(input.InputString)
                ? new Result
                {
                    Success = true,
                    Error = null,
                }
                : throw new Exception(input.InputString);
        }
        catch (Exception ex)
        {
            return ex.Handle(options);
        }
    }
}
