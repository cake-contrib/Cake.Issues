namespace Cake.Issues.Build;

/// <summary>
/// Interface for settings affecting how issues are reported to build servers from issue providers.
/// </summary>
public interface IReportIssuesToBuildServerFromIssueProviderSettings : IReadIssuesSettings, IReportIssuesToBuildServerSettings
{
}