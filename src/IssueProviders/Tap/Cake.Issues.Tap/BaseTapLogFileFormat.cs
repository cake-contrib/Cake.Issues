namespace Cake.Issues.Tap;

using Cake.Core.Diagnostics;

/// <summary>
/// Base class for all log file formats supported by the TAP issue provider.
/// </summary>
/// <param name="log">The Cake log instance.</param>
public abstract class BaseTapLogFileFormat(ICakeLog log)
    : BaseLogFileFormat<TapIssuesProvider, TapIssuesSettings>(log)
{
}
