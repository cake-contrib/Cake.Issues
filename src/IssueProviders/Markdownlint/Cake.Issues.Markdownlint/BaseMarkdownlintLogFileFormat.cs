namespace Cake.Issues.Markdownlint;

using Cake.Core.Diagnostics;

/// <summary>
/// Base class for all log file formats supported by the Markdownlint issue provider.
/// </summary>
/// <param name="log">The Cake log instance.</param>
public abstract class BaseMarkdownlintLogFileFormat(ICakeLog log)
    : BaseLogFileFormat<MarkdownlintIssuesProvider, MarkdownlintIssuesSettings>(log)
{
}
