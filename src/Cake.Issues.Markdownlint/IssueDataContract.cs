namespace Cake.Issues.Markdownlint
{
    using System.Runtime.Serialization;

#pragma warning disable SA1401 // Fields must be private
#pragma warning disable SA1307 // Accessible fields must begin with upper-case letter
#pragma warning disable SA1402 // File may only contain a single class
#pragma warning disable SA1600 // Elements must be documented
#pragma warning disable SA1649 // File name must match first type name

    [DataContract]
    internal class Issue
    {
        [DataMember]
        public int lineNumber;

        [DataMember]
        public string ruleName;

        [DataMember]
        public string ruleAlias;

        [DataMember]
        public string ruleDescription;

        [DataMember]
        public object errorDetail;

        [DataMember]
        public string errorContext;

        [DataMember]
        public int[] errorRange;
    }
#pragma warning restore SA1401 // Fields must be private
#pragma warning restore SA1307 // Accessible fields must begin with upper-case letter
#pragma warning restore SA1402 // File may only contain a single class
#pragma warning restore SA1600 // Elements must be documented
#pragma warning restore SA1649 // File name must match first type name
}
