namespace Cake.Issues.EsLint;

using System.Runtime.Serialization;

#pragma warning disable SA1401 // Fields must be private
#pragma warning disable SA1307 // Accessible fields must begin with upper-case letter
#pragma warning disable SA1402 // File may only contain a single class
#pragma warning disable SA1600 // Elements must be documented
#pragma warning disable SA1649 // File name must match first type name

[DataContract]
internal class LogFile
{
    [DataMember]
    public string filePath;

    [DataMember]
    public Message[] messages;
}

[DataContract]
internal class Message
{
    [DataMember]
    public string ruleId;

    [DataMember]
    public int severity;

    [DataMember]
    public string message;

    [DataMember]
    public int line;

    [DataMember]
    public int column;

    [DataMember]
    public string nodeType;

    [DataMember]
    public string source;
}
#pragma warning restore SA1401 // Fields must be private
#pragma warning restore SA1307 // Accessible fields must begin with upper-case letter
#pragma warning restore SA1402 // File may only contain a single class
#pragma warning restore SA1600 // Elements must be documented
#pragma warning restore SA1649 // File name must match first type name

