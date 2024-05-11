﻿namespace Cake.Issues.Tests.Testing
{
    internal sealed class FakeIssueProviderFixture : BaseIssueProviderFixture<FakeIssueProvider>
    {
        private readonly List<IIssue> issues = [];

        public FakeIssueProviderFixture()
        {
        }

        public FakeIssueProviderFixture(IEnumerable<IIssue> issues)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            issues.NotNull();

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
