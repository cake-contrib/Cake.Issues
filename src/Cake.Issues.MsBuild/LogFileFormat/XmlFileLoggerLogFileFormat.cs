namespace Cake.Issues.MsBuild.LogFileFormat
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Xml.Linq;
    using Core.Diagnostics;

    /// <summary>
    /// MsBuild log format as written by the <c>XmlFileLogger</c> class from MSBuild Extension Pack.
    /// </summary>
    internal class XmlFileLoggerLogFileFormat : BaseMsBuildLogFileFormat
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlFileLoggerLogFileFormat"/> class.
        /// </summary>
        /// <param name="log">The Cake log instance.</param>
        public XmlFileLoggerLogFileFormat(ICakeLog log)
            : base(log)
        {
        }

        /// <inheritdoc/>
        public override IEnumerable<IIssue> ReadIssues(
            MsBuildIssuesProvider issueProvider,
            RepositorySettings repositorySettings,
            MsBuildIssuesSettings issueProviderSettings)
        {
            issueProvider.NotNull(nameof(issueProvider));
            repositorySettings.NotNull(nameof(repositorySettings));
            issueProviderSettings.NotNull(nameof(issueProviderSettings));

            var result = new List<IIssue>();

            // Read log file.
            var logDocument = XDocument.Parse(issueProviderSettings.LogFileContent.ToStringUsingEncoding(true));

            // Loop through all warning tags.
            foreach (var warning in logDocument.Descendants("warning"))
            {
                // Read affected project from the warning.
                if (!TryGetProject(warning, repositorySettings, out string projectFileRelativePath))
                {
                    continue;
                }

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

                // Determine rule URL.
                Uri ruleUrl = null;
                if (!string.IsNullOrWhiteSpace(rule))
                {
                    ruleUrl = MsBuildRuleUrlResolver.Instance.ResolveRuleUrl(rule);
                }

                // Build issue.
                result.Add(
                    IssueBuilder
                        .NewIssue(warning.Value, issueProvider)
                        .WithPriority(IssuePriority.Warning)
                        .InProject(projectFileRelativePath, System.IO.Path.GetFileNameWithoutExtension(projectFileRelativePath))
                        .InFile(fileName, line)
                        .OfRule(rule, ruleUrl)
                        .Create());
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

            // Convert negative line numbers or line number 0 to null
            if (line <= 0)
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
                rule = null;
                return true;
            }

            rule = codeAttr.Value;
            return !string.IsNullOrWhiteSpace(rule);
        }

        /// <summary>
        /// Determines the project for a warning logged in a MsBuild log.
        /// </summary>
        /// <param name="warning">Warning element from MsBuild log.</param>
        /// <param name="repositorySettings">Repository settings to use.</param>
        /// <param name="project">Returns project.</param>
        /// <returns>True if the project could be parsed.</returns>
        private static bool TryGetProject(
            XElement warning,
            RepositorySettings repositorySettings,
            out string project)
        {
            project = string.Empty;

            var projectNode = warning.Ancestors("project").FirstOrDefault();
            if (projectNode == null)
            {
                return true;
            }

            var projectAttr = projectNode.Attribute("file");
            if (projectAttr == null)
            {
                return true;
            }

            project = projectAttr.Value;
            if (string.IsNullOrWhiteSpace(project))
            {
                return true;
            }

            // Make path relative to repository root.
            project = project.Substring(repositorySettings.RepositoryRoot.FullPath.Length);

            // Remove leading directory separator.
            if (project.StartsWith(System.IO.Path.DirectorySeparatorChar.ToString()))
            {
                project = project.Substring(1);
            }

            return true;
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
                    var compileTaskDirectory = System.IO.Path.GetDirectoryName(parentFileAttr.Value);
                    fileName = System.IO.Path.Combine(compileTaskDirectory, fileName);
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
            if (fileName.StartsWith(System.IO.Path.DirectorySeparatorChar.ToString()))
            {
                fileName = fileName.Substring(1);
            }

            return true;
        }
    }
}
