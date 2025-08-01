namespace Cake.Issues.CodeClimate;

using Newtonsoft.Json;

/// <summary>
/// Data contract for CodeClimate format issues.
/// </summary>
internal class CodeClimateIssue
{
    /// <summary>
    /// Gets or sets the issue type. Should always be "issue".
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; } = null!;

    /// <summary>
    /// Gets or sets the check name.
    /// </summary>
    [JsonProperty("check_name")]
    public string CheckName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the issue description.
    /// </summary>
    [JsonProperty("description")]
    public string Description { get; set; } = null!;

    /// <summary>
    /// Gets or sets the markdown content explaining the issue.
    /// </summary>
    [JsonProperty("content")]
    public string Content { get; set; } = null!;

    /// <summary>
    /// Gets or sets the issue categories.
    /// </summary>
    [JsonProperty("categories")]
    public string[] Categories { get; set; } = null!;

    /// <summary>
    /// Gets or sets the issue location.
    /// </summary>
    [JsonProperty("location")]
    public CodeClimateLocation Location { get; set; } = null!;

    /// <summary>
    /// Gets or sets the remediation points.
    /// </summary>
    [JsonProperty("remediation_points")]
    public int? RemediationPoints { get; set; }

    /// <summary>
    /// Gets or sets the severity.
    /// </summary>
    [JsonProperty("severity")]
    public string Severity { get; set; } = null!;

    /// <summary>
    /// Gets or sets the fingerprint.
    /// </summary>
    [JsonProperty("fingerprint")]
    public string Fingerprint { get; set; } = null!;
}

/// <summary>
/// Data contract for CodeClimate location.
/// </summary>
internal class CodeClimateLocation
{
    /// <summary>
    /// Gets or sets the file path.
    /// </summary>
    [JsonProperty("path")]
    public string Path { get; set; } = null!;

    /// <summary>
    /// Gets or sets the line-based location.
    /// </summary>
    [JsonProperty("lines")]
    public CodeClimateLines Lines { get; set; } = null!;

    /// <summary>
    /// Gets or sets the position-based location.
    /// </summary>
    [JsonProperty("positions")]
    public CodeClimatePositions Positions { get; set; } = null!;
}

/// <summary>
/// Data contract for CodeClimate line-based location.
/// </summary>
internal class CodeClimateLines
{
    /// <summary>
    /// Gets or sets the begin line.
    /// </summary>
    [JsonProperty("begin")]
    public int Begin { get; set; }

    /// <summary>
    /// Gets or sets the end line.
    /// </summary>
    [JsonProperty("end")]
    public int End { get; set; }
}

/// <summary>
/// Data contract for CodeClimate position-based location.
/// </summary>
internal class CodeClimatePositions
{
    /// <summary>
    /// Gets or sets the begin position.
    /// </summary>
    [JsonProperty("begin")]
    public CodeClimatePosition Begin { get; set; } = null!;

    /// <summary>
    /// Gets or sets the end position.
    /// </summary>
    [JsonProperty("end")]
    public CodeClimatePosition End { get; set; } = null!;
}

/// <summary>
/// Data contract for CodeClimate position.
/// </summary>
internal class CodeClimatePosition
{
    /// <summary>
    /// Gets or sets the line number.
    /// </summary>
    [JsonProperty("line")]
    public int Line { get; set; }

    /// <summary>
    /// Gets or sets the column number.
    /// </summary>
    [JsonProperty("column")]
    public int Column { get; set; }
}