using Frends.Utilities.Sleep.Definitions;
using NUnit.Framework;

namespace Frends.Utilities.Sleep.Test;

[TestFixture]
public class UnitTest
{
    [Test]
    public async Task TestSleep()
    {
        var input = new Input { Hours = 0, Minutes = 0, Seconds = 1, Milliseconds = 0 };
        var result = await Utilities.Sleep(input, new Options(), CancellationToken.None);
        Assert.That(result.Success, Is.True);
        Assert.That(result.Error, Is.Null);
    }

    [Test]
    public void TestSleepCanceledThrows()
    {
        var input = new Input { Seconds = 10 };
        using var cts = new CancellationTokenSource();
        cts.Cancel();
        Assert.ThrowsAsync<TaskCanceledException>(async () => await Utilities.Sleep(input, new Options(), cts.Token));
    }

    [Test]
    public async Task TestSleepInvalidInputReturnsError()
    {
        var input = new Input { Seconds = -1000000000 };
        var options = new Options { ThrowErrorOnFailure = false, ErrorMessageOnFailure = "Sleep failed" };
        var result = await Utilities.Sleep(input, options, CancellationToken.None);
        Assert.That(result.Success, Is.False);
        Assert.That(result.Error.Message, Does.StartWith("Sleep failed"));
    }
}
