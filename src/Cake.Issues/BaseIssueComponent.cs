namespace Cake.Issues;

using System;
using Cake.Core.Diagnostics;

/// <summary>
/// Base class for all Cake.Issues component.
/// </summary>
/// <typeparam name="T">Type of settings.</typeparam>
public abstract class BaseIssueComponent<T> : IBaseIssueComponent<T>
    where T : class, IRepositorySettings
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseIssueComponent{T}"/> class.
    /// </summary>
    /// <param name="log">The Cake log context.</param>
    protected BaseIssueComponent(ICakeLog log)
    {
        log.NotNull();

        this.Log = log;
    }

    /// <summary>
    /// Gets the Cake log context.
    /// </summary>
    protected ICakeLog Log { get; }

    /// <summary>
    /// Gets the settings.
    /// Is set after <see cref="Initialize"/> was called from the core add-in.
    /// </summary>
    protected T Settings { get; private set; }

    /// <inheritdoc/>
    public virtual bool Initialize(T settings)
    {
        settings.NotNull();

        this.Settings = settings;

        return true;
    }

    /// <inheritdoc/>
    public void AssertInitialized()
    {
        if (this.Settings == null)
        {
            throw new InvalidOperationException("Initialize needs to be called first.");
        }
    }
}
