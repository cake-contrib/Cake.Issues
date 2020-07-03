﻿namespace Cake.Issues
{
    using Cake.Core.IO;

    /// <summary>
    /// Settings for reading issues.
    /// </summary>
    public class ReadIssuesSettings : RepositorySettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadIssuesSettings"/> class.
        /// </summary>
        /// <param name="repositoryRoot">Root path of the repository.</param>
        public ReadIssuesSettings(DirectoryPath repositoryRoot)
            : base(repositoryRoot)
        {
        }

        /// <summary>
        /// Gets or sets the name of the run.
        /// </summary>
        public string Run { get; set; }
    }
}
