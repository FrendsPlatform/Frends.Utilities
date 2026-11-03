using System.ComponentModel;
using Frends.Utilities.AlwaysTrue.Definitions;
using Frends.Utilities.AlwaysTrue.Helpers;

namespace Frends.Utilities.AlwaysTrue;

/// <summary>
/// Task Class for Utilities operations.
/// </summary>
public static class Utilities
{
    /// <summary>
    /// Return boolean value true.
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends-Utilities-AlwaysTrue)
    /// </summary>
    /// <param name="input">Essential parameters.</param>
    /// <param name="options">Additional parameters.</param>
    /// <param name="cancellationToken">A cancellation token provided by Frends Platform.</param>
    /// <returns>object { bool Success, bool TaskResult, object Error { string Message, Exception AdditionalInfo } }</returns>
    public static Result AlwaysTrue(
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
                TaskResult = true,
                Error = null,
            };
        }
        catch (Exception ex)
        {
            return ex.Handle(options);
        }
    }
}
