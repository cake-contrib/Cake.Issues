namespace Cake.Issues.Reporting.Sarif;

using System;

/// <summary>
/// Settings for <see cref="SarifIssueReportFormatAliases"/>.
/// </summary>
public class SarifIssueReportFormatSettings
{
    /// <summary>
    /// Gets or sets a Guid shared by all runs of the same type.
    /// Will be written to the <c>automationDetails.correlationGuid</c> property.
    /// </summary>
    /// <remarks>
    /// Consider an engineering system that allows engineers to define “build definitions”, and that assigns a GUID
    /// to each build definition.
    /// In such a system, the build definition’s GUID could serve as <see cref="CorrelationGuid"/>.
    /// It would be the same for all runs produced by the same build definition, and different between any two runs
    /// produced by different build definitions.
    /// </remarks>
    public Guid CorrelationGuid { get; set; }
}
