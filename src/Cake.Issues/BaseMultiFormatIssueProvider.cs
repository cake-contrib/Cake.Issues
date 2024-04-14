namespace Cake.Issues
{
    using System.Collections.Generic;
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Base class for issue providers supporting multiple log formats.
    /// </summary>
    /// <typeparam name="TSettings">Type of the settings.</typeparam>
    /// <typeparam name="TIssueProvider">Type of the issue provider.</typeparam>
    /// <param name="log">The Cake log context.</param>
    /// <param name="settings">Settings for the issue provider.</param>
    public abstract class BaseMultiFormatIssueProvider<TSettings, TIssueProvider>(ICakeLog log, TSettings settings)
        : BaseConfigurableIssueProvider<TSettings>(log, settings)
        where TSettings : BaseMultiFormatIssueProviderSettings<TIssueProvider, TSettings>
        where TIssueProvider : BaseMultiFormatIssueProvider<TSettings, TIssueProvider>
    {
        /// <inheritdoc/>
        protected override IEnumerable<IIssue> InternalReadIssues()
        {
            return
                this.IssueProviderSettings.Format.ReadIssues(
                    (TIssueProvider)this,
                    this.Settings,
                    this.IssueProviderSettings);
        }
    }
}
