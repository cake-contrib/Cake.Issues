namespace Cake.Issues
{
    using System;
    using Core.Diagnostics;

    /// <summary>
    /// Base class for all Cake.Issues component.
    /// </summary>
    /// <typeparam name="T">Type of settings.</typeparam>
    public abstract class BaseIssueComponent<T> : IBaseIssueComponent<T>
        where T : RepositorySettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseIssueComponent{T}"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        protected BaseIssueComponent(ICakeLog log)
        {
            log.NotNull(nameof(log));

            this.Log = log;
        }

        /// <summary>
        /// Gets the Cake log context.
        /// </summary>
        protected ICakeLog Log { get; }

        /// <summary>
        /// Gets the settings.
        /// Is set after <see cref="Initialize"/> was called from the core addin.
        /// </summary>
        protected T Settings { get; private set; }

        /// <inheritdoc/>
        public virtual bool Initialize(T settings)
        {
            settings.NotNull(nameof(settings));

            this.Settings = settings;

            return true;
        }

        /// <summary>
        /// Asserts that settings are set.
        /// </summary>
        protected void AssertSettings()
        {
            if (this.Settings == null)
            {
                throw new InvalidOperationException("Initialize needs to be called first.");
            }
        }
    }
}
