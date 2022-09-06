﻿using Frends.Utilities.Sleep.Definitions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Frends.Utilities.Sleep;

/// <summary>
/// Utilities Sleep task.
/// </summary>
public class Utilities
{
    /// <summary>
    /// A cancellable task that will complete after a time delay.
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends.Utilities.Sleep)
    /// </summary>
    /// <param name="input">Input parameters.</param>
    /// <param name="cancellationToken">Token generated by Frends to stop this task.</param>
    /// <returns>Object { bool SleepResult }</returns>
    public async static Task<Result> Sleep(Input input, CancellationToken cancellationToken)
    {
        var delay = new TimeSpan(0, input.Hours, input.Minutes, input.Seconds, input.Milliseconds);
        await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
        return new Result (true);
    }
}