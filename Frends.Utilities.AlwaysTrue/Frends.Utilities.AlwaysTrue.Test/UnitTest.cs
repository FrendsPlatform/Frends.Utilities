using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Frends.Utilities.AlwaysTrue.Test;

[TestClass]
public class UnitTest
{
    [TestMethod]
    public void TestResultTrue()
    {
        Assert.IsTrue(Utilities.AlwaysTrue().Equals(true));
    }
}