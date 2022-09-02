using Frends.Utilities.AlwaysTrue.Definitions;

namespace Frends.Utilities.AlwaysTrue;

/// <summary>
/// Utilities AlwaysTrue task.
/// </summary>
public class Utilities
{
    /// <summary>
    /// Return boolean value true.
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends.Utilities.AlwaysTrue)
    /// </summary>
    /// <returns>Object { bool TaskResult }</returns>
    public static Result AlwaysTrue()
    {
       return new Result(true);
    }
}