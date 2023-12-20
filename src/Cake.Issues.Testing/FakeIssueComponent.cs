namespace Cake.Issues.Testing
{
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Implementation of a <see cref="BaseIssueComponent{T}"/> for use in test cases.
    /// </summary>
    /// <param name="log">The Cake log instance.</param>
    public class FakeIssueComponent(ICakeLog log) : BaseIssueComponent<RepositorySettings>(log)
    {
        /// <summary>
        /// Gets the Cake log instance.
        /// </summary>
        public new ICakeLog Log => base.Log;

        /// <summary>
        /// Gets the settings.
        /// </summary>
        public new RepositorySettings Settings => base.Settings;
    }
}
