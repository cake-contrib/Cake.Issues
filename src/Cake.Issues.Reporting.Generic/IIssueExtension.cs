namespace Cake.Issues.Reporting.Generic
{
    using Cake.Core.IO;

    /// <summary>
    /// Extension for <see cref="IIssue"/>.
    /// </summary>
    public static class IIssueExtension
    {
        /// <summary>
        /// Returns the path of the <see cref="IIssue.AffectedFileRelativePath"/>.
        /// </summary>
        /// <param name="issue">Issue for which the path should be returned.</param>
        /// <returns>Path of the file affected by the issue.</returns>
        public static string FilePath(this IIssue issue)
        {
            return issue.AffectedFileRelativePath?.GetDirectory().FullPath;
        }

        /// <summary>
        /// Returns the name of the file of the <see cref="IIssue.AffectedFileRelativePath"/>.
        /// </summary>
        /// <param name="issue">Issue for which the file name should be returned.</param>
        /// <returns>Name of the file affected by the issue.</returns>
        public static string FileName(this IIssue issue)
        {
            return issue.AffectedFileRelativePath?.GetFilename().ToString();
        }
    }
}
