namespace Cake.Issues.Reporting.Generic
{
    /// <summary>
    /// Possible types how a issue can be linked to the file.
    /// </summary>
    public enum FileLinkType
    {
        /// <summary>
        /// Link through a hyperlink.
        /// </summary>
        Href,

        /// <summary>
        /// Link through a JavaScript click.
        /// </summary>
        OnClick
    }
}
