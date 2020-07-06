﻿namespace Cake.Issues.Serialization
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Class for serializing and deserializing an <see cref="IIssue"/> instance.
    /// </summary>
    [DataContract]
    internal class SerializableIssueV3
    {
        /// <summary>
        /// Gets the version of the serialization format.
        /// </summary>
        [DataMember]
        public int Version
        {
            get
            {
                return 3;
            }
        }

        /// <inheritdoc cref="IIssue.Identifier" />
        [DataMember]
        public string Identifier { get; set; }

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

        /// <inheritdoc cref="IIssue.EndLine" />
        [DataMember]
        public int? EndLine { get; set; }

        /// <inheritdoc cref="IIssue.Column" />
        [DataMember]
        public int? Column { get; set; }

        /// <inheritdoc cref="IIssue.EndColumn" />
        [DataMember]
        public int? EndColumn { get; set; }

        /// <inheritdoc cref="IIssue.MessageText" />
        [DataMember]
        public string MessageText { get; set; }

        /// <inheritdoc cref="IIssue.MessageMarkdown" />
        [DataMember]
        public string MessageMarkdown { get; set; }

        /// <inheritdoc cref="IIssue.MessageHtml" />
        [DataMember]
        public string MessageHtml { get; set; }

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

        /// <inheritdoc cref="IIssue.Run" />
        [DataMember]
        public string Run { get; set; }
    }
}
