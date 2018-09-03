namespace Cake.Issues.Reporting.Generic
{
    using System;

    /// <summary>
    /// Settings how issues should be linked to files.
    /// </summary>
    public class FileLinkSettings
    {
        /// <summary>
        /// Gets or sets the pattern which should be used to link issues to files.
        /// Fields in the form <c>{FieldName}</c> are replaced with the value of the isse.
        /// All fields of <see cref="ReportColumn"/> supported.
        /// </summary>
        public string FileLinkPattern { get; set; }

        /// <summary>
        /// Returns settings for linking to files hosted in GitHub.
        /// </summary>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// eg. <code>https://github.com/cake-contrib/Cake.Issues.Reporting.Generic</code>.</param>
        /// <param name="branch">Name of the branch.</param>
        /// <param name="rootPath">Root path of the files.
        /// <c>null</c> or <see cref="string.Empty"/> if files are in the root of the repository.</param>
        /// <returns>Settings for linking to files hosted in GitHub.</returns>
        public static FileLinkSettings GitHub(
            Uri repositoryUrl,
            string branch,
            string rootPath)
        {
            repositoryUrl.NotNull(nameof(repositoryUrl));
            branch.NotNullOrWhiteSpace(nameof(branch));

            return new FileLinkSettings()
            {
                FileLinkPattern = repositoryUrl.Append("blob", branch, rootPath, "{FilePath}#L{Line}").ToString()
            };
        }

        /// <summary>
        /// Returns settings for linking to files hosted in Visual Studio Team Service or Team Foundation Server.
        /// </summary>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// eg. <code>http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository</code>.</param>
        /// <param name="branch">Name of the branch.</param>
        /// <param name="rootPath">Root path of the files.
        /// <c>null</c> or <see cref="string.Empty"/> if files are in the root of the repository.</param>
        /// <returns>Settings for linking to files hosted in Visual Studio Team Service or Team Foundation Server.</returns>
        public static FileLinkSettings TeamFoundationServer(
            Uri repositoryUrl,
            string branch,
            string rootPath)
        {
            repositoryUrl.NotNull(nameof(repositoryUrl));
            branch.NotNullOrWhiteSpace(nameof(branch));

            if (!string.IsNullOrWhiteSpace(rootPath))
            {
                rootPath = rootPath.Trim('/') + "/";
            }

            return new FileLinkSettings()
            {
                FileLinkPattern =
                    repositoryUrl.ToString().TrimEnd('/') + "?path=" + rootPath + "{FilePath}&version=GB" + branch + "&line={Line}"
            };
        }
    }
}
