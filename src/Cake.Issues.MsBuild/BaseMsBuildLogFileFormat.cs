namespace Cake.Issues.MsBuild
{
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Base class for all MSBuild log file format.
    /// </summary>
    public abstract class BaseMsBuildLogFileFormat : BaseLogFileFormat<MsBuildIssuesProvider, MsBuildIssuesSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseMsBuildLogFileFormat"/> class.
        /// </summary>
        /// <param name="log">The Cake log instance.</param>
        protected BaseMsBuildLogFileFormat(ICakeLog log)
            : base(log)
        {
        }

        /// <summary>
        /// Validates a file path.
        /// </summary>
        /// <param name="filePath">Full file path.</param>
        /// <param name="repositorySettings">Repository settings.</param>
        /// <returns>Tuple containing a value if validation was successful, and file path relative to repository root.</returns>
        protected (bool Valid, string FilePath) ValidateFilePath(string filePath, IRepositorySettings repositorySettings)
        {
            filePath.NotNullOrWhiteSpace(nameof(filePath));
            repositorySettings.NotNull(nameof(repositorySettings));

            // Ignore files from outside the repository.
            if (!this.CheckIfFileIsInRepository(filePath, repositorySettings))
            {
                return (false, string.Empty);
            }

            // Make path relative to repository root.
            filePath = this.MakeFilePathRelativeToRepositoryRoot(filePath, repositorySettings);

            // Remove leading directory separator.
            filePath = this.RemoveLeadingDirectorySeparator(filePath);

            return (true, filePath);
        }

        /// <summary>
        /// Checks if a file is part of the repository.
        /// </summary>
        /// <param name="filePath">Full file path.</param>
        /// <param name="repositorySettings">Repository settings.</param>
        /// <returns>True if file is in repository, false otherwise.</returns>
        protected bool CheckIfFileIsInRepository(string filePath, IRepositorySettings repositorySettings)
        {
            filePath.NotNullOrWhiteSpace(nameof(filePath));
            repositorySettings.NotNull(nameof(repositorySettings));

            if (!filePath.IsSubpathOf(repositorySettings.RepositoryRoot.FullPath))
            {
                this.Log.Warning(
                    "Ignored issue for file '{0}' since it is outside the repository folder at {1}.",
                    filePath,
                    repositorySettings.RepositoryRoot);

                return false;
            }

            return true;
        }

        /// <summary>
        /// Make path relative to repository root.
        /// </summary>
        /// <param name="filePath">Full file path.</param>
        /// <param name="repositorySettings">Repository settings.</param>
        /// <returns>File path relative to the repository root.</returns>
        protected string MakeFilePathRelativeToRepositoryRoot(string filePath, IRepositorySettings repositorySettings)
        {
            filePath.NotNullOrWhiteSpace(nameof(filePath));
            repositorySettings.NotNull(nameof(repositorySettings));

            return filePath.Substring(repositorySettings.RepositoryRoot.FullPath.Length);
        }

        /// <summary>
        /// Remove the leading directory separator from a file path.
        /// </summary>
        /// <param name="filePath">Full file path.</param>
        /// <returns>File path without leading directory separator.</returns>
        protected string RemoveLeadingDirectorySeparator(string filePath)
        {
            filePath.NotNullOrWhiteSpace(nameof(filePath));

            if (filePath.StartsWith(System.IO.Path.DirectorySeparatorChar.ToString()))
            {
                return filePath.Substring(1);
            }

            return filePath;
        }
    }
}
