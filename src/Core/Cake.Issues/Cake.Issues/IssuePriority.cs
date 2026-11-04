namespace Cake.Issues;

/// <summary>
/// Default priorities for <see cref="IIssue.Priority"/>.
/// </summary>
public enum IssuePriority
{
    /// <summary>
    /// Undefined priority.
    /// </summary>
    Undefined = 0,

    /// <summary>
    /// Issues which brings attention to a specific part of the code or recommends a way of improvement.
    /// </summary>
    Hint = 100,

    /// <summary>
    /// Issues which aren't necessarily bad or wrong, but probably useful to know.
    /// </summary>
    Suggestion = 200,

    /// <summary>
    /// Issues that do not prevent code from compiling but may nevertheless represent serious coding inefficiencies.
    /// </summary>
    Warning = 300,

    /// <summary>
    /// Issues that either prevent code from compiling or result in runtime errors.
    /// </summary>
    Error = 400,
}
