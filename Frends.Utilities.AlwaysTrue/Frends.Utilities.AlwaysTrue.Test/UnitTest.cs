using Frends.Utilities.AlwaysTrue.Definitions;
using NUnit.Framework;

namespace Frends.Utilities.AlwaysTrue.Test;

[TestFixture]
public class UnitTest
{
    [Test]
    public void TestResultTrue()
    {
        var result = Utilities.AlwaysTrue(new Input(), new Options(), CancellationToken.None);
        Assert.That(result.Success, Is.True);
        Assert.That(result.Error, Is.Null);
    }

    [Test]
    public void TestCanceledThrows()
    {
        using var cts = new CancellationTokenSource();
        cts.Cancel();
        Assert.Throws<OperationCanceledException>(() => Utilities.AlwaysTrue(new Input(), new Options(), cts.Token));
    }
}
