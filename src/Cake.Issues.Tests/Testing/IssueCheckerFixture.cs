namespace Cake.Issues.Tests.Testing
{
    internal class IssueCheckerFixture : IssueBuilderFixture
    {
        public IssueCheckerFixture()
            : this("Identifier", "Message", "ProviderType", "ProviderName")
        {
        }

        public IssueCheckerFixture(string identifier, string messageText, string providerType, string providerName)
            : base(identifier, messageText, providerType, providerName)
        {
            this.ProviderType = providerType;
            this.ProviderName = providerName;
            this.Run = "Test Run";
            this.Identifier = identifier;
            this.ProjectFileRelativePath = @"src\project.file";
            this.ProjectName = "ProjectName";
            this.AffectedFileRelativePath = @"src\source.file";
            this.Line = 42;
            this.EndLine = 420;
            this.Column = 23;
            this.EndColumn = 230;
            this.FileLink = new Uri("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12");
            this.MessageText = messageText;
            this.MessageHtml = "messageHtml";
            this.MessageMarkdown = "messageMarkdown";
            this.Priority = 100;
            this.PriorityName = "PriorityName";
            this.Rule = "Rule";
            this.RuleName = "RuleName";
            this.RuleUrl = new Uri("https://google.com");
            this.AdditionalInformation = [];

            _ = this.IssueBuilder
                .ForRun(this.Run)
                .WithMessageInHtmlFormat(this.MessageHtml)
                .WithMessageInMarkdownFormat(this.MessageMarkdown)
                .InProject(this.ProjectFileRelativePath, this.ProjectName)
                .InFile(this.AffectedFileRelativePath, this.Line, this.EndLine, this.Column, this.EndColumn)
                .WithFileLink(this.FileLink)
                .OfRule(this.Rule, this.RuleName, this.RuleUrl)
                .WithPriority(this.Priority, this.PriorityName)
                .WithAdditionalInformation(this.AdditionalInformation);

            this.Issue =
                this.IssueBuilder.Create();
        }

        public IIssue Issue { get; }

        public string ProviderType { get; }

        public string ProviderName { get; }

        public string Run { get; }

        public string Identifier { get; }

        public string ProjectFileRelativePath { get; }

        public string ProjectName { get; }

        public string AffectedFileRelativePath { get; }

        public int Line { get; }

        public int EndLine { get; }

        public int Column { get; }

        public int EndColumn { get; }

        public Uri FileLink { get; }

        public string MessageText { get; }

        public string MessageHtml { get; }

        public string MessageMarkdown { get; }

        public int Priority { get; }

        public string PriorityName { get; }

        public string Rule { get; }

        public string RuleName { get; }

        public Uri RuleUrl { get; }

        public Dictionary<string, string> AdditionalInformation { get; }
    }
}
