namespace Cake.Issues
{
    /// <summary>
    /// Interface describing a Cake.Issues component.
    /// </summary>
    /// <typeparam name="T">Type of settings.</typeparam>
    public interface IBaseIssueComponent<in T>
        where T : RepositorySettings
    {
        /// <summary>
        /// Initializes the issue provider.
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <returns><c>true</c> if the initialization was successful, <c>false</c> otherwise.</returns>
        bool Initialize(T settings);
    }
}
