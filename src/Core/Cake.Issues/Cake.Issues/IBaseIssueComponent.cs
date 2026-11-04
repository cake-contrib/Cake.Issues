namespace Cake.Issues;

using System;

/// <summary>
/// Interface describing a Cake.Issues component.
/// </summary>
/// <typeparam name="T">Type of settings.</typeparam>
public interface IBaseIssueComponent<in T>
    where T : IRepositorySettings
{
    /// <summary>
    /// Initializes the component.
    /// </summary>
    /// <param name="settings">The settings.</param>
    /// <returns><c>true</c> if the initialization was successful, <c>false</c> otherwise.</returns>
    bool Initialize(T settings);

    /// <summary>
    /// Asserts that <see cref="Initialize(T)"/> was called.
    /// </summary>
    /// <exception cref="InvalidOperationException">If <see cref="Initialize(T)"/> was not called.</exception>
    void AssertInitialized();
}
