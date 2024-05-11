namespace Cake.Issues.FileLinking
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class containing builder for optional settings for linking to files.
    /// </summary>
    public class FileLinkOptionalSettingsBuilder : FileLinkSettings
    {
        private readonly Func<IIssue, IDictionary<string, string>, Uri> builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileLinkOptionalSettingsBuilder"/> class.
        /// </summary>
        /// <param name="builder">Callback called for building the file link.</param>
        internal FileLinkOptionalSettingsBuilder(Func<IIssue, IDictionary<string, string>, Uri> builder)
            : base(builder)
        {
            builder.NotNull();

            this.builder = builder;
        }

        /// <summary>
        /// Sets the root path for the files.
        /// </summary>
        /// <param name="rootPath">Root path for the files.
        /// <c>null</c> if files are in the root of the repository.</param>
        /// <returns>Object for creating the file link.</returns>
        public FileLinkSettings WithRootPath(string rootPath)
        {
            if (rootPath != null)
            {
                rootPath.NotNullOrWhiteSpace();

                if (!rootPath.IsValidPath())
                {
                    throw new ArgumentException($"Invalid path '{rootPath}'", nameof(rootPath));
                }
            }

            return
                new FileLinkSettings(
                    (issue, values) =>
                    {
                        values.Add("rootPath", rootPath);

                        return this.builder(issue, values);
                    });
        }
    }
}
