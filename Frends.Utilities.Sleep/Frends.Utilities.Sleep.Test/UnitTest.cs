using Frends.Utilities.Sleep.Definitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Frends.Utilities.Sleep.Test;

[TestClass]
public class UnitTest
{
    [TestMethod]
    public async Task TestSleep()
    {
        Input input = new() { Hours = 0, Minutes = 0, Seconds = 10, Milliseconds = 0};
        var result = await Utilities.Sleep(input, default);
        Assert.IsTrue(result.SleepResult.Equals(true));
    }
}