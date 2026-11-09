using System.ComponentModel;
using Frends.Utilities.AlwaysTrue.Definitions;

namespace Frends.Utilities.AlwaysTrue;

/// <summary>
/// Task Class for Utilities operations.
/// </summary>
public static class Utilities
{
    /// <summary>
    /// Return boolean value true.
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends-Utilities-AlwaysTrue)
    /// </summary>
    /// <returns>object { bool Success }</returns>
    public static Result AlwaysTrue()
    {
        return new Result
        {
            Success = true,
        };
    }
}
