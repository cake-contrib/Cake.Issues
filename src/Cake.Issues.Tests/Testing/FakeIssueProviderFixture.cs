namespace Cake.Issues.Tests.Testing
{
    using System.Collections.Generic;
    using Cake.Issues.Testing;

    internal sealed class FakeIssueProviderFixture : BaseIssueProviderFixture<FakeIssueProvider>
    {
        private readonly List<IIssue> issues = new List<IIssue>();

        public FakeIssueProviderFixture()
        {
        }

        public FakeIssueProviderFixture(IEnumerable<IIssue> issues)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            issues.NotNull(nameof(issues));

            // ReSharper disable once PossibleMultipleEnumeration
            this.issues.AddRange(issues);
        }

        protected override IList<object> GetCreateIssueProviderArguments()
        {
            var result = base.GetCreateIssueProviderArguments();
            result.Add(this.issues);
            return result;
        }
    }
}
