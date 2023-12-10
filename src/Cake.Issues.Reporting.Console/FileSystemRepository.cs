namespace Cake.Issues.Reporting.Console
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Errata;

    /// <summary>
    /// Repository to read the source files from the file system.
    /// </summary>
    /// <param name="settings">Settings for report creation.</param>
    internal sealed class FileSystemRepository(ICreateIssueReportSettings settings) : ISourceRepository
    {
        private readonly ICreateIssueReportSettings settings = settings;
        private readonly Dictionary<string, Source> cache = new (StringComparer.OrdinalIgnoreCase);

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
