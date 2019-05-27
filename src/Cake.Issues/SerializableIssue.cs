namespace Cake.Issues
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Class for serializing and deserializing an <see cref="IIssue"/> instance.
    /// </summary>
    [DataContract]
    internal class SerializableIssue
    {
        /// <inheritdoc cref="IIssue.ProjectFileRelativePath" />
        [DataMember]
        public string ProjectFileRelativePath { get; set; }

        /// <inheritdoc cref="IIssue.ProjectName" />
        [DataMember]
        public string ProjectName { get; set; }

        /// <inheritdoc cref="IIssue.AffectedFileRelativePath" />
        [DataMember]
        public string AffectedFileRelativePath { get; set; }

        /// <inheritdoc cref="IIssue.Line" />
        [DataMember]
        public int? Line { get; set; }

        /// <inheritdoc cref="IIssue.Message" />
        [DataMember]
        public string Message { get; set; }

        /// <inheritdoc cref="IIssue.Priority" />
        [DataMember]
        public int? Priority { get; set; }

        /// <inheritdoc cref="IIssue.PriorityName" />
        [DataMember]
        public string PriorityName { get; set; }

        /// <inheritdoc cref="IIssue.Rule" />
        [DataMember]
        public string Rule { get; set; }

        /// <inheritdoc cref="IIssue.RuleUrl" />
        [DataMember]
        public string RuleUrl { get; set; }

        /// <inheritdoc cref="IIssue.ProviderType" />
        [DataMember]
        public string ProviderType { get; set; }

        /// <inheritdoc cref="IIssue.ProviderName" />
        [DataMember]
        public string ProviderName { get; set; }
    }
}
