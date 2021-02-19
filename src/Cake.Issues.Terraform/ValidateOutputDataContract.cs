namespace Cake.Issues.Terraform
{
    using System.Runtime.Serialization;

#pragma warning disable SA1310 // Field names must not contain underscore
#pragma warning disable SA1401 // Fields must be private
#pragma warning disable SA1307 // Accessible fields must begin with upper-case letter
#pragma warning disable SA1402 // File may only contain a single class
#pragma warning disable SA1600 // Elements must be documented
#pragma warning disable SA1649 // File name must match first type name

    [DataContract]
    internal class ValidateFile
    {
        [DataMember]
        public bool valid;

        [DataMember]
        public int error_count;

        [DataMember]
        public int warning_count;

        [DataMember]
        public Diagnostic[] diagnostics;
    }

    [DataContract]
    internal class Diagnostic
    {
        [DataMember]
        public string severity;

        [DataMember]
        public string summary;

        [DataMember]
        public string detail;

        [DataMember]
        public Range range;
    }

    [DataContract]
    internal class Range
    {
        [DataMember]
        public string filename;

        [DataMember]
        public Location start;

        [DataMember]
        public Location end;
    }

    [DataContract]
    internal class Location
    {
        [DataMember]
        public int line;

        [DataMember]
        public int column;
    }

#pragma warning restore SA1310 // Field names must not contain underscore
#pragma warning restore SA1401 // Fields must be private
#pragma warning restore SA1307 // Accessible fields must begin with upper-case letter
#pragma warning restore SA1402 // File may only contain a single class
#pragma warning restore SA1600 // Elements must be documented
#pragma warning restore SA1649 // File name must match first type name
}