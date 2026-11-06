using Frends.Utilities.ThrowException.Definitions;
using NUnit.Framework;

namespace Frends.Utilities.ThrowException.Test;

[TestFixture]
public class UnitTest
{
    [Test]
    public void ThrowsWhenMessageGiven()
    {
        var input = new Input { InputString = "Test message." };
        var ex = Assert.Throws<Exception>(() => Utilities.ThrowException(input));
        Assert.That(ex.Message, Is.EqualTo("Test message."));
    }

    [Test]
    public void ReturnsFalseWhenMessageEmpty()
    {
        var input = new Input { InputString = string.Empty };
        var result = Utilities.ThrowException(input);
        Assert.That(result.Success, Is.False);
    }
}
