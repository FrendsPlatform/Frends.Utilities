namespace Frends.Utilities.RunProcess.Tests;

using Frends.Utilities.RunProcess.Definitions;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.IO;

[TestFixture]
internal class UnitTests
{
    private readonly string _testDir = Path.Combine(Path.GetTempPath(), @"ExecTests" + DateTime.Now.ToString("yyyyMMdd_HHmmss"));
    private readonly string _inputFile = "file8kb.txt";
    private string _process;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _process = Environment.OSVersion.Platform.ToString().Equals("Unix") ? "/bin/bash" : "cmd.exe";
    }

    [SetUp]
    public void Setup()
    {
        if (!Directory.Exists(_testDir))
        {
            Directory.CreateDirectory(_testDir);
            File.WriteAllText(Path.Combine(_testDir, _inputFile), new string('a', (8 * 1024) + 5));
        }
    }

    [TearDown]
    public void TearDown()
    {
        if (Directory.Exists(_testDir))
            Directory.Delete(_testDir, true);
    }

    [Test]
    public void RunProcess_RunMultipleArgs()
    {
        var testFileWithPath = Path.Combine(_testDir, "test4.txt");

        var args = new[]
        {
            new Argument { Name = "/C", Value = "set" },
            new Argument { Name = "/A", Value = "(1+10)" },
            new Argument { Name = ">>", Value = testFileWithPath },
        };

        var input = new Input { FileName = _process, Arguments = args };
        var options = new Options { KillProcessAfterTimeout = false, TimeoutSeconds = 30, RedirectStandardInput = false };

        Utilities.RunProcess(input, options);

        Assert.AreEqual(File.ReadAllText(testFileWithPath), "11");
    }

    [Test]
    public void RunProcess_TimeoutNoKillProcess()
    {
        var args = new[]
        {
            new Argument { Name = "/C", Value = "timeout 30 /nobreak >NUL" },
        };

        var input = new Input { FileName = _process, Arguments = args };
        var options = new Options
        {
            KillProcessAfterTimeout = false,
            TimeoutSeconds = 15,
            RedirectStandardInput = false,
            ThrowExceptionOnErrorResponse = true,
        };

        var ex = Assert.Throws<TimeoutException>(() => Utilities.RunProcess(input, options));
        Console.WriteLine(ex.Message);
        Assert.IsTrue(ex.Message.Contains("External process"));
        Assert.IsTrue(ex.Message.Contains("execution timed out after 15 seconds. (2)"));
    }

    [Test]
    public void RunProcess_TimeoutKillProcess()
    {
        var args = new[]
        {
            new Argument { Name = "/C", Value = "timeout 30 /nobreak >NUL" },
        };

        var input = new Input { FileName = _process, Arguments = args };
        var options = new Options
        {
            KillProcessAfterTimeout = true,
            TimeoutSeconds = 15,
            RedirectStandardInput = false,
            ThrowExceptionOnErrorResponse = true,
        };

        var ex = Assert.Throws<TimeoutException>(() => Utilities.RunProcess(input, options));
        Console.WriteLine(ex.Message);
        Assert.IsTrue(ex.Message.Contains("External process"));
        Assert.IsTrue(ex.Message.Contains("execution timed out after 15 seconds. (2)"));
    }

    [Test]
    public void RunProcess_FillSTDOUT()
    {
        var testFileWithPath = Path.Combine(_testDir, _inputFile);
        var args = new[]
        {
            new Argument { Name = "/C", Value = $"type {testFileWithPath}" },
        };

        var input = new Input { FileName = _process, Arguments = args };
        var options = new Options { KillProcessAfterTimeout = false, TimeoutSeconds = 30, RedirectStandardInput = false };

        var result = Utilities.RunProcess(input, options);

        Assert.IsTrue(result.Output.Length >= 8096 + 5);
        Assert.IsTrue(result.Output[1234] == 'a');

        input = new Input { FileName = _process, Arguments = args };
        options = new Options { KillProcessAfterTimeout = false, TimeoutSeconds = 30, RedirectStandardInput = false };

        result = Utilities.RunProcess(input, options);

        Assert.IsTrue(result.Output.Length >= 8096 + 5);
        Assert.IsTrue(result.Output[1234] == 'a');
    }

    [Test]
    public void RunProcess_FillSTDOUTTimeout30secsKillProcess()
    {
        var testFileWithPath = Path.Combine(_testDir, _inputFile);
        var args = new[]
        {
            new Argument { Name = "/C", Value = $"timeout 120 /nobreak >NUL" },
        };

        var input = new Input { FileName = _process, Arguments = args };
        var options = new Options { KillProcessAfterTimeout = true, TimeoutSeconds = 60, RedirectStandardInput = false };

        var ex = Assert.Throws<TimeoutException>(() => Utilities.RunProcess(input, options));
        Console.WriteLine(ex.Message);
        Assert.IsTrue(ex.Message.Contains("External process"));
        Assert.IsTrue(ex.Message.Contains("execution timed out after 60 seconds. (2)"));
    }

    [Test]
    public void RunProcess_FailingProcess()
    {
        var args = new[]
        {
            new Argument { Name = "/C", Value = "type filethatdontexist.txt" },
        };

        var input = new Input { FileName = _process, Arguments = args };
        var options = new Options { KillProcessAfterTimeout = false, TimeoutSeconds = 30, RedirectStandardInput = false, ThrowExceptionOnErrorResponse = false };

        var result = Utilities.RunProcess(input, options);
        Assert.AreEqual($"The system cannot find the file specified.{Environment.NewLine}", result.StdErr);
    }

    [Test]
    public void RunProcess_STDERR()
    {
        var args = new[]
        {
            new Argument { Name = "/C", Value = "type filethatdontexist.txt" },
        };

        var input = new Input { FileName = _process, Arguments = args };
        var options = new Options { KillProcessAfterTimeout = false, TimeoutSeconds = 30, RedirectStandardInput = false, ThrowExceptionOnErrorResponse = true };

        var ex = Assert.Throws<ApplicationException>(() => Utilities.RunProcess(input, options));
        Assert.AreEqual($"External process execution failed with returncode: 1 and output: {Environment.NewLine}The system cannot find the file specified.{Environment.NewLine}", ex.Message);
    }
}
