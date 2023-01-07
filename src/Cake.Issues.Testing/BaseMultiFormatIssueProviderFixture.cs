namespace Cake.Issues.Testing
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Base class for test fixtures for testing issue providers inherited from <see cref="BaseMultiFormatIssueProvider{TSettings, TIssueProvider}"/>.
    /// </summary>
    /// <typeparam name="TIssueProvider">Type of issue provider.</typeparam>
    /// <typeparam name="TSettings">Type of the settings for the issue provider.</typeparam>
    /// <typeparam name="TLogFileFormat">Type of the log file format.</typeparam>
    public abstract class BaseMultiFormatIssueProviderFixture<TIssueProvider, TSettings, TLogFileFormat> : BaseConfigurableIssueProviderFixture<TIssueProvider, TSettings>
        where TIssueProvider : BaseMultiFormatIssueProvider<TSettings, TIssueProvider>
        where TSettings : BaseMultiFormatIssueProviderSettings<TIssueProvider, TSettings>
        where TLogFileFormat : ILogFileFormat<TIssueProvider, TSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseMultiFormatIssueProviderFixture{TIssueProvider, TSettings, TLogFileFormat}"/> class.
        /// </summary>
        /// <param name="fileResourceName">Name of the resource to load.</param>
        protected BaseMultiFormatIssueProviderFixture(string fileResourceName)
            : base(fileResourceName)
        {
        }

        /// <inheritdoc/>
        protected override IList<object> GetCreateIssueProviderSettingsArguments()
        {
            var result = base.GetCreateIssueProviderSettingsArguments();
            result.Add((TLogFileFormat)Activator.CreateInstance(typeof(TLogFileFormat), this.Log));
            return result;
        }
    }
}
