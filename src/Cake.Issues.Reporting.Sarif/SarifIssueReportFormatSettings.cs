namespace Cake.Issues.Reporting.Sarif;

using System;
using System.Collections.Generic;

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

    /// <summary>
    /// Gets or sets a Guid whose value is a GUID-valued string that provides a unique, stable identifier for the run.
    /// Will be written to the <c>automationDetails.guid</c> property.
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Gets or sets a Guid whose value is a GUID-valued string equal to <see cref="Guid"/> of a previous run.
    /// Will be written to the <c>run.baselineGuid</c> property.
    /// </summary>
    /// <remarks>
    /// Setting <see cref="BaselineGuid"/> and <see cref="ExistingIssues"/> is required to generate the
    /// baseline state of the issues.
    /// </remarks>
    public Guid BaselineGuid { get; set; }

    /// <summary>
    /// Gets the existing issues from a previous report.
    /// This can be used to generate a Sarif report with baseline information.
    /// </summary>
    /// <remarks>
    /// Setting <see cref="ExistingIssues"/> and <see cref="BaselineGuid"/> is required to generate the
    /// baseline state of the issues.
    /// </remarks>
    public List<IIssue> ExistingIssues { get; } = [];
}
