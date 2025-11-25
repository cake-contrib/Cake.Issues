namespace Cake.Issues.MsBuild;

using Cake.Core.Diagnostics;

/// <summary>
/// Base class for all MSBuild log file format.
/// </summary>
/// <param name="log">The Cake log instance.</param>
public abstract class BaseMsBuildLogFileFormat(ICakeLog log)
    : BaseLogFileFormat<MsBuildIssuesProvider, MsBuildIssuesSettings>(log)
{
}
