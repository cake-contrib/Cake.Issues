namespace Cake.Issues.PullRequests.AppVeyor
{
    /// <summary>
    /// Settings for <see cref="AppVeyorBuildsAlias"/>.
    /// </summary>
    public class AppVeyorBuildSettings
    {
        private string messagePattern = "Project: {ProjectName}, File: {FilePath}, Line: {Line}";
        private string detailsPattern = "{Rule}: {Message}";

        /// <summary>
        /// Initializes a new instance of the <see cref="AppVeyorBuildSettings"/> class.
        /// </summary>
        public AppVeyorBuildSettings()
        {
        }

        /// <summary>
        /// Gets or sets the pattern of the message to display.
        /// See <see cref="Cake.Issues.IIssueExtensions.ReplaceIssuePattern(string, IIssue)"/> for possible patterns.
        /// The default value is: "Project: {ProjectName}, File: {FilePath}, Line: {Line}".
        /// </summary>
        public string MessagePattern
        {
            get => this.messagePattern;
            set
            {
                value.NotNull(nameof(value));
                this.messagePattern = value;
            }
        }

        /// <summary>
        /// Gets or sets the pattern of the message details to display.
        /// See <see cref="Cake.Issues.IIssueExtensions.ReplaceIssuePattern(string, IIssue)"/> for possible patterns.
        /// The default value is: "{Rule}: {Message}".
        /// </summary>
        public string DetailsPattern
        {
            get => this.detailsPattern;
            set
            {
                value.NotNull(nameof(value));
                this.detailsPattern = value;
            }
        }
    }
}