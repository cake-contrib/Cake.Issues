namespace Cake.Issues.Testing
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Base class for test fixtures for testing issue providers inherited from <see cref="BaseConfigurableIssueProvider{T}"/>.
    /// </summary>
    /// <typeparam name="TIssueProvider">Type of issue provider.</typeparam>
    /// <typeparam name="TSettings">Type of the settings for the issue provider.</typeparam>
    public abstract class BaseConfigurableIssueProviderFixture<TIssueProvider, TSettings> : BaseIssueProviderFixture<TIssueProvider>
        where TIssueProvider : BaseConfigurableIssueProvider<TSettings>
        where TSettings : IssueProviderSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseConfigurableIssueProviderFixture{TIssueProvider, TSettings}"/> class.
        /// </summary>
        /// <param name="fileResourceName">Name of the resource to load.</param>
        protected BaseConfigurableIssueProviderFixture(string fileResourceName)
        {
            fileResourceName.NotNullOrWhiteSpace();

            var resourceName = this.FileResourceNamespace + fileResourceName;

            using (var ms = new MemoryStream())
            using (var stream = this.GetType().Assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new ArgumentException(
                        $"Resource {resourceName} not found",
                        nameof(fileResourceName));
                }

                stream.CopyTo(ms);
                this.LogFileContent = ms.ToArray();
            }
        }

        /// <summary>
        /// Gets or sets the content of the log file.
        /// </summary>
        public byte[] LogFileContent { get; set; }

        /// <summary>
        /// Gets the namespace where test file resources will be searched.
        /// </summary>
        protected abstract string FileResourceNamespace { get; }

        /// <inheritdoc/>
        protected override IList<object> GetCreateIssueProviderArguments()
        {
            var result = base.GetCreateIssueProviderArguments();
            result.Add(this.CreateIssueProviderSettings());
            return result;
        }

        /// <summary>
        /// Creates a new instance of the issue provider settings.
        /// </summary>
        /// <returns>Instance of the issue provider settings.</returns>
        protected virtual IList<object> GetCreateIssueProviderSettingsArguments() =>
            this.LogFileContent != null
                ? (IList<object>)new List<object> { this.LogFileContent }
                : throw new InvalidOperationException("No log content set.");

        /// <summary>
        /// Creates a new instance of the issue provider settings.
        /// </summary>
        /// <returns>Instance of the issue provider.</returns>
        private TSettings CreateIssueProviderSettings() =>
            (TSettings)Activator.CreateInstance(
                typeof(TSettings),
                [.. this.GetCreateIssueProviderSettingsArguments()]);
    }
}
