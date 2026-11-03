using Frends.Utilities.ThrowException.Definitions;
using NUnit.Framework;

namespace Frends.Utilities.ThrowException.Test;

[TestFixture]
public class UnitTest
{
    [Test]
    public void TestThrowsWhenMessageGiven()
    {
        var input = new Input { InputString = "Test message." };
        var ex = Assert.Throws<Exception>(() => Utilities.ThrowException(input, new Options(), CancellationToken.None));
        Assert.That(ex.Message, Is.EqualTo("Test message."));
    }

    [Test]
    public void TestSuccessWhenMessageEmpty()
    {
        var input = new Input { InputString = string.Empty };
        var result = Utilities.ThrowException(input, new Options(), CancellationToken.None);
        Assert.That(result.Success, Is.True);
        Assert.That(result.Error, Is.Null);
    }

    [Test]
    public void TestReturnsErrorWhenThrowDisabled()
    {
        var input = new Input { InputString = "Test message." };
        var options = new Options { ThrowErrorOnFailure = false, ErrorMessageOnFailure = "Custom" };
        var result = Utilities.ThrowException(input, options, CancellationToken.None);
        Assert.That(result.Success, Is.False);
        Assert.That(result.Error.Message, Is.EqualTo("Custom: Test message."));
    }
}
