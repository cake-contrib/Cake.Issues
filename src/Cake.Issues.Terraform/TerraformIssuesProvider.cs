namespace Cake.Issues.Terraform
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using Cake.Core.Diagnostics;
    using Cake.Core.IO;

    /// <summary>
    /// Provider for warnings reported by Terraform.
    /// </summary>
    /// <param name="log">The Cake log context.</param>
    /// <param name="issueProviderSettings">Settings for the issue provider.</param>
    internal class TerraformIssuesProvider(ICakeLog log, TerraformIssuesSettings issueProviderSettings)
        : BaseConfigurableIssueProvider<TerraformIssuesSettings>(log, issueProviderSettings)
    {
        /// <summary>
        /// Gets the name of the Terraform issue provider.
        /// This name can be used to identify issues based on the <see cref="IIssue.ProviderType"/> property.
        /// </summary>
        public static string ProviderTypeName => typeof(TerraformIssuesProvider).FullName;

        /// <inheritdoc />
        public override string ProviderName => "Terraform";

        /// <inheritdoc />
        protected override IEnumerable<IIssue> InternalReadIssues()
        {
            // Determine path of the doc root.
            var terraformRootPath = this.IssueProviderSettings.TerraformRootPath;
            if (terraformRootPath.IsRelative)
            {
                terraformRootPath = terraformRootPath.MakeAbsolute(this.Settings.RepositoryRoot);
            }

            ValidateFile validateFile;

            var logFileContent = this.IssueProviderSettings.LogFileContent.ToStringUsingEncoding(true);

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(logFileContent)))
            {
                var jsonSerializer = new DataContractJsonSerializer(typeof(ValidateFile));
                validateFile = jsonSerializer.ReadObject(ms) as ValidateFile;
            }

            return validateFile != null
                ? (from diagnostic in validateFile.diagnostics
                   select
                        IssueBuilder
                            .NewIssue(GetMessage(diagnostic.summary, diagnostic.detail), this)
                            .InFile(
                                this.TryGetFile(diagnostic.range?.filename, terraformRootPath),
                                diagnostic.range?.start?.line,
                                diagnostic.range?.end?.line,
                                diagnostic.range?.start?.column,
                                diagnostic.range?.end?.column)
                            .OfRule(GetRule(diagnostic.summary, diagnostic.detail))
                            .WithPriority(GetPriority(diagnostic.severity))
                            .Create())
                : throw new InvalidOperationException("Failed to read log file content.");
        }

        /// <summary>
        /// Returns the message for an issue based on summary and detail.
        /// </summary>
        /// <param name="summary">Summary of the diagnostic entry.</param>
        /// <param name="detail">Detail of the diagnostic entry.</param>
        /// <returns>Issue message.</returns>
        [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1515:Single-line comment should be preceded by blank line", Justification = "Not for expression bodies")]
        private static string GetMessage(string summary, string detail) =>
            // If a diagnostic entry only contains a summary we use it for message instead of rule.
            !string.IsNullOrWhiteSpace(detail) ? detail : summary;

        /// <summary>
        /// Returns the rule for an issue based on summary and detail.
        /// </summary>
        /// <param name="summary">Summary of the diagnostic entry.</param>
        /// <param name="detail">Detail of the diagnostic entry.</param>
        /// <returns>Issue message.</returns>
        [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1515:Single-line comment should be preceded by blank line", Justification = "Not for expression bodies")]
        private static string GetRule(string summary, string detail) =>
            // If a diagnostic entry only contains a summary we don't use it for rule as it is already used for message.
            !string.IsNullOrWhiteSpace(detail) ? summary : null;

        /// <summary>
        /// Converts the severity to a priority.
        /// </summary>
        /// <param name="severity">Severity as reported by Terraform.</param>
        /// <returns>Priority.</returns>
        private static IssuePriority GetPriority(string severity) =>
            severity.ToLowerInvariant() switch
            {
                "error" => IssuePriority.Error,
                "warning" => IssuePriority.Warning,
                _ => IssuePriority.Undefined,
            };

        /// <summary>
        /// Reads the affected file path from an issue logged by terraform validate.
        /// </summary>
        /// <param name="fileName">The file name in the current log entry.</param>
        /// <param name="terraformRootPath">Absolute path to the root directory of the Terraform scripts.</param>
        /// <returns>The full path to the affected file.</returns>
        private string TryGetFile(
            string fileName,
            DirectoryPath terraformRootPath)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return null;
            }

            // Add path to repository root
            fileName = terraformRootPath.CombineWithFilePath(fileName).FullPath;

            // Make path relative to repository root.
            return
                fileName
                    .MakeFilePathRelativeToRepositoryRoot(this.Settings);
        }
    }
}