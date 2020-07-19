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
    }
}
