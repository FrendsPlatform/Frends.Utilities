using Frends.Utilities.Echo.Definitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Frends.Utilities.Echo.Test;

[TestClass]
public class UnitTest
{
    [TestMethod]
    public void TestResultTrue()
    {
        Input input = new() { InputString = "Test message." };
        var result = Utilities.Echo(input, new Options(), CancellationToken.None);
        Assert.IsTrue(result.Success);
        Assert.IsTrue(result.ResultString.Equals("Test message."));
    }
}
