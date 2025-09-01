namespace Cake.Issues.Build;

using Cake.Core.IO;

/// <summary>
/// Settings affecting how issues are reported to build servers from issue providers.
/// </summary>
/// <param name="repositoryRoot">Root path of the repository.</param>
public class ReportIssuesToBuildServerFromIssueProviderSettings(DirectoryPath repositoryRoot) : ReportIssuesToBuildServerSettings(repositoryRoot), IReportIssuesToBuildServerFromIssueProviderSettings
{
    /// <inheritdoc />
    public string Run { get; set; }

    /// <inheritdoc />
    public FileLinkSettings FileLinkSettings { get; set; }
}