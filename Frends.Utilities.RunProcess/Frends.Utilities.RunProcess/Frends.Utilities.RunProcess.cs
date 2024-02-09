﻿namespace Frends.Utilities.RunProcess;

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Frends.Utilities.RunProcess.Definitions;

/// <summary>
/// Main class of the Task.
/// </summary>
public static class Utilities
{
    /// <summary>
    /// Frends Task for running processes in local machine.
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends.Utilities.RunProcess).
    /// </summary>
    /// <param name="input">Input parameters.</param>
    /// <param name="options">Options parameters.</param>
    /// <returns>Object { int ExitCode, string Output, string StdErr }</returns>
    public static Result RunProcess([PropertyTab] Input input, [PropertyTab] Options options)
    {
        using var process = new Process();

        process.StartInfo.UseShellExecute = false;
        foreach (var item in input.Arguments)
            process.StartInfo.ArgumentList.Add(item);
        process.StartInfo.FileName = input.FileName;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardInput = options.RedirectStandardInput;

        var stdoutSb = new StringBuilder();
        var stderrSb = new StringBuilder();

        using var outputWaitHandle = new AutoResetEvent(false);
        using var errorWaitHandle = new AutoResetEvent(false);

        void ProcessOnOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            try
            {
                if (e.Data == null)
                    outputWaitHandle.Set();
                else
                    stdoutSb.AppendLine(e.Data);
            }
            catch (Exception exception)
            {
                Trace.TraceError($"Error while executing process {input.FileName} and handling output: {exception}");
            }
        }

        void ProcessOnErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            try
            {
                if (e.Data == null)
                    errorWaitHandle.Set();
                else
                    stderrSb.AppendLine(e.Data);
            }
            catch (Exception exception)
            {
                Trace.TraceError($"Error while executing process {input.FileName} and handling error output: {exception}");
            }
        }

        try
        {
            process.OutputDataReceived += ProcessOnOutputDataReceived;
            process.ErrorDataReceived += ProcessOnErrorDataReceived;

            // Start the process. All event handlers etc. are now registered
            process.Start();

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            // convert timeout seconds to milliseconds
            var timeoutMS = options.TimeoutSeconds * 1000;

            // Also wait events to be done
            if (process.WaitForExit(timeoutMS) && outputWaitHandle.WaitOne(timeoutMS) && errorWaitHandle.WaitOne(timeoutMS))
            {
                if (process.HasExited)
                {
                    // Exited - return object / throw error
                    if (process.ExitCode != 0 && options.ThrowExceptionOnErrorResponse)
                        throw new ApplicationException($"External process execution failed with returncode: {process.ExitCode} and output: {Environment.NewLine}{stderrSb}");

                    return new Result(process.ExitCode, stdoutSb.ToString(), stderrSb.ToString());
                }
                else
                {
                    // Timeout & process is runnig
                    if (options.KillProcessAfterTimeout)
                        process.Kill();

                    throw new TimeoutException($"External process <{process.Id}> execution timed out after {options.TimeoutSeconds} seconds. (1)");
                }
            }
            else
            {
                // Timeout & process is runnimg
                if (!process.HasExited && options.KillProcessAfterTimeout)
                    process.Kill();

                throw new TimeoutException($"External process <{process.Id}> execution timed out after {options.TimeoutSeconds} seconds. (2)");
            }
        }
        finally
        {
            process.OutputDataReceived -= ProcessOnOutputDataReceived;
            process.ErrorDataReceived -= ProcessOnErrorDataReceived;
        }
    }
}
