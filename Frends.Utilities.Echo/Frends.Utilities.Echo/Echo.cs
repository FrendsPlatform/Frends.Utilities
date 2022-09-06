using Frends.Utilities.Echo.Definitions;
namespace Frends.Utilities.Echo;

/// <summary>
/// Utilities Echo task.
/// </summary>
public class Utilities
{
    /// <summary>
    /// Returns given string.
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends.Utilities.Sleep)
    /// </summary>
    /// <param name="input">Input parameter.</param>
    /// <returns>Object { string ResultString }</returns>
    public static Result Echo(Input input)
    {
        return new Result(input.InputString);
    }
}