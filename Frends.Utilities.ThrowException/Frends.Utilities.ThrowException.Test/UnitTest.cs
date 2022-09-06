using Frends.Utilities.ThrowException.Definitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Frends.Utilities.ThrowException.Test;

[TestClass]
public class UnitTest
{
    [TestMethod]
    public void TestResultTrue()
    {
        Input input = new() { InputString = "Test message." };
        Assert.ThrowsException<Exception>(() => Utilities.ThrowException(input)).Message.Contains("Test message.");
    }

    [TestMethod]
    public void TestResultFalse()
    {
        Input input = new() { InputString = "" };
        var result = Utilities.ThrowException(input);
        Assert.IsTrue(result.ExceptionThrown.Equals(false));
    }
}