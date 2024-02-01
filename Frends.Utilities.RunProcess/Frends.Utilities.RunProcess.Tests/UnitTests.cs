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
    private readonly string _process = Environment.ExpandEnvironmentVariables(@"%windir%\system32\cmd.exe");

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
    public void RunMultipleArgs()
    {
        var testFileWithPath = Path.Combine(_testDir, @"test4.txt");

        var args = new[]
        {
            new Argument { Name = "/C", Value = "set" },
            new Argument { Name = "/A", Value = "(1+10)" },
            new Argument { Name = ">>", Value = testFileWithPath },
        };

        var input = new Input { FileName = _process, Arguments = args };
        var options = new Options { KillProcessAfterTimeout = false, TimeoutSeconds = 30, RedirectStandardInput = false };

        Utilities.RunProcess(input, options, default);

        Assert.AreEqual(File.ReadAllText(testFileWithPath), "11");
    }

    [Test]
    public void TimeoutNoKillProcess()
    {
        ActualValueDelegate<object> test = TestBaseTimeoutKill(false);
        Assert.That(test, Throws.TypeOf<TimeoutException>());
    }

    [Test]
    public void TimeoutKillProcess()
    {
        ActualValueDelegate<object> test = TestBaseTimeoutKill(true);
        Assert.That(test, Throws.TypeOf<TimeoutException>());
    }

    [Test]
    public void FillSTDOUT()
    {
        var testFileWithPath = Path.Combine(_testDir, _inputFile);

        var args = new[]
        {
            new Argument { Name = "/C", Value = $"type {testFileWithPath}" },
        };

        var input = new Input { FileName = _process, Arguments = args };
        var options = new Options { KillProcessAfterTimeout = false, TimeoutSeconds = 30, RedirectStandardInput = false };

        var result = Utilities.RunProcess(input, options, default);

        Assert.IsTrue(result.Output.Length >= 8096 + 5);
        Assert.IsTrue(result.Output[1234] == 'a');

        input = new Input { FileName = "cmd.exe", Arguments = args };
        options = new Options { KillProcessAfterTimeout = false, TimeoutSeconds = 30, RedirectStandardInput = false };

        result = Utilities.RunProcess(input, options, default);

        Assert.IsTrue(result.Output.Length >= 8096 + 5);
        Assert.IsTrue(result.Output[1234] == 'a');
    }

    [Test]
    public void FillSTDOUTTimeout30secsKillProcess()
    {
        ActualValueDelegate<object> test = TestBufferTimeoutKill();
        Assert.That(test, Throws.TypeOf<TimeoutException>());
    }

    [Test]
    public void Test()
    {
        var args = new[]
        {
            new Argument { Name = "/C", Value = "echo foo" },
            new Argument { Name = "/C", Value = "exit 1" },
        };

        var input = new Input { FileName = "cmd.exe", Arguments = args };
        var options = new Options { KillProcessAfterTimeout = false, TimeoutSeconds = 30, RedirectStandardInput = false, ThrowExceptionOnErrorResponse = true };

        var result = Utilities.RunProcess(input, options, default);
        Console.WriteLine(result.Output);
    }

    [Test]
    public void STDERR()
    {
        var args = new[]
        {
            new Argument { Name = "/C", Value = "exit 1" },
        };

        var input = new Input { FileName = "cmd.exe", Arguments = args };
        var options = new Options { KillProcessAfterTimeout = false, TimeoutSeconds = 30, RedirectStandardInput = false, ThrowExceptionOnErrorResponse = true };

        var ex = Assert.Throws<ApplicationException>(() => Utilities.RunProcess(input, options, default));
        Console.WriteLine(ex.Message);
        Assert.IsTrue(ex.Message.Contains("External process execution failed with returncode: 1"));
    }

    private ActualValueDelegate<object> TestBufferTimeoutKill()
    {
        var testFileWithPath = Path.Combine(_testDir, _inputFile);

        var args = new[]
        {
            new Argument { Name = "/C", Value = $"type {testFileWithPath} && timeout 60 /nobreak >NUL" },
        };

        var input = new Input { FileName = _process, Arguments = args };
        var options = new Options { KillProcessAfterTimeout = true, TimeoutSeconds = 30, RedirectStandardInput = false };
        return () => Utilities.RunProcess(input, options, default);
    }

    private ActualValueDelegate<object> TestBaseTimeoutKill(bool optionsKillProcess)
    {
        var args = new[]
        {
            new Argument { Name = "/C", Value = "timeout 10 /nobreak >NUL" },
        };

        var input = new Input { FileName = _process, Arguments = args };
        var options = new Options { KillProcessAfterTimeout = optionsKillProcess, TimeoutSeconds = 5, RedirectStandardInput = false };

        return () => Utilities.RunProcess(input, options, default);
    }
}
