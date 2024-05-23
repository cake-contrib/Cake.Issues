namespace Cake.Issues.EsLint;

using Cake.Core.Diagnostics;

/// <summary>
/// Base class for all ESLint log file format implementations.
/// </summary>
/// <param name="log">The Cake log instance.</param>
public abstract class BaseEsLintLogFileFormat(ICakeLog log)
    : BaseLogFileFormat<EsLintIssuesProvider, EsLintIssuesSettings>(log)
{
}
