namespace Cake.Issues.BuildServer.AppVeyor;

/// <summary>
/// Settings for <see cref="AppVeyorBuildsAliases"/>.
/// </summary>
public class AppVeyorBuildSettings
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AppVeyorBuildSettings"/> class.
    /// </summary>
    public AppVeyorBuildSettings()
    {
    }

    /// <summary>
    /// Gets or sets the pattern of the message to display.
    /// See <see cref="Cake.Issues.IIssueExtensions.ReplaceIssuePattern(string, IIssue)"/> for possible patterns.
    /// The default value is: "Project: {ProjectName}, File: {FilePath}, Line: {Line}".
    /// </summary>
    public string MessagePattern
    {
        get;
        set
        {
            value.NotNull();
            field = value;
        }
    } = "Project: {ProjectName}, File: {FilePath}, Line: {Line}";

    /// <summary>
    /// Gets or sets the pattern of the message details to display.
    /// See <see cref="Cake.Issues.IIssueExtensions.ReplaceIssuePattern(string, IIssue)"/> for possible patterns.
    /// The default value is: "{Rule}: {MessageText}".
    /// </summary>
    public string DetailsPattern
    {
        get;
        set
        {
            value.NotNull();
            field = value;
        }
    } = "{Rule}: {MessageText}";
}