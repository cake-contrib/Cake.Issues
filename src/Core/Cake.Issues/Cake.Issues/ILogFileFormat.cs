namespace Cake.Issues;

using System.Collections.Generic;

/// <summary>
/// Definition of a log file format for <see cref="IIssueProvider"/> which support different log file formats.
/// </summary>
/// <typeparam name="TIssueProvider">Type of the issue provider.</typeparam>
/// <typeparam name="TSettings">Type of the settings.</typeparam>
public interface ILogFileFormat<in TIssueProvider, in TSettings>
    where TIssueProvider : BaseMultiFormatIssueProvider<TSettings, TIssueProvider>
    where TSettings : BaseMultiFormatIssueProviderSettings<TIssueProvider, TSettings>
{
    /// <summary>
    /// Gets all issues.
    /// </summary>
    /// <param name="issueProvider">Issue provider instance.</param>
    /// <param name="repositorySettings">Repository settings to use.</param>
    /// <param name="issueProviderSettings">Settings for issue provider to use.</param>
    /// <returns>List of issues.</returns>
    IEnumerable<IIssue> ReadIssues(
        TIssueProvider issueProvider,
        IRepositorySettings repositorySettings,
        TSettings issueProviderSettings);
}
