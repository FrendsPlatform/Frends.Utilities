namespace Frends.Utilities.RunProcess;

using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Frends.Utilities.RunProcess.Definitions;
using Frends.Utilities.RunProcess.Helpers;

/// <summary>
/// Main class of the Task.
/// </summary>
public static class Utilities
{
    /// <summary>
    /// Frends Task for running cmd or bash commands in a local machine.
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends.Utilities.RunProcess).
    /// </summary>
    /// <param name="input">Input parameters.</param>
    /// <param name="options">Options parameters.</param>
    /// <param name="cancellationToken">A cancellation token provided by Frends Platform.</param>
    /// <returns>object { bool Success, int ExitCode, string Output, string StdErr, object Error { string Message, Exception AdditionalInfo } }</returns>
    public static Result RunProcess([PropertyTab] Input input, [PropertyTab] Options options, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Execute(input, options);
        }
        catch (Exception ex)
        {
            return ex.Handle(options);
        }
    }

    private static Result Execute(Input input, Options options)
    {
        var startInfo = new ProcessStartInfo
        {
            UseShellExecute = false,
            FileName = input.FileName,
            WindowStyle = ProcessWindowStyle.Hidden,
            CreateNoWindow = true,
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            RedirectStandardInput = options.RedirectStandardInput,
        };

        if (!string.IsNullOrEmpty(input.WorkingDirectory))
            startInfo.WorkingDirectory = input.WorkingDirectory;

        foreach (var item in input.Arguments)
            startInfo.ArgumentList.Add(item);

        using var process = new Process();
        process.StartInfo = startInfo;

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
                    else if (!string.IsNullOrEmpty(stderrSb.ToString()) && options.ThrowExceptionOnErrorResponse)
                        throw new ApplicationException($"External process execution failed with returncode: 1 and output: {Environment.NewLine}{stderrSb}");

                    return new Result
                    {
                        Success = true,
                        ExitCode = process.ExitCode,
                        Output = stdoutSb.ToString(),
                        StdErr = stderrSb.ToString(),
                        Error = null,
                    };
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
