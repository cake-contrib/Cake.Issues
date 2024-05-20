namespace Cake.Issues.Tests.Testing
{
    public sealed class IssueCheckerTests
    {
        public sealed class TheCheckMethodWithIssueBuilder
        {
            [Fact]
            public void Should_Throw_If_IssueToCheck_Is_Null()
            {
                // Given
                var fixture = new IssueBuilderFixture();
                const IIssue issueToCheck = null;
                var expectedIssue = fixture.IssueBuilder;

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(issueToCheck, expectedIssue));

                // Then
                result.IsArgumentNullException("issueToCheck");
            }

            [Fact]
            public void Should_Throw_If_ExpectedIssue_Is_Null()
            {
                // Given
                var fixture = new IssueBuilderFixture();
                var issueToCheck = fixture.IssueBuilder.Create();
                const IssueBuilder expectedIssue = null;

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(issueToCheck, expectedIssue));

                // Then
                result.IsArgumentNullException("expectedIssue");
            }

            [Fact]
            public void Should_Not_Throw_If_Issues_Are_Identical()
            {
                // Given
                var fixture = new IssueCheckerFixture();
                var issueToCheck = fixture.IssueBuilder.Create();
                var expectedIssue = fixture.IssueBuilder;

                // When
                IssueChecker.Check(issueToCheck, expectedIssue);

                // Then
            }
        }

        public sealed class TheCheckMethodComparingTwoIssues
        {
            [Fact]
            public void Should_Throw_If_IssueToCheck_Is_Null()
            {
                // Given
                var fixture = new IssueBuilderFixture();
                const IIssue issueToCheck = null;
                var expectedIssue = fixture.IssueBuilder.Create();

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(issueToCheck, expectedIssue));

                // Then
                result.IsArgumentNullException("issueToCheck");
            }

            [Fact]
            public void Should_Throw_If_ExpectedIssue_Is_Null()
            {
                // Given
                var fixture = new IssueBuilderFixture();
                var issueToCheck = fixture.IssueBuilder.Create();
                const IIssue expectedIssue = null;

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(issueToCheck, expectedIssue));

                // Then
                result.IsArgumentNullException("expectedIssue");
            }

            [Fact]
            public void Should_Not_Throw_If_Issues_Are_Identical()
            {
                // Given
                var fixture = new IssueCheckerFixture();
                var issueToCheck = fixture.IssueBuilder.Create();
                var expectedIssue = fixture.IssueBuilder.Create();

                // When
                IssueChecker.Check(issueToCheck, expectedIssue);

                // Then
            }
        }

        public sealed class TheCheckMethodWithIndividualValues
        {
            [Fact]
            public void Should_Throw_If_Issue_Is_Null()
            {
                // Given
                var fixture = new IssueCheckerFixture();
                const IIssue issue = null;

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        issue,
                        fixture.ProviderType,
                        fixture.ProviderName,
                        fixture.Run,
                        fixture.Identifier,
                        fixture.ProjectFileRelativePath,
                        fixture.ProjectName,
                        fixture.AffectedFileRelativePath,
                        fixture.Line,
                        fixture.EndLine,
                        fixture.Column,
                        fixture.EndColumn,
                        fixture.FileLink,
                        fixture.MessageText,
                        fixture.MessageHtml,
                        fixture.MessageMarkdown,
                        fixture.Priority,
                        fixture.PriorityName,
                        fixture.Rule,
                        fixture.RuleName,
                        fixture.RuleUrl,
                        fixture.AdditionalInformation));

                // Then
                result.IsArgumentNullException("issue");
            }

            [Fact]
            public void Should_Not_Throw_If_All_Values_Are_The_Same()
            {
                // Given
                var fixture = new IssueCheckerFixture();

                // When
                IssueChecker.Check(
                    fixture.Issue,
                    fixture.ProviderType,
                    fixture.ProviderName,
                    fixture.Run,
                    fixture.Identifier,
                    fixture.ProjectFileRelativePath,
                    fixture.ProjectName,
                    fixture.AffectedFileRelativePath,
                    fixture.Line,
                    fixture.EndLine,
                    fixture.Column,
                    fixture.EndColumn,
                    fixture.FileLink,
                    fixture.MessageText,
                    fixture.MessageHtml,
                    fixture.MessageMarkdown,
                    fixture.Priority,
                    fixture.PriorityName,
                    fixture.Rule,
                    fixture.RuleName,
                    fixture.RuleUrl,
                    fixture.AdditionalInformation);

                // Then
            }

            [Theory]
            [InlineData("ProviderType", "Foo")]
            [InlineData(null, "Foo")]
            [InlineData("", "Foo")]
            [InlineData(" ", "Foo")]
            public void Should_Throw_If_ProviderType_Is_Different(string expectedValue, string actualValue)
            {
                // Given
                var fixture = new IssueCheckerFixture("Identifier", "Message", actualValue, "ProviderName");

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        fixture.Issue,
                        expectedValue,
                        fixture.ProviderName,
                        fixture.Run,
                        fixture.Identifier,
                        fixture.ProjectFileRelativePath,
                        fixture.ProjectName,
                        fixture.AffectedFileRelativePath,
                        fixture.Line,
                        fixture.EndLine,
                        fixture.Column,
                        fixture.EndColumn,
                        fixture.FileLink,
                        fixture.MessageText,
                        fixture.MessageHtml,
                        fixture.MessageMarkdown,
                        fixture.Priority,
                        fixture.PriorityName,
                        fixture.Rule,
                        fixture.RuleName,
                        fixture.RuleUrl,
                        fixture.AdditionalInformation));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldStartWith("Expected issue.ProviderType");
            }

            [Theory]
            [InlineData("ProviderName", "Foo")]
            [InlineData(null, "Foo")]
            [InlineData("", "Foo")]
            [InlineData(" ", "Foo")]
            public void Should_Throw_If_ProviderName_Is_Different(string expectedValue, string actualValue)
            {
                // Given
                var fixture = new IssueCheckerFixture("Identifier", "Message", "ProviderType", actualValue);

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        fixture.Issue,
                        fixture.ProviderType,
                        expectedValue,
                        fixture.Run,
                        fixture.Identifier,
                        fixture.ProjectFileRelativePath,
                        fixture.ProjectName,
                        fixture.AffectedFileRelativePath,
                        fixture.Line,
                        fixture.EndLine,
                        fixture.Column,
                        fixture.EndColumn,
                        fixture.FileLink,
                        fixture.MessageText,
                        fixture.MessageHtml,
                        fixture.MessageMarkdown,
                        fixture.Priority,
                        fixture.PriorityName,
                        fixture.Rule,
                        fixture.RuleName,
                        fixture.RuleUrl,
                        fixture.AdditionalInformation));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldStartWith("Expected issue.ProviderName");
            }

            [Theory]
            [InlineData("Run", "Foo")]
            [InlineData(null, "Foo")]
            [InlineData("", "Foo")]
            [InlineData(" ", "Foo")]
            public void Should_Throw_If_Run_Is_Different(string expectedValue, string actualValue)
            {
                // Given
                var fixture = new IssueCheckerFixture();
                var issue =
                    fixture.IssueBuilder
                        .ForRun(actualValue)
                        .Create();

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        issue,
                        fixture.ProviderType,
                        fixture.ProviderName,
                        expectedValue,
                        fixture.Identifier,
                        fixture.ProjectFileRelativePath,
                        fixture.ProjectName,
                        fixture.AffectedFileRelativePath,
                        fixture.Line,
                        fixture.EndLine,
                        fixture.Column,
                        fixture.EndColumn,
                        fixture.FileLink,
                        fixture.MessageText,
                        fixture.MessageHtml,
                        fixture.MessageMarkdown,
                        fixture.Priority,
                        fixture.PriorityName,
                        fixture.Rule,
                        fixture.RuleName,
                        fixture.RuleUrl,
                        fixture.AdditionalInformation));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldStartWith("Expected issue.Run");
            }

            [Theory]
            [InlineData("Message", "Foo")]
            [InlineData(null, "Foo")]
            [InlineData("", "Foo")]
            [InlineData(" ", "Foo")]
            public void Should_Throw_If_Identifier_Is_Different(string expectedValue, string actualValue)
            {
                // Given
                var fixture = new IssueCheckerFixture(actualValue, "Message", "ProviderType", "ProviderName");

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        fixture.Issue,
                        fixture.ProviderType,
                        fixture.ProviderName,
                        fixture.Run,
                        expectedValue,
                        fixture.ProjectFileRelativePath,
                        fixture.ProjectName,
                        fixture.AffectedFileRelativePath,
                        fixture.Line,
                        fixture.EndLine,
                        fixture.Column,
                        fixture.EndColumn,
                        fixture.FileLink,
                        fixture.MessageText,
                        fixture.MessageHtml,
                        fixture.MessageMarkdown,
                        fixture.Priority,
                        fixture.PriorityName,
                        fixture.Rule,
                        fixture.RuleName,
                        fixture.RuleUrl,
                        fixture.AdditionalInformation));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldStartWith("Expected issue.Identifier");
            }

            [Theory]
            [InlineData(@"src\project.file", @"src\foo")]
            public void Should_Throw_If_ProjectFileRelativePath_Is_Different(string expectedValue, string actualValue)
            {
                // Given
                var fixture = new IssueCheckerFixture();
                var issue =
                    fixture.IssueBuilder
                        .InProjectFile(actualValue)
                        .Create();

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        issue,
                        fixture.ProviderType,
                        fixture.ProviderName,
                        fixture.Run,
                        fixture.Identifier,
                        expectedValue,
                        fixture.ProjectName,
                        fixture.AffectedFileRelativePath,
                        fixture.Line,
                        fixture.EndLine,
                        fixture.Column,
                        fixture.EndColumn,
                        fixture.FileLink,
                        fixture.MessageText,
                        fixture.MessageHtml,
                        fixture.MessageMarkdown,
                        fixture.Priority,
                        fixture.PriorityName,
                        fixture.Rule,
                        fixture.RuleName,
                        fixture.RuleUrl,
                        fixture.AdditionalInformation));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldStartWith("Expected issue.ProjectFileRelativePath");
            }

            [Theory]
            [InlineData("ProjectName", "Foo")]
            [InlineData(null, "Foo")]
            [InlineData("", "Foo")]
            [InlineData(" ", "Foo")]
            public void Should_Throw_If_ProjectName_Is_Different(string expectedValue, string actualValue)
            {
                // Given
                var fixture = new IssueCheckerFixture();
                var issue =
                    fixture.IssueBuilder
                        .InProject(fixture.ProjectFileRelativePath, actualValue)
                        .Create();

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        issue,
                        fixture.ProviderType,
                        fixture.ProviderName,
                        fixture.Run,
                        fixture.Identifier,
                        fixture.ProjectFileRelativePath,
                        expectedValue,
                        fixture.AffectedFileRelativePath,
                        fixture.Line,
                        fixture.EndLine,
                        fixture.Column,
                        fixture.EndColumn,
                        fixture.FileLink,
                        fixture.MessageText,
                        fixture.MessageHtml,
                        fixture.MessageMarkdown,
                        fixture.Priority,
                        fixture.PriorityName,
                        fixture.Rule,
                        fixture.RuleName,
                        fixture.RuleUrl,
                        fixture.AdditionalInformation));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldStartWith("Expected issue.ProjectName");
            }

            [Theory]
            [InlineData(@"src\source.file", @"src\foo")]
            public void Should_Throw_If_AffectedFileRelativePath_Is_Different(string expectedValue, string actualValue)
            {
                // Given
                var fixture = new IssueCheckerFixture();
                var issue =
                    fixture.IssueBuilder
                        .InFile(actualValue)
                        .Create();

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        issue,
                        fixture.ProviderType,
                        fixture.ProviderName,
                        fixture.Run,
                        fixture.Identifier,
                        fixture.ProjectFileRelativePath,
                        fixture.ProjectName,
                        expectedValue,
                        fixture.Line,
                        fixture.EndLine,
                        fixture.Column,
                        fixture.EndColumn,
                        fixture.FileLink,
                        fixture.MessageText,
                        fixture.MessageHtml,
                        fixture.MessageMarkdown,
                        fixture.Priority,
                        fixture.PriorityName,
                        fixture.Rule,
                        fixture.RuleName,
                        fixture.RuleUrl,
                        fixture.AdditionalInformation));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldStartWith("Expected issue.AffectedFileRelativePath");
            }

            [Theory]
            [InlineData(42, 23)]
            [InlineData(null, 42)]
            [InlineData(42, null)]
            public void Should_Throw_If_Line_Is_Different(int? expectedValue, int? actualValue)
            {
                // Given
                var fixture = new IssueCheckerFixture();
                var issue =
                    fixture.IssueBuilder
                        .InFile(fixture.AffectedFileRelativePath, actualValue)
                        .Create();

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        issue,
                        fixture.ProviderType,
                        fixture.ProviderName,
                        fixture.Run,
                        fixture.Identifier,
                        fixture.ProjectFileRelativePath,
                        fixture.ProjectName,
                        fixture.AffectedFileRelativePath,
                        expectedValue,
                        null,
                        null,
                        null,
                        fixture.FileLink,
                        fixture.MessageText,
                        fixture.MessageHtml,
                        fixture.MessageMarkdown,
                        fixture.Priority,
                        fixture.PriorityName,
                        fixture.Rule,
                        fixture.RuleName,
                        fixture.RuleUrl,
                        fixture.AdditionalInformation));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldStartWith("Expected issue.Line");
            }

            [Theory]
            [InlineData(420, 230)]
            [InlineData(null, 420)]
            [InlineData(420, null)]
            public void Should_Throw_If_EndLine_Is_Different(int? expectedValue, int? actualValue)
            {
                // Given
                var fixture = new IssueCheckerFixture();
                var issue =
                    fixture.IssueBuilder
                        .InFile(fixture.AffectedFileRelativePath, fixture.Line, actualValue, fixture.Column, fixture.EndColumn)
                        .Create();

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        issue,
                        fixture.ProviderType,
                        fixture.ProviderName,
                        fixture.Run,
                        fixture.Identifier,
                        fixture.ProjectFileRelativePath,
                        fixture.ProjectName,
                        fixture.AffectedFileRelativePath,
                        fixture.Line,
                        expectedValue,
                        fixture.Column,
                        fixture.EndColumn,
                        fixture.FileLink,
                        fixture.MessageText,
                        fixture.MessageHtml,
                        fixture.MessageMarkdown,
                        fixture.Priority,
                        fixture.PriorityName,
                        fixture.Rule,
                        fixture.RuleName,
                        fixture.RuleUrl,
                        fixture.AdditionalInformation));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldStartWith("Expected issue.EndLine");
            }

            [Theory]
            [InlineData(42, 23)]
            [InlineData(null, 42)]
            [InlineData(42, null)]
            public void Should_Throw_If_Column_Is_Different(int? expectedValue, int? actualValue)
            {
                // Given
                var fixture = new IssueCheckerFixture();
                var issue =
                    fixture.IssueBuilder
                        .InFile(fixture.AffectedFileRelativePath, fixture.Line, fixture.EndLine, actualValue, null)
                        .Create();

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        issue,
                        fixture.ProviderType,
                        fixture.ProviderName,
                        fixture.Run,
                        fixture.Identifier,
                        fixture.ProjectFileRelativePath,
                        fixture.ProjectName,
                        fixture.AffectedFileRelativePath,
                        fixture.Line,
                        fixture.EndLine,
                        expectedValue,
                        null,
                        fixture.FileLink,
                        fixture.MessageText,
                        fixture.MessageHtml,
                        fixture.MessageMarkdown,
                        fixture.Priority,
                        fixture.PriorityName,
                        fixture.Rule,
                        fixture.RuleName,
                        fixture.RuleUrl,
                        fixture.AdditionalInformation));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldStartWith("Expected issue.Column");
            }

            [Theory]
            [InlineData(42, 23)]
            [InlineData(null, 42)]
            [InlineData(42, null)]
            public void Should_Throw_If_EndColumn_Is_Different(int? expectedValue, int? actualValue)
            {
                // Given
                var fixture = new IssueCheckerFixture();
                var issue =
                    fixture.IssueBuilder
                        .InFile(fixture.AffectedFileRelativePath, fixture.Line, fixture.EndLine, fixture.Column, actualValue)
                        .Create();

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        issue,
                        fixture.ProviderType,
                        fixture.ProviderName,
                        fixture.Run,
                        fixture.Identifier,
                        fixture.ProjectFileRelativePath,
                        fixture.ProjectName,
                        fixture.AffectedFileRelativePath,
                        fixture.Line,
                        fixture.EndLine,
                        fixture.Column,
                        expectedValue,
                        fixture.FileLink,
                        fixture.MessageText,
                        fixture.MessageHtml,
                        fixture.MessageMarkdown,
                        fixture.Priority,
                        fixture.PriorityName,
                        fixture.Rule,
                        fixture.RuleName,
                        fixture.RuleUrl,
                        fixture.AdditionalInformation));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldStartWith("Expected issue.EndColumn");
            }

            [Theory]
            [InlineData("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12", "https://github.com/foo/bar/blob/develop/src/bar.cs")]
            [InlineData("https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L12", "https://github.com/myorg/myrepo/blob/develop/src/foo.cs#L10-L13")]
            public void Should_Throw_If_FileLink_Is_Different(string expectedValue, string actualValue)
            {
                // Given
                var fixture = new IssueCheckerFixture();
                var issue =
                    fixture.IssueBuilder
                        .WithFileLink(new Uri(actualValue))
                        .Create();

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        issue,
                        fixture.ProviderType,
                        fixture.ProviderName,
                        fixture.Run,
                        fixture.Identifier,
                        fixture.ProjectFileRelativePath,
                        fixture.ProjectName,
                        fixture.AffectedFileRelativePath,
                        fixture.Line,
                        fixture.EndLine,
                        fixture.Column,
                        fixture.EndColumn,
                        new Uri(expectedValue),
                        fixture.MessageText,
                        fixture.MessageHtml,
                        fixture.MessageMarkdown,
                        fixture.Priority,
                        fixture.PriorityName,
                        fixture.Rule,
                        fixture.RuleName,
                        fixture.RuleUrl,
                        fixture.AdditionalInformation));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldStartWith("Expected issue.FileLink");
            }

            [Theory]
            [InlineData("Message", "Foo")]
            [InlineData(null, "Foo")]
            [InlineData("", "Foo")]
            [InlineData(" ", "Foo")]
            public void Should_Throw_If_MessageText_Is_Different(string expectedValue, string actualValue)
            {
                // Given
                var fixture = new IssueCheckerFixture("Identifier", actualValue, "ProviderType", "ProviderName");

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        fixture.Issue,
                        fixture.ProviderType,
                        fixture.ProviderName,
                        fixture.Run,
                        fixture.Identifier,
                        fixture.ProjectFileRelativePath,
                        fixture.ProjectName,
                        fixture.AffectedFileRelativePath,
                        fixture.Line,
                        fixture.EndLine,
                        fixture.Column,
                        fixture.EndColumn,
                        fixture.FileLink,
                        expectedValue,
                        fixture.MessageHtml,
                        fixture.MessageMarkdown,
                        fixture.Priority,
                        fixture.PriorityName,
                        fixture.Rule,
                        fixture.RuleName,
                        fixture.RuleUrl,
                        fixture.AdditionalInformation));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldStartWith("Expected issue.MessageText");
            }

            [Theory]
            [InlineData("Message", "Foo")]
            [InlineData(null, "Foo")]
            [InlineData("", "Foo")]
            [InlineData(" ", "Foo")]
            public void Should_Throw_If_MessageHtml_Is_Different(string expectedValue, string actualValue)
            {
                // Given
                var fixture = new IssueCheckerFixture();
                var issue =
                    fixture.IssueBuilder
                        .WithMessageInHtmlFormat(actualValue)
                        .Create();

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        issue,
                        fixture.ProviderType,
                        fixture.ProviderName,
                        fixture.Run,
                        fixture.Identifier,
                        fixture.ProjectFileRelativePath,
                        fixture.ProjectName,
                        fixture.AffectedFileRelativePath,
                        fixture.Line,
                        fixture.EndLine,
                        fixture.Column,
                        fixture.EndColumn,
                        fixture.FileLink,
                        fixture.MessageText,
                        expectedValue,
                        fixture.MessageMarkdown,
                        fixture.Priority,
                        fixture.PriorityName,
                        fixture.Rule,
                        fixture.RuleName,
                        fixture.RuleUrl,
                        fixture.AdditionalInformation));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldStartWith("Expected issue.MessageHtml");
            }

            [Theory]
            [InlineData("Message", "Foo")]
            [InlineData(null, "Foo")]
            [InlineData("", "Foo")]
            [InlineData(" ", "Foo")]
            public void Should_Throw_If_MessageMarkdown_Is_Different(string expectedValue, string actualValue)
            {
                // Given
                var fixture = new IssueCheckerFixture();
                var issue =
                    fixture.IssueBuilder
                        .WithMessageInMarkdownFormat(actualValue)
                        .Create();

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        issue,
                        fixture.ProviderType,
                        fixture.ProviderName,
                        fixture.Run,
                        fixture.Identifier,
                        fixture.ProjectFileRelativePath,
                        fixture.ProjectName,
                        fixture.AffectedFileRelativePath,
                        fixture.Line,
                        fixture.EndLine,
                        fixture.Column,
                        fixture.EndColumn,
                        fixture.FileLink,
                        fixture.MessageText,
                        fixture.MessageHtml,
                        expectedValue,
                        fixture.Priority,
                        fixture.PriorityName,
                        fixture.Rule,
                        fixture.RuleName,
                        fixture.RuleUrl,
                        fixture.AdditionalInformation));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldStartWith("Expected issue.MessageMarkdown");
            }

            [Theory]
            [InlineData(IssuePriority.Error, IssuePriority.Warning)]
            public void Should_Throw_If_Priority_Is_Different(IssuePriority expectedValue, IssuePriority actualValue)
            {
                // Given
                var fixture = new IssueCheckerFixture();
                var issue =
                    fixture.IssueBuilder
                        .WithPriority(actualValue)
                        .Create();

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        issue,
                        fixture.ProviderType,
                        fixture.ProviderName,
                        fixture.Run,
                        fixture.Identifier,
                        fixture.ProjectFileRelativePath,
                        fixture.ProjectName,
                        fixture.AffectedFileRelativePath,
                        fixture.Line,
                        fixture.EndLine,
                        fixture.Column,
                        fixture.EndColumn,
                        fixture.FileLink,
                        fixture.MessageText,
                        fixture.MessageHtml,
                        fixture.MessageMarkdown,
                        (int)expectedValue,
                        fixture.PriorityName,
                        fixture.Rule,
                        fixture.RuleName,
                        fixture.RuleUrl,
                        fixture.AdditionalInformation));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldStartWith("Expected issue.Priority");
            }

            [Theory]
            [InlineData("PriorityName", "Foo")]
            [InlineData(null, "Foo")]
            [InlineData("", "Foo")]
            [InlineData(" ", "Foo")]
            public void Should_Throw_If_PriorityName_Is_Different(string expectedValue, string actualValue)
            {
                // Given
                var fixture = new IssueCheckerFixture();
                var issue =
                    fixture.IssueBuilder
                        .WithPriority(fixture.Priority, actualValue)
                        .Create();

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        issue,
                        fixture.ProviderType,
                        fixture.ProviderName,
                        fixture.Run,
                        fixture.Identifier,
                        fixture.ProjectFileRelativePath,
                        fixture.ProjectName,
                        fixture.AffectedFileRelativePath,
                        fixture.Line,
                        fixture.EndLine,
                        fixture.Column,
                        fixture.EndColumn,
                        fixture.FileLink,
                        fixture.MessageText,
                        fixture.MessageHtml,
                        fixture.MessageMarkdown,
                        fixture.Priority,
                        expectedValue,
                        fixture.Rule,
                        fixture.RuleName,
                        fixture.RuleUrl,
                        fixture.AdditionalInformation));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldStartWith("Expected issue.PriorityName");
            }

            [Theory]
            [InlineData("Rule", "Foo")]
            [InlineData(null, "Foo")]
            [InlineData("", "Foo")]
            [InlineData(" ", "Foo")]
            public void Should_Throw_If_Rule_Is_Different(string expectedValue, string actualValue)
            {
                // Given
                var fixture = new IssueCheckerFixture();
                var issue =
                    fixture.IssueBuilder
                        .OfRule(actualValue)
                        .Create();

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        issue,
                        fixture.ProviderType,
                        fixture.ProviderName,
                        fixture.Run,
                        fixture.Identifier,
                        fixture.ProjectFileRelativePath,
                        fixture.ProjectName,
                        fixture.AffectedFileRelativePath,
                        fixture.Line,
                        fixture.EndLine,
                        fixture.Column,
                        fixture.EndColumn,
                        fixture.FileLink,
                        fixture.MessageText,
                        fixture.MessageHtml,
                        fixture.MessageMarkdown,
                        fixture.Priority,
                        fixture.PriorityName,
                        expectedValue,
                        fixture.RuleName,
                        fixture.RuleUrl,
                        fixture.AdditionalInformation));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldStartWith("Expected issue.Rule");
            }

            [Theory]
            [InlineData("Rule", "Foo")]
            [InlineData(null, "Foo")]
            [InlineData("", "Foo")]
            [InlineData(" ", "Foo")]
            public void Should_Throw_If_RuleName_Is_Different(string expectedValue, string actualValue)
            {
                // Given
                var fixture = new IssueCheckerFixture();
                var issue =
                    fixture.IssueBuilder
                        .OfRule(fixture.Rule, actualValue)
                        .Create();

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        issue,
                        fixture.ProviderType,
                        fixture.ProviderName,
                        fixture.Run,
                        fixture.Identifier,
                        fixture.ProjectFileRelativePath,
                        fixture.ProjectName,
                        fixture.AffectedFileRelativePath,
                        fixture.Line,
                        fixture.EndLine,
                        fixture.Column,
                        fixture.EndColumn,
                        fixture.FileLink,
                        fixture.MessageText,
                        fixture.MessageHtml,
                        fixture.MessageMarkdown,
                        fixture.Priority,
                        fixture.PriorityName,
                        fixture.Rule,
                        expectedValue,
                        fixture.RuleUrl,
                        fixture.AdditionalInformation));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldStartWith("Expected issue.RuleName");
            }

            [Theory]
            [InlineData("https://google.com", "https://foo.bar")]
            public void Should_Throw_If_RuleUrl_Is_Different(string expectedValue, string actualValue)
            {
                // Given
                var fixture = new IssueCheckerFixture();
                var issue =
                    fixture.IssueBuilder
                        .OfRule(fixture.Rule, new Uri(actualValue))
                        .Create();

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        issue,
                        fixture.ProviderType,
                        fixture.ProviderName,
                        fixture.Run,
                        fixture.Identifier,
                        fixture.ProjectFileRelativePath,
                        fixture.ProjectName,
                        fixture.AffectedFileRelativePath,
                        fixture.Line,
                        fixture.EndLine,
                        fixture.Column,
                        fixture.EndColumn,
                        fixture.FileLink,
                        fixture.MessageText,
                        fixture.MessageHtml,
                        fixture.MessageMarkdown,
                        fixture.Priority,
                        fixture.PriorityName,
                        fixture.Rule,
                        fixture.RuleName,
                        new Uri(expectedValue),
                        fixture.AdditionalInformation));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldStartWith("Expected issue.RuleUrl");
            }

            [Theory]
            [InlineData("author", "george", "author", "tom")]
            public void Should_Throw_If_AdditionalInformation_Is_Different(string expectedKey, string expectedValue, string actualKey, string actualValue)
            {
                // Given
                var fixture = new IssueCheckerFixture();
                var issue =
                    fixture.IssueBuilder
                        .WithAdditionalInformation(actualKey, actualValue)
                        .Create();

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        issue,
                        fixture.ProviderType,
                        fixture.ProviderName,
                        fixture.Run,
                        fixture.Identifier,
                        fixture.ProjectFileRelativePath,
                        fixture.ProjectName,
                        fixture.AffectedFileRelativePath,
                        fixture.Line,
                        fixture.EndLine,
                        fixture.Column,
                        fixture.EndColumn,
                        fixture.FileLink,
                        fixture.MessageText,
                        fixture.MessageHtml,
                        fixture.MessageMarkdown,
                        fixture.Priority,
                        fixture.PriorityName,
                        fixture.Rule,
                        fixture.RuleName,
                        fixture.RuleUrl,
                        new Dictionary<string, string>
                        {
                            { expectedKey, expectedValue },
                        }));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldStartWith("Expected issue.AdditionalInformation");
            }

            [Fact]
            public void Should_Throw_If_AdditionalInformation_Has_Other_Items_Than_Expected()
            {
                // Given
                var fixture = new IssueCheckerFixture();

                var actual = new Dictionary<string, string>
                {
                    {
                        "Key",
                        "Value"
                    },
                    {
                        "UnexpectedKey",
                        "UnexpectedValue"
                    },
                };
                var expected = new Dictionary<string, string>
                {
                    {
                        "Key",
                        "Value"
                    },
                };

                var issue =
                    fixture.IssueBuilder
                        .WithAdditionalInformation(actual)
                        .Create();

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        issue,
                        fixture.ProviderType,
                        fixture.ProviderName,
                        fixture.Run,
                        fixture.Identifier,
                        fixture.ProjectFileRelativePath,
                        fixture.ProjectName,
                        fixture.AffectedFileRelativePath,
                        fixture.Line,
                        fixture.EndLine,
                        fixture.Column,
                        fixture.EndColumn,
                        fixture.FileLink,
                        fixture.MessageText,
                        fixture.MessageHtml,
                        fixture.MessageMarkdown,
                        fixture.Priority,
                        fixture.PriorityName,
                        fixture.Rule,
                        fixture.RuleName,
                        fixture.RuleUrl,
                        expected));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldStartWith("issue.AdditionalInformation contains an item with the key");
            }

            [Fact]
            public void Should_Throw_If_AdditionalInformation_Lacks_An_Item()
            {
                // Given
                var fixture = new IssueCheckerFixture();

                var actual = new Dictionary<string, string>
                {
                    {
                        "Key",
                        "Value"
                    },
                };
                var expected = new Dictionary<string, string>
                {
                    {
                        "Key",
                        "Value"
                    },
                    {
                        "ExpectedKey",
                        "ExpectedValue"
                    },
                };

                var issue =
                    fixture.IssueBuilder
                        .WithAdditionalInformation(actual)
                        .Create();

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        issue,
                        fixture.ProviderType,
                        fixture.ProviderName,
                        fixture.Run,
                        fixture.Identifier,
                        fixture.ProjectFileRelativePath,
                        fixture.ProjectName,
                        fixture.AffectedFileRelativePath,
                        fixture.Line,
                        fixture.EndLine,
                        fixture.Column,
                        fixture.EndColumn,
                        fixture.FileLink,
                        fixture.MessageText,
                        fixture.MessageHtml,
                        fixture.MessageMarkdown,
                        fixture.Priority,
                        fixture.PriorityName,
                        fixture.Rule,
                        fixture.RuleName,
                        fixture.RuleUrl,
                        expected));

                // Then
                var ex = result.ShouldBeOfType<Exception>();
                ex.Message.ShouldStartWith("Expected issue.AdditionalInformation to have an item with the key");
            }
        }
    }
}