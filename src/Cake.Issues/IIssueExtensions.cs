namespace Cake.Issues
{
    /// <summary>
    /// Extensions for <see cref="IIssue"/>.
    /// </summary>
    public static class IIssueExtensions
    {
        /// <summary>
        /// Returns the full path of <see cref="IIssue.ProjectFileRelativePath"/> or <c>null</c>.
        /// </summary>
        /// <param name="issue">Issue for which the path should be returned.</param>
        /// <returns>Full path to the project to which the file affected by the issue belongs.</returns>
        public static string ProjectPath(this IIssue issue)
        {
            issue.NotNull(nameof(issue));

            return issue.ProjectFileRelativePath?.FullPath;
        }

        /// <summary>
        /// Returns the directory of the <see cref="IIssue.ProjectFileRelativePath"/>.
        /// </summary>
        /// <param name="issue">Issue for which the project directory should be returned.</param>
        /// <returns>Directory of the project to which the file affected by the issue belongs.</returns>
        public static string ProjectDirectory(this IIssue issue)
        {
            issue.NotNull(nameof(issue));

            return issue.ProjectFileRelativePath?.GetDirectory().FullPath;
        }

        /// <summary>
        /// Returns the full path of the <see cref="IIssue.AffectedFileRelativePath"/>.
        /// </summary>
        /// <param name="issue">Issue for which the path should be returned.</param>
        /// <returns>Full path of the file affected by the issue.</returns>
        public static string FilePath(this IIssue issue)
        {
            issue.NotNull(nameof(issue));

            return issue.AffectedFileRelativePath?.FullPath;
        }

        /// <summary>
        /// Returns the directory of the <see cref="IIssue.AffectedFileRelativePath"/>.
        /// </summary>
        /// <param name="issue">Issue for which the directory should be returned.</param>
        /// <returns>Directory of the file affected by the issue.</returns>
        public static string FileDirectory(this IIssue issue)
        {
            issue.NotNull(nameof(issue));

            return issue.AffectedFileRelativePath?.GetDirectory().FullPath;
        }

        /// <summary>
        /// Returns the name of the file of the <see cref="IIssue.AffectedFileRelativePath"/>.
        /// </summary>
        /// <param name="issue">Issue for which the file name should be returned.</param>
        /// <returns>Name of the file affected by the issue.</returns>
        public static string FileName(this IIssue issue)
        {
            issue.NotNull(nameof(issue));

            return issue.AffectedFileRelativePath?.GetFilename().ToString();
        }

        /// <summary>
        /// Returns a string with all patterns replaced by the values of <paramref name="issue"/>.
        /// </summary>
        /// <param name="pattern">Pattern whose values should be replaced.
        /// The following patterns are supported:
        /// <list type="table">
        ///     <listheader>
        ///         <term>Pattern</term>
        ///         <description>Description</description>
        ///     </listheader>
        ///     <item>
        ///         <term>{ProviderType}</term>
        ///         <description>The value of <see cref="IIssue.ProviderType"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>{ProviderName}</term>
        ///         <description>The value of <see cref="IIssue.ProviderName"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>{Priority}</term>
        ///         <description>The value of <see cref="IIssue.Priority"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>{PriorityName}</term>
        ///         <description>The value of <see cref="IIssue.PriorityName"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>{ProjectPath}</term>
        ///         <description>The value of <see cref="ProjectPath(IIssue)"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>{ProjectDirectory}</term>
        ///         <description>The value of <see cref="ProjectDirectory(IIssue)"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>{ProjectName}</term>
        ///         <description>The value of <see cref="IIssue.ProjectName"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>{FilePath}</term>
        ///         <description>The value of <see cref="FilePath(IIssue)"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>{FileDirectory}</term>
        ///         <description>The value of <see cref="FileDirectory(IIssue)"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>{FileName}</term>
        ///         <description>The value of <see cref="FileName(IIssue)"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>{Line}</term>
        ///         <description>The value of <see cref="IIssue.Line"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>{Rule}</term>
        ///         <description>The value of <see cref="IIssue.Rule"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>{RuleUrl}</term>
        ///         <description>The value of <see cref="IIssue.RuleUrl"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term>{Message}</term>
        ///         <description>The value of <see cref="IIssue.Message"/>.</description>
        ///     </item>
        /// </list>
        /// </param>
        /// <param name="issue">Issue whose values should be used to replace the patterns.</param>
        /// <returns>Value with all patterns replaced.</returns>
        public static string ReplaceIssuePattern(this string pattern, IIssue issue)
        {
            pattern.NotNull(nameof(pattern));
            issue.NotNull(nameof(issue));

            return
                pattern
                    .Replace("{ProviderType}", issue.ProviderType)
                    .Replace("{ProviderName}", issue.ProviderName)
                    .Replace("{Priority}", issue.Priority?.ToString())
                    .Replace("{PriorityName}", issue.PriorityName)
                    .Replace("{ProjectPath}", issue.ProjectPath())
                    .Replace("{ProjectDirectory}", issue.ProjectDirectory())
                    .Replace("{ProjectName}", issue.ProjectName)
                    .Replace("{FilePath}", issue.FilePath())
                    .Replace("{FileDirectory}", issue.FileDirectory())
                    .Replace("{FileName}", issue.FileName())
                    .Replace("{Line}", issue.Line?.ToString())
                    .Replace("{Rule}", issue.Rule)
                    .Replace("{RuleUrl}", issue.RuleUrl?.ToString())
                    .Replace("{Message}", issue.Message);
        }
    }
}