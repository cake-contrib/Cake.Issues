namespace Cake.Issues
{
    using Cake.Core.IO;

    /// <summary>
    /// Settings containing a path to a repository.
    /// </summary>
    public class RepositorySettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositorySettings"/> class.
        /// </summary>
        /// <param name="repositoryRoot">Root path of the repository.</param>
        public RepositorySettings(DirectoryPath repositoryRoot)
        {
            repositoryRoot.NotNull(nameof(repositoryRoot));

            this.RepositoryRoot = repositoryRoot;
        }

        /// <summary>
        /// Gets the Root path of the repository.
        /// </summary>
        public DirectoryPath RepositoryRoot { get; }
    }
}
