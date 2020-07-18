namespace Cake.Issues
{
    using System;

    /// <summary>
    /// Extensions for <see cref="FileLinkSettings"/>.
    /// </summary>
    public static class FileLinkSettingsExtensions
    {
        /// <summary>
        /// Returns the URL to the file on the source code hosting system
        /// defined by <paramref name="settings"/> for the issue <paramref name="issue"/>.
        /// </summary>
        /// <param name="settings">Settings describing the source code hosting.</param>
        /// <param name="issue">Issue for which the link should be returned.</param>
        /// <returns>URL to the file on the source code hosting system.</returns>
        public static Uri GetFileLink(this FileLinkSettings settings, IIssue issue)
        {
            settings.NotNull(nameof(settings));
            issue.NotNull(nameof(issue));

            return new Uri(settings.FileLinkPattern.ReplaceIssuePattern(issue));
        }
    }
}
