namespace Cake.Issues;

/// <summary>
/// Possible format options for issues.
/// </summary>
public enum IssueCommentFormat
{
    /// <summary>
    /// Undefined format.
    /// </summary>
    Undefined,

    /// <summary>
    /// Plain text.
    /// </summary>
    PlainText,

    /// <summary>
    /// Hypertext markup language.
    /// </summary>
    Html,

    /// <summary>
    /// Markdown syntax.
    /// </summary>
    Markdown,
}
