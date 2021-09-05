namespace Cake.Issues.Reporting.Console
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Errata;

    /// <summary>
    /// Repository to read the source files from the file system.
    /// </summary>
    internal sealed class FileSystemRepository : ISourceRepository
    {
        private readonly ICreateIssueReportSettings settings;
        private readonly Dictionary<string, Source> cache = new (StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemRepository"/> class.
        /// </summary>
        /// <param name="settings">Settings for report creation.</param>
        public FileSystemRepository(ICreateIssueReportSettings settings)
        {
            this.settings = settings;
        }

        /// <inheritdoc />
        public bool TryGet(string id, out Source source)
        {
            if (!this.cache.TryGetValue(id, out source))
            {
                var filePath = this.settings.RepositoryRoot.Combine(id).FullPath;

                if (!File.Exists(filePath))
                {
                    return false;
                }

                source = new Source(id, File.ReadAllText(filePath));
                this.cache[id] = source;
            }

            return true;
        }
    }
}
