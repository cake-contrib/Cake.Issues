namespace Cake.Issues.DocFx;

using System.Runtime.Serialization;

#pragma warning disable SA1310 // Field names must not contain underscore
#pragma warning disable SA1401 // Fields must be private
#pragma warning disable SA1307 // Accessible fields must begin with upper-case letter
#pragma warning disable SA1600 // Elements must be documented

[DataContract]
internal class LogEntryDataContract
{
    [DataMember]
    public string message_severity;

    [DataMember]
    public string severity;

    [DataMember]
    public string file;

    [DataMember]
    public int? line;

    [DataMember]
    public string message;

    [DataMember]
    public string source;

    [DataMember]
    public string code;

    public string Severity => this.severity ?? this.message_severity;

    public string Rule =>
        this.severity != null
            ? this.code ?? this.source
            : this.source ?? this.code;
}

#pragma warning restore SA1310 // Field names must not contain underscore
#pragma warning restore SA1401 // Fields must be private
#pragma warning restore SA1307 // Accessible fields must begin with upper-case letter
#pragma warning restore SA1600 // Elements must be documented
