namespace Cake.Issues.Tests
{
    using System;
    using System.Collections.Generic;

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
            this.RuleUrl = new Uri("https://google.com");
            this.AdditionalInformation = new Dictionary<string, string>();

            this.IssueBuilder
                .ForRun(this.Run)
                .WithMessageInHtmlFormat(this.MessageHtml)
                .WithMessageInMarkdownFormat(this.MessageMarkdown)
                .InProject(this.ProjectFileRelativePath, this.ProjectName)
                .InFile(this.AffectedFileRelativePath, this.Line, this.EndLine, this.Column, this.EndColumn)
                .WithFileLink(this.FileLink)
                .OfRule(this.Rule, this.RuleUrl)
                .WithPriority(this.Priority, this.PriorityName)
                .WithAdditionalInformation(this.AdditionalInformation);

            this.Issue =
                this.IssueBuilder.Create();
        }

        public IIssue Issue { get; private set; }

        public string ProviderType { get; private set; }

        public string ProviderName { get; private set; }

        public string Run { get; private set; }

        public string Identifier { get; private set; }

        public string ProjectFileRelativePath { get; private set; }

        public string ProjectName { get; private set; }

        public string AffectedFileRelativePath { get; private set; }

        public int Line { get; private set; }

        public int EndLine { get; private set; }

        public int Column { get; private set; }

        public int EndColumn { get; private set; }

        public Uri FileLink { get; private set; }

        public string MessageText { get; private set; }

        public string MessageHtml { get; private set; }

        public string MessageMarkdown { get; private set; }

        public int Priority { get; private set; }

        public string PriorityName { get; private set; }

        public string Rule { get; private set; }

        public Uri RuleUrl { get; private set; }

        public Dictionary<string, string> AdditionalInformation { get; private set; }
    }
}
