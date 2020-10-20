namespace Cake.Issues.MsBuild.LogFileFormat
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Xml.Linq;
    using Cake.Core.Diagnostics;

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
            IRepositorySettings repositorySettings,
            MsBuildIssuesSettings issueProviderSettings)
        {
#pragma warning disable SA1123 // Do not place regions within elements
            #region DupFinder Exclusion
#pragma warning restore SA1123 // Do not place regions within elements

            issueProvider.NotNull(nameof(issueProvider));
            repositorySettings.NotNull(nameof(repositorySettings));
            issueProviderSettings.NotNull(nameof(issueProviderSettings));

            #endregion

            var result = new List<IIssue>();

            // Read log file.
            var raw = issueProviderSettings.LogFileContent.ToStringUsingEncoding(true);
            var filtered = string.Concat(raw.Where(c => !char.IsControl(c)));
            var logDocument = XDocument.Parse(filtered);

            // Loop through all warning and error tags.
            var elements = new List<XElement>(logDocument.Descendants("warning"));
            elements.AddRange(logDocument.Descendants("error"));

            foreach (var element in elements)
            {
                // Ignore warnings or errors without a message.
                if (string.IsNullOrWhiteSpace(element.Value))
                {
                    continue;
                }

                // Read affected project from the warning or error.
                if (!this.TryGetProject(element, repositorySettings, out string projectFileRelativePath))
                {
                    continue;
                }

                // Read affected file from the warning or error.
                if (!this.TryGetFile(element, repositorySettings, out string fileName))
                {
                    continue;
                }

                // Read affected line from the warning or error.
                if (!TryGetLine(element, out var line))
                {
                    continue;
                }

                // Read affected column from the warning or error.
                if (!TryGetColumn(element, out var column))
                {
                    continue;
                }

                // Read rule code from the warning or error.
                if (!TryGetRule(element, out string rule))
                {
                    continue;
                }

                // Determine rule URL.
                Uri ruleUrl = null;
                if (!string.IsNullOrWhiteSpace(rule))
                {
                    ruleUrl = MsBuildRuleUrlResolver.Instance.ResolveRuleUrl(rule);
                }

                var priority = element.Name.LocalName == "error" ? IssuePriority.Error : IssuePriority.Warning;

                // Build issue.
                result.Add(
                    IssueBuilder
                        .NewIssue(element.Value, issueProvider)
                        .WithPriority(priority)
                        .InProject(projectFileRelativePath, System.IO.Path.GetFileNameWithoutExtension(projectFileRelativePath))
                        .InFile(fileName, line, column)
                        .OfRule(rule, ruleUrl)
                        .Create());
            }

            return result;
        }

        /// <summary>
        /// Reads the affected line from a warning or error logged in a MsBuild log.
        /// </summary>
        /// <param name="element">Warning or error element from MsBuild log.</param>
        /// <param name="line">Returns line.</param>
        /// <returns>True if the line could be parsed.</returns>
        private static bool TryGetLine(XElement element, out int? line)
        {
            line = null;

            var lineAttr = element.Attribute("line");

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
        /// Reads the affected column from a warning or error logged in a MsBuild log.
        /// </summary>
        /// <param name="element">Warning or error element from MsBuild log.</param>
        /// <param name="column">Returns column.</param>
        /// <returns>True if the column could be parsed.</returns>
        private static bool TryGetColumn(XElement element, out int? column)
        {
            column = null;

            var columnAttr = element.Attribute("column");

            var columnValue = columnAttr?.Value;
            if (string.IsNullOrWhiteSpace(columnValue))
            {
                return false;
            }

            column = int.Parse(columnValue, CultureInfo.InvariantCulture);

            // Convert negative column numbers or column number 0 to null
            if (column <= 0)
            {
                column = null;
            }

            return true;
        }

        /// <summary>
        /// Reads the rule code from a warning or error logged in a MsBuild log.
        /// </summary>
        /// <param name="error">Warning or error element from MsBuild log.</param>
        /// <param name="rule">Returns the code of the rule.</param>
        /// <returns>True if the rule code could be parsed.</returns>
        private static bool TryGetRule(XElement error, out string rule)
        {
            var codeAttr = error.Attribute("code");
            if (codeAttr == null)
            {
                rule = null;
                return true;
            }

            rule = codeAttr.Value;
            return !string.IsNullOrWhiteSpace(rule);
        }

        /// <summary>
        /// Determines the project for a warning or error logged in a MsBuild log.
        /// </summary>
        /// <param name="element">Warning or error element from MsBuild log.</param>
        /// <param name="repositorySettings">Repository settings to use.</param>
        /// <param name="project">Returns project.</param>
        /// <returns>True if the project could be parsed.</returns>
        private bool TryGetProject(
            XElement element,
            IRepositorySettings repositorySettings,
            out string project)
        {
            project = string.Empty;

            var projectNode = element.Ancestors("project").FirstOrDefault();
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

            // Validate project path and make relative to repository root.
            bool result;
            (result, project) = this.ValidateFilePath(project, repositorySettings);
            return result;
        }

        /// <summary>
        /// Reads the affected file path from a warning or error logged in a MsBuild log.
        /// </summary>
        /// <param name="element">Warning or error element from MsBuild log.</param>
        /// <param name="repositorySettings">Repository settings to use.</param>
        /// <param name="fileName">Returns the full path to the affected file.</param>
        /// <returns>True if the file path could be parsed.</returns>
        private bool TryGetFile(
            XElement element,
            IRepositorySettings repositorySettings,
            out string fileName)
        {
            fileName = string.Empty;

            var fileAttr = element.Attribute("file");
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
                var parentFileAttr = element.Parent?.Attribute("file");
                if (parentFileAttr != null)
                {
                    var compileTaskDirectory = System.IO.Path.GetDirectoryName(parentFileAttr.Value);
                    fileName = System.IO.Path.Combine(compileTaskDirectory, fileName);
                }
                else
                {
                    fileName = System.IO.Path.Combine(repositorySettings.RepositoryRoot.FullPath, fileName);
                }
            }

            // Validate file path and make relative to repository root.
            bool result;
            (result, fileName) = this.ValidateFilePath(fileName, repositorySettings);
            return result;
        }
    }
}
