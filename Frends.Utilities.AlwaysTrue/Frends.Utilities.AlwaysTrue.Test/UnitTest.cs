using Frends.Utilities.AlwaysTrue.Definitions;
using NUnit.Framework;

namespace Frends.Utilities.AlwaysTrue.Test;

[TestFixture]
public class UnitTest
{
    [Test]
    public void TestResultTrue()
    {
        var result = Utilities.AlwaysTrue();
        Assert.That(result.Success, Is.True);
    }
}
