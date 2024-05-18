namespace Cake.Issues.Serialization
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Extensions for <see cref="SerializableIssueV2"/>.
    /// </summary>
    internal static class SerializableIssueV2Extensions
    {
        /// <summary>
        /// Converts a <see cref="SerializableIssueV2"/> to an <see cref="Issue"/>.
        /// </summary>
        /// <param name="serializableIssue">Issue which should be converted.</param>
        /// <returns>Converted issue.</returns>
        internal static Issue ToIssue(this SerializableIssueV2 serializableIssue)
        {
            serializableIssue.NotNull();

            Uri ruleUrl = null;
            if (!string.IsNullOrWhiteSpace(serializableIssue.RuleUrl))
            {
                ruleUrl = new Uri(serializableIssue.RuleUrl);
            }

            return new Issue(
                serializableIssue.MessageText,
                serializableIssue.ProjectFileRelativePath,
                serializableIssue.ProjectName,
                serializableIssue.AffectedFileRelativePath,
                serializableIssue.Line,
                null,
                null,
                null,
                null,
                serializableIssue.MessageText,
                serializableIssue.MessageHtml,
                serializableIssue.MessageMarkdown,
                serializableIssue.Priority,
                serializableIssue.PriorityName,
                serializableIssue.Rule,
                null,
                ruleUrl,
                null,
                serializableIssue.ProviderType,
                serializableIssue.ProviderName,
                new Dictionary<string, string>());
        }
    }
}
