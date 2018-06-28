namespace Cake.Issues.Reporting.Generic
{
    /// <summary>
    /// Extensions for <see cref="GenericIssueReportFormatSettings"/>.
    /// </summary>
    public static class GenericIssueReportFormatSettingsExtensions
    {
        /// <summary>
        /// Adds an option which should be passed to the template.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="key">Key of the option.</param>
        /// <param name="value">Value of the option.</param>
        /// <returns>The <paramref name="settings"/> instance with option added to <see cref="GenericIssueReportFormatSettings.Options"/>.</returns>
        public static GenericIssueReportFormatSettings WithOption(this GenericIssueReportFormatSettings settings, string key, object value)
        {
            settings.NotNull(nameof(settings));

            settings.Options.Add(key, value);

            return settings;
        }
    }
}
