namespace Cake.Issues;

/// <summary>
/// Specifies the preferred coordinate format for issue locations.
/// </summary>
public enum LocationCoordinates
{
    /// <summary>
    /// Use line and column coordinates.
    /// </summary>
    LineColumn,

    /// <summary>
    /// Use character offset coordinates.
    /// </summary>
    Offset,

    /// <summary>
    /// Use both line/column and offset coordinates when available.
    /// </summary>
    Both,
}