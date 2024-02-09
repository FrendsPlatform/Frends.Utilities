namespace Frends.Utilities.RunProcess.Tests;

using Frends.Utilities.RunProcess.Definitions;
using NUnit.Framework;
using System;
using System.IO;

[TestFixture]
internal class UnitTests
{
    private readonly string _testDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../TestData/");
    private readonly string _inputFile = "file8kb.txt";
    private readonly bool _windows = !Environment.OSVersion.Platform.ToString().Equals("Unix");

    private string _testFilePath;
    private Input input;

    [SetUp]
    public void Setup()
    {
        input = new Input()
        {
            Platform = _windows ? Platform.Windows : Platform.Unix,
            FileName = _windows ? "cmd.exe" : "/bin/bash",
        };

        if (!Directory.Exists(_testDir))
        {
            Directory.CreateDirectory(_testDir);
            File.WriteAllText(Path.Combine(_testDir, _inputFile), new string('a', (8 * 1024) + 5));
        }

        _testFilePath = Path.GetFullPath(Path.Combine(_testDir, _inputFile));
    }

    [TearDown]
    public void TearDown()
    {
        if (Directory.Exists(_testDir))
            Directory.Delete(_testDir, true);
    }

    [Test]
    public void RunProcess_Echo()
    {
        var args = _windows
            ? new string[] { "/C", "echo Hello" }
            : new string[] { "-c", @"echo ""Hello""" };

        input.Arguments = args;

        var options = new Options
        {
            KillProcessAfterTimeout = false,
            TimeoutSeconds = 15,
            RedirectStandardInput = true,
            ThrowExceptionOnErrorResponse = true,
        };

        var result = Utilities.RunProcess(input, options);
        Assert.AreEqual($"Hello{Environment.NewLine}", result.Output);
    }

    [Test]
    public void RunProcess_TimeoutNoKillProcess()
    {
        var args = _windows
            ? new string[] { "/C", "timeout /t 30 > NUL 2>&1" }
            : new string[] { "-c", "sleep 30 > /dev/null 2>&1" };

        input.Arguments = args;
        var options = new Options
        {
            KillProcessAfterTimeout = false,
            TimeoutSeconds = 15,
            RedirectStandardInput = false,
            ThrowExceptionOnErrorResponse = true,
        };

        var ex = Assert.Throws<TimeoutException>(() => Utilities.RunProcess(input, options));
        Assert.IsTrue(ex.Message.Contains("External process"));
        Assert.IsTrue(ex.Message.Contains("execution timed out after 15 seconds. (2)"));
    }

    [Test]
    public void RunProcess_RunMultipleArguments()
    {
        var testFileWithPath = _windows ? Path.Combine(_testDir, "testWin.txt") : Path.Combine(_testDir, "testUnix.txt");
        var args = _windows
            ? new string[] { "/C", "set", "/A", "1+10", $">{testFileWithPath}" }
            : new string[] { "-c", $"echo $((1+10)) > {testFileWithPath}" };

        input.Arguments = args;
        var options = new Options { KillProcessAfterTimeout = false, TimeoutSeconds = 30, RedirectStandardInput = false };

        Utilities.RunProcess(input, options);
        if (_windows)
            Assert.AreEqual("11", File.ReadAllText(testFileWithPath));
        else
            Assert.AreEqual($"11{Environment.NewLine}", File.ReadAllText(testFileWithPath));
    }

    [Test]
    public void RunProcess_TimeoutKillProcess()
    {
        var args = _windows
            ? new string[] { "/C timeout 30 /nobreak >NUL" }
            : new string[] { "-c", "sleep 30 > /dev/null 2>&1" };

        input.Arguments = args;
        var options = new Options
        {
            KillProcessAfterTimeout = true,
            TimeoutSeconds = 15,
            RedirectStandardInput = false,
            ThrowExceptionOnErrorResponse = true,
        };

        var ex = Assert.Throws<TimeoutException>(() => Utilities.RunProcess(input, options));
        Assert.IsTrue(ex.Message.Contains("External process"));
        Assert.IsTrue(ex.Message.Contains("execution timed out after 15 seconds. (2)"));
    }

    [Test]
    public void RunProcess_FillSTDOUT()
    {
        var args = _windows
            ? new string[] { $"/C type {_testFilePath}" }
            : new string[] { "-c", $"cat {_testFilePath}" };

        input.Arguments = args;
        var options = new Options { KillProcessAfterTimeout = false, TimeoutSeconds = 30, RedirectStandardInput = false };

        var result = Utilities.RunProcess(input, options);

        Assert.IsTrue(result.Output.Length >= 8096 + 5);
        Assert.IsTrue(result.Output[1234] == 'a');
    }

    [Test]
    public void RunProcess_FillSTDOUTTimeout30secsKillProcess()
    {
        var args = _windows
            ? new string[] { "/C timeout 30 /nobreak >NUL" }
            : new string[] { "-c", $"sleep 30 > /dev/null 2>&1" };

        input.Arguments = args;
        var options = new Options { KillProcessAfterTimeout = true, TimeoutSeconds = 15, RedirectStandardInput = false, ThrowExceptionOnErrorResponse = true };

        var ex = Assert.Throws<TimeoutException>(() => Utilities.RunProcess(input, options));
        Assert.IsTrue(ex.Message.Contains("External process"));
        Assert.IsTrue(ex.Message.Contains("execution timed out after 15 seconds. (2)"));
    }

    [Test]
    public void RunProcess_FailingProcess()
    {
        var args = _windows
            ? new string[] { "/C type filethatdontexist.txt" }
            : new string[] { "-c", @"echo  ""$(<filethatdontexist.txt)""" };

        input.Arguments = args;
        var options = new Options { KillProcessAfterTimeout = false, TimeoutSeconds = 30, RedirectStandardInput = false, ThrowExceptionOnErrorResponse = false };

        var result = Utilities.RunProcess(input, options);
        if (_windows)
            Assert.AreEqual($"The system cannot find the file specified.{Environment.NewLine}", result.StdErr);
        else
            Assert.AreEqual($"/bin/bash: line 1: filethatdontexist.txt: No such file or directory{Environment.NewLine}", result.StdErr);
    }

    [Test]
    public void RunProcess_STDERR()
    {
        var args = _windows
        ? new string[] { "/C type filethatdontexist.txt" }
        : new string[] { "-c", @"echo  ""$(<filethatdontexist.txt)""" };

        input.Arguments = args;
        var options = new Options { KillProcessAfterTimeout = false, TimeoutSeconds = 30, RedirectStandardInput = false, ThrowExceptionOnErrorResponse = true };

        var ex = Assert.Throws<ApplicationException>(() => Utilities.RunProcess(input, options));
        if (_windows)
            Assert.AreEqual($"External process execution failed with returncode: 1 and output: {Environment.NewLine}The system cannot find the file specified.{Environment.NewLine}", ex.Message);
        else
            Assert.AreEqual($"External process execution failed with returncode: 1 and output: {Environment.NewLine}/bin/bash: line 1: filethatdontexist.txt: No such file or directory{Environment.NewLine}", ex.Message);
    }
}
