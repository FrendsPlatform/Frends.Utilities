using System.ComponentModel;
using Frends.Utilities.Sleep.Definitions;
using Frends.Utilities.Sleep.Helpers;

namespace Frends.Utilities.Sleep;

/// <summary>
/// Utilities Sleep task.
/// </summary>
public static class Utilities
{
    /// <summary>
    /// A cancellable task that will complete after a time delay.
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends-Utilities-Sleep)
    /// </summary>
    /// <param name="input">Essential parameters.</param>
    /// <param name="options">Additional parameters.</param>
    /// <param name="cancellationToken">A cancellation token provided by Frends Platform.</param>
    /// <returns>object { bool Success, object Error { string Message, Exception AdditionalInfo } }</returns>
    public static async Task<Result> Sleep(
        [PropertyTab] Input input,
        [PropertyTab] Options options,
        CancellationToken cancellationToken)
    {
        try
        {
            var delay = new TimeSpan(0, input.Hours, input.Minutes, input.Seconds, input.Milliseconds);
            await Task.Delay(delay, cancellationToken).ConfigureAwait(false);

            return new Result
            {
                Success = true,
                Error = null,
            };
        }
        catch (Exception ex)
        {
            return ex.Handle(options);
        }
    }
}
