namespace Cake.Issues;

/// <summary>
/// Extension methods for <see cref="string"/>.
/// </summary>
public static class StringExtensions
{
    private const string NotSetString = "[italic grey]Not set[/]";

    /// <summary>
    /// Returns the string value or a <c>Not set</c>> markup suitable for Spectre.Console.
    /// </summary>
    /// <param name="value">String which should be returned.</param>
    /// <returns>String value or a <c>Not set</c>> markup.</returns>
    public static string ToStringWithNullMarkup(this object value) =>
        value == null ? NotSetString : value.ToString();
}
