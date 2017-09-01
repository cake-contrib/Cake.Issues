namespace Cake.Issues.MsBuild
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Xml.Linq;
    using Core.Diagnostics;
    using IssueProvider;

    /// <summary>
    /// MsBuild log format as written by the <c>XmlFileLogger</c> class from MSBuild Extension Pack.
    /// </summary>
    internal class XmlFileLoggerFormat : LogFileFormat
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlFileLoggerFormat"/> class.
        /// </summary>
        /// <param name="log">The Cake log instance.</param>
        public XmlFileLoggerFormat(ICakeLog log)
            : base(log)
        {
        }

        /// <inheritdoc/>
        public override IEnumerable<IIssue> ReadIssues(
            RepositorySettings repositorySettings,
            MsBuildIssuesSettings msBuildIssuesSettings)
        {
            repositorySettings.NotNull(nameof(repositorySettings));
            msBuildIssuesSettings.NotNull(nameof(msBuildIssuesSettings));

            var result = new List<IIssue>();

            var logDocument = XDocument.Parse(msBuildIssuesSettings.LogFileContent);

            // Loop through all warning tags.
            foreach (var warning in logDocument.Descendants("warning"))
            {
                // Read affected file from the warning.
                if (!this.TryGetFile(warning, repositorySettings, out string fileName))
                {
                    continue;
                }

                // Read affected line from the warning.
                if (!TryGetLine(warning, out var line))
                {
                    continue;
                }

                // Read rule code from the warning.
                if (!TryGetRule(warning, out string rule))
                {
                    continue;
                }

                result.Add(new Issue<MsBuildIssuesProvider>(
                    fileName,
                    line,
                    warning.Value,
                    0,
                    rule,
                    MsBuildRuleUrlResolver.Instance.ResolveRuleUrl(rule)));
            }

            return result;
        }

        /// <summary>
        /// Reads the affected line from a warning logged in a MsBuild log.
        /// </summary>
        /// <param name="warning">Warning element from MsBuild log.</param>
        /// <param name="line">Returns line.</param>
        /// <returns>True if the line could be parsed.</returns>
        private static bool TryGetLine(XElement warning, out int? line)
        {
            line = null;

            var lineAttr = warning.Attribute("line");

            var lineValue = lineAttr?.Value;
            if (string.IsNullOrWhiteSpace(lineValue))
            {
                return false;
            }

            line = int.Parse(lineValue, CultureInfo.InvariantCulture);

            // Convert negative lines numbers to null
            if (line < 0)
            {
                line = null;
            }

            return true;
        }

        /// <summary>
        /// Reads the rule code from a warning logged in a MsBuild log.
        /// </summary>
        /// <param name="warning">Warning element from MsBuild log.</param>
        /// <param name="rule">Returns the code of the rule.</param>
        /// <returns>True if the rule code could be parsed.</returns>
        private static bool TryGetRule(XElement warning, out string rule)
        {
            rule = string.Empty;

            var codeAttr = warning.Attribute("code");
            if (codeAttr == null)
            {
                return false;
            }

            rule = codeAttr.Value;
            return !string.IsNullOrWhiteSpace(rule);
        }

        /// <summary>
        /// Reads the affected file path from a warning logged in a MsBuild log.
        /// </summary>
        /// <param name="warning">Warning element from MsBuild log.</param>
        /// <param name="repositorySettings">Repository settings to use.</param>
        /// <param name="fileName">Returns the full path to the affected file.</param>
        /// <returns>True if the file path could be parsed.</returns>
        private bool TryGetFile(
            XElement warning,
            RepositorySettings repositorySettings,
            out string fileName)
        {
            fileName = string.Empty;

            var fileAttr = warning.Attribute("file");
            if (fileAttr == null)
            {
                return true;
            }

            fileName = fileAttr.Value;
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return true;
            }

            // If not absolute path, combine with file path from compile task.
            if (!fileName.IsFullPath())
            {
                var parentFileAttr = warning.Parent?.Attribute("file");
                if (parentFileAttr != null)
                {
                    var compileTaskDirectory = Path.GetDirectoryName(parentFileAttr.Value);
                    fileName = Path.Combine(compileTaskDirectory, fileName);
                }
            }

            // Ignore files from outside the repository.
            if (!fileName.IsSubpathOf(repositorySettings.RepositoryRoot.FullPath))
            {
                this.Log.Warning(
                    "Ignored issue for file '{0}' since it is outside the repository folder at {1}.",
                    fileName,
                    repositorySettings.RepositoryRoot);

                return false;
            }

            // Make path relative to repository root.
            fileName = fileName.Substring(repositorySettings.RepositoryRoot.FullPath.Length);

            // Remove leading directory separator.
            if (fileName.StartsWith(Path.DirectorySeparatorChar.ToString()))
            {
                fileName = fileName.Substring(1);
            }

            return true;
        }
    }
}
