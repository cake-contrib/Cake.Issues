namespace Cake.Issues
{
    using System.Collections.Generic;
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Base class for issue providers supporting multiple log formats.
    /// </summary>
    /// <typeparam name="TSettings">Type of the settings.</typeparam>
    /// <typeparam name="TIssueProvider">Type of the issue provider.</typeparam>
    public abstract class BaseMultiFormatIssueProvider<TSettings, TIssueProvider> : BaseConfigurableIssueProvider<TSettings>
        where TSettings : BaseMultiFormatIssueProviderSettings<TIssueProvider, TSettings>
        where TIssueProvider : BaseMultiFormatIssueProvider<TSettings, TIssueProvider>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseMultiFormatIssueProvider{TSettings, TIssueProvider}"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for the issue provider.</param>
        protected BaseMultiFormatIssueProvider(ICakeLog log, TSettings settings)
            : base(log, settings)
        {
        }

        /// <inheritdoc/>
        protected override IEnumerable<IIssue> InternalReadIssues(FileLinkSettings fileLinkSettings)
        {
            return
                this.IssueProviderSettings.Format.ReadIssues(
                    (TIssueProvider)this,
                    this.Settings,
                    this.IssueProviderSettings);
        }
    }
}
