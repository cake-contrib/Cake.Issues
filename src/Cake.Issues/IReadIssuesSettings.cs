namespace Cake.Issues
{
    /// <summary>
    /// Interface for settings for reading issues.
    /// </summary>
    public interface IReadIssuesSettings : IRepositorySettings
    {
        /// <summary>
        /// Gets or sets the name of the run.
        /// </summary>
        string Run { get; set; }

        /// <summary>
        /// Gets or sets settings which can be used for resolving links to source files.
        /// </summary>
        FileLinkSettings FileLinkSettings { get; set; }
    }
}
