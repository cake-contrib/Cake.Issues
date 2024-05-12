namespace Cake.Issues.Serialization
{
    using System;

    /// <summary>
    /// Extensions for <see cref="SerializableIssueV5"/>.
    /// </summary>
    internal static class SerializableIssueV5Extensions
    {
        /// <summary>
        /// Converts a <see cref="SerializableIssueV5"/> to an <see cref="Issue"/>.
        /// </summary>
        /// <param name="serializableIssue">Issue which should be converted.</param>
        /// <returns>Converted issue.</returns>
        internal static Issue ToIssue(this SerializableIssueV5 serializableIssue)
        {
            serializableIssue.NotNull();

            Uri ruleUrl = null;
            if (!string.IsNullOrWhiteSpace(serializableIssue.RuleUrl))
            {
                ruleUrl = new Uri(serializableIssue.RuleUrl);
            }

            Uri fileLink = null;
            if (!string.IsNullOrWhiteSpace(serializableIssue.FileLink))
            {
                fileLink = new Uri(serializableIssue.FileLink);
            }

            return new Issue(
                serializableIssue.Identifier,
                serializableIssue.ProjectFileRelativePath,
                serializableIssue.ProjectName,
                serializableIssue.AffectedFileRelativePath,
                serializableIssue.Line,
                serializableIssue.EndLine,
                serializableIssue.Column,
                serializableIssue.EndColumn,
                fileLink,
                serializableIssue.MessageText,
                serializableIssue.MessageHtml,
                serializableIssue.MessageMarkdown,
                serializableIssue.Priority,
                serializableIssue.PriorityName,
                serializableIssue.RuleId,
                serializableIssue.RuleName,
                ruleUrl,
                serializableIssue.Run,
                serializableIssue.ProviderType,
                serializableIssue.ProviderName,
                serializableIssue.AdditionalInformation);
        }
    }
}
