namespace Cake.Issues.Serialization
{
    using System;

    /// <summary>
    /// Extensions for <see cref="SerializableIssue"/>.
    /// </summary>
    internal static class SerializableIssueExtensions
    {
        /// <summary>
        /// Converts a <see cref="SerializableIssue"/> to an <see cref="Issue"/>.
        /// </summary>
        /// <param name="serializableIssue">Issue which should be converted.</param>
        /// <returns>Converted issue.</returns>
        internal static Issue ToIssue(this SerializableIssue serializableIssue)
        {
            serializableIssue.NotNull(nameof(serializableIssue));

            Uri ruleUrl = null;
            if (!string.IsNullOrWhiteSpace(serializableIssue.RuleUrl))
            {
                ruleUrl = new Uri(serializableIssue.RuleUrl);
            }

            return new Issue(
                serializableIssue.ProjectFileRelativePath,
                serializableIssue.ProjectName,
                serializableIssue.AffectedFileRelativePath,
                serializableIssue.Line,
                serializableIssue.Message,
                serializableIssue.Priority,
                serializableIssue.PriorityName,
                serializableIssue.Rule,
                ruleUrl,
                serializableIssue.ProviderType,
                serializableIssue.ProviderName);
        }
    }
}
