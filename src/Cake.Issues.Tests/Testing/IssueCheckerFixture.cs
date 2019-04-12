namespace Cake.Issues.Tests
{
    using System;

    internal class IssueCheckerFixture : IssueBuilderFixture
    {
        public IssueCheckerFixture()
            : this("Message", "ProviderType", "ProviderName")
        {
        }

        public IssueCheckerFixture(string message, string providerType, string providerName)
            : base(message, providerType, providerName)
        {
            this.ProviderType = providerType;
            this.ProviderName = providerName;
            this.ProjectFileRelativePath = @"src\project.file";
            this.ProjectName = "ProjectName";
            this.AffectedFileRelativePath = @"src\source.file";
            this.Line = 42;
            this.Message = message;
            this.Priority = 100;
            this.PriorityName = "PriorityName";
            this.Rule = "Rule";
            this.RuleUrl = new Uri("https://google.com");

            this.IssueBuilder
                .InProject(this.ProjectFileRelativePath, this.ProjectName)
                .InFile(this.AffectedFileRelativePath, this.Line)
                .OfRule(this.Rule, this.RuleUrl)
                .WithPriority(this.Priority, this.PriorityName);

            this.Issue =
                this.IssueBuilder.Create();
        }

        public IIssue Issue { get; private set; }

        public string ProviderType { get; private set; }

        public string ProviderName { get; private set; }

        public string ProjectFileRelativePath { get; private set; }

        public string ProjectName { get; private set; }

        public string AffectedFileRelativePath { get; private set; }

        public int Line { get; private set; }

        public string Message { get; private set; }

        public int Priority { get; private set; }

        public string PriorityName { get; private set; }

        public string Rule { get; private set; }

        public Uri RuleUrl { get; private set; }
    }
}
