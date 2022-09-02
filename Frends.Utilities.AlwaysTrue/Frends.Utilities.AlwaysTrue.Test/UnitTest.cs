using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Frends.Utilities.AlwaysTrue.Test;

[TestClass]
public class UnitTest
{
    [TestMethod]
    public void TestResultTrue()
    {
        var result = Utilities.AlwaysTrue();
        Assert.IsTrue(result.TaskResult); 
    }
}