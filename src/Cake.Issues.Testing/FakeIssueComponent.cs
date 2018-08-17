namespace Cake.Issues.Testing
{
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Implementation of a <see cref="BaseIssueComponent{T}"/> for use in test cases.
    /// </summary>
    public class FakeIssueComponent : BaseIssueComponent<RepositorySettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FakeIssueComponent"/> class.
        /// </summary>
        /// <param name="log">The Cake log instance</param>
        public FakeIssueComponent(ICakeLog log)
            : base(log)
        {
        }

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
