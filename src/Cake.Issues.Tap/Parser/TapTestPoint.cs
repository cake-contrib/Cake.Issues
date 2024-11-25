namespace Cake.Issues.Tap.Parser;

using System.Collections.Generic;

/// <summary>
/// Represents a result of a test point in a TAP file.
/// </summary>
internal class TapTestPoint
{
    /// <summary>
    /// Gets or sets a value indicating whether the test point passed or failed.
    /// </summary>
    public bool TestStatus { get; set; }

    /// <summary>
    /// Gets or sets the unique ID of the test point.
    /// </summary>
    public int? TestPointID { get; set; }

    /// <summary>
    /// Gets or sets the description of the test point.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the test point has been marked with <c>TODO</c>.
    /// </summary>
    public bool IsTodo { get; set; }

    /// <summary>
    /// Gets or sets the explanation why the test point has been marked with <c>TODO</c>.
    /// </summary>
    public string TodoExplanation { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the test point has been marked as <c>SKIP</c>.
    /// </summary>
    public bool IsSkip { get; set; }

    /// <summary>
    /// Gets or sets the explanation why the test point has been marked as <c>SKIP</c>.
    /// </summary>
    public string SkipExplanation { get; set; }

    /// <summary>
    /// Gets or sets additional YAML diagnostics of the test.
    /// </summary>
    public Dictionary<string, object> Diagnostics { get; set; } = [];
}
