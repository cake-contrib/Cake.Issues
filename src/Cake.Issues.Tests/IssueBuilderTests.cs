namespace Cake.Issues.Tests
{
    using System;
    using Cake.Issues.Testing;
    using Cake.Testing;
    using Shouldly;
    using Xunit;

    public sealed class IssueBuilderTests
    {
        public sealed class TheNewIssueMethod
        {
            [Fact]
            public void Should_Throw_If_Message_Is_Null()
            {
                // Given
                string message = null;
                var providerType = "ProviderType";
                var providerName = "ProviderName";

                // When
                var result = Record.Exception(() =>
                    IssueBuilder.NewIssue(message, providerType, providerName));

                // Then
                result.IsArgumentNullException("message");
            }

            [Fact]
            public void Should_Throw_If_Message_Is_Empty()
            {
                // Given
                var message = string.Empty;
                var providerType = "ProviderType";
                var providerName = "ProviderName";

                // When
                var result = Record.Exception(() =>
                    IssueBuilder.NewIssue(message, providerType, providerName));

                // Then
                result.IsArgumentOutOfRangeException("message");
            }

            [Fact]
            public void Should_Throw_If_Message_Is_WhiteSpace()
            {
                // Given
                var message = " ";
                var providerType = "ProviderType";
                var providerName = "ProviderName";

                // When
                var result = Record.Exception(() =>
                    IssueBuilder.NewIssue(message, providerType, providerName));

                // Then
                result.IsArgumentOutOfRangeException("message");
            }

            [Fact]
            public void Should_Throw_If_ProviderType_Is_Null()
            {
                // Given
                var message = "Message";
                string providerType = null;
                var providerName = "ProviderName";

                // When
                var result = Record.Exception(() =>
                    IssueBuilder.NewIssue(message, providerType, providerName));

                // Then
                result.IsArgumentNullException("providerType");
            }

            [Fact]
            public void Should_Throw_If_ProviderType_Is_Empty()
            {
                // Given
                var message = "Message";
                var providerType = string.Empty;
                var providerName = "ProviderName";

                // When
                var result = Record.Exception(() =>
                    IssueBuilder.NewIssue(message, providerType, providerName));

                // Then
                result.IsArgumentOutOfRangeException("providerType");
            }

            [Fact]
            public void Should_Throw_If_ProviderType_Is_WhiteSpace()
            {
                // Given
                var message = "Message";
                var providerType = " ";
                var providerName = "ProviderName";

                // When
                var result = Record.Exception(() =>
                    IssueBuilder.NewIssue(message, providerType, providerName));

                // Then
                result.IsArgumentOutOfRangeException("providerType");
            }

            [Fact]
            public void Should_Throw_If_ProviderName_Is_Null()
            {
                // Given
                var message = "Message";
                var providerType = "ProviderType";
                string providerName = null;

                // When
                var result = Record.Exception(() =>
                    IssueBuilder.NewIssue(message, providerType, providerName));

                // Then
                result.IsArgumentNullException("providerName");
            }

            [Fact]
            public void Should_Throw_If_ProviderName_Is_Empty()
            {
                // Given
                var message = "Message";
                var providerType = "ProviderType";
                var providerName = string.Empty;

                // When
                var result = Record.Exception(() =>
                    IssueBuilder.NewIssue(message, providerType, providerName));

                // Then
                result.IsArgumentOutOfRangeException("providerName");
            }

            [Fact]
            public void Should_Throw_If_ProviderName_Is_WhiteSpace()
            {
                // Given
                var message = "Message";
                var providerType = "ProviderType";
                var providerName = " ";

                // When
                var result = Record.Exception(() =>
                    IssueBuilder.NewIssue(message, providerType, providerName));

                // Then
                result.IsArgumentOutOfRangeException("providerName");
            }
        }

        public sealed class TheNewIssueOfTMethod
        {
            [Fact]
            public void Should_Throw_If_Message_Is_Null()
            {
                // Given
                string message = null;
                var issueProvider = new FakeIssueProvider(new FakeLog());

                // When
                var result = Record.Exception(() =>
                    IssueBuilder.NewIssue(message, issueProvider));

                // Then
                result.IsArgumentNullException("message");
            }

            [Fact]
            public void Should_Throw_If_Message_Is_Empty()
            {
                // Given
                var message = string.Empty;
                var issueProvider = new FakeIssueProvider(new FakeLog());

                // When
                var result = Record.Exception(() =>
                    IssueBuilder.NewIssue(message, issueProvider));

                // Then
                result.IsArgumentOutOfRangeException("message");
            }

            [Fact]
            public void Should_Throw_If_Message_Is_WhiteSpace()
            {
                // Given
                var message = " ";
                var issueProvider = new FakeIssueProvider(new FakeLog());

                // When
                var result = Record.Exception(() =>
                    IssueBuilder.NewIssue(message, issueProvider));

                // Then
                result.IsArgumentOutOfRangeException("message");
            }

            [Fact]
            public void Should_Throw_If_IssueProvider_Is_Null()
            {
                // Given
                var message = "Message";
                IIssueProvider issueProvider = null;

                // When
                var result = Record.Exception(() =>
                    IssueBuilder.NewIssue(message, issueProvider));

                // Then
                result.IsArgumentNullException("issueProvider");
            }
        }

        public sealed class TheInProjectFileMethod
        {
            [Fact]
            public void Should_Handle_Project_Paths_Which_Are_Null()
            {
                // Given
                var fixture = new IssueBuilderFixture();
                string projectPath = null;

                // When
                var issue = fixture.IssueBuilder.InProjectFile(projectPath).Create();

                // Then
                issue.ProjectFileRelativePath.ShouldBe(null);
            }

            [Fact]
            public void Should_Handle_Project_Paths_Which_Are_Empty()
            {
                // Given
                var fixture = new IssueBuilderFixture();
                var projectPath = string.Empty;

                // When
                var issue = fixture.IssueBuilder.InProjectFile(projectPath).Create();

                // Then
                issue.ProjectFileRelativePath.ShouldBe(null);
            }

            [Fact]
            public void Should_Handle_Project_Path_Which_Are_WhiteSpace()
            {
                // Given
                var fixture = new IssueBuilderFixture();
                var projectPath = " ";

                // When
                var issue = fixture.IssueBuilder.InProjectFile(projectPath).Create();

                // Then
                issue.ProjectFileRelativePath.ShouldBe(null);
            }

            [Theory]
            [InlineData(@"src/project.csproj")]
            public void Should_Set_ProjectFileRelativePath(string projectPath)
            {
                // Given
                var fixture = new IssueBuilderFixture();

                // When
                var issue = fixture.IssueBuilder.InProjectFile(projectPath).Create();

                // Then
                issue.ProjectFileRelativePath.ToString().ShouldBe(projectPath);
            }
        }

        public sealed class TheInProjectOfNameMethod
        {
            [Fact]
            public void Should_Handle_Project_Names_Which_Are_Null()
            {
                // Given
                var fixture = new IssueBuilderFixture();
                string projectName = null;

                // When
                var issue = fixture.IssueBuilder.InProjectOfName(projectName).Create();

                // Then
                issue.ProjectName.ShouldBe(projectName);
            }

            [Fact]
            public void Should_Handle_Project_Names_Which_Are_Empty()
            {
                // Given
                var fixture = new IssueBuilderFixture();
                var projectName = string.Empty;

                // When
                var issue = fixture.IssueBuilder.InProjectOfName(projectName).Create();

                // Then
                issue.ProjectName.ShouldBe(projectName);
            }

            [Fact]
            public void Should_Handle_Project_Names_Which_Are_WhiteSpace()
            {
                // Given
                var fixture = new IssueBuilderFixture();
                var projectName = " ";

                // When
                var issue = fixture.IssueBuilder.InProjectOfName(projectName).Create();

                // Then
                issue.ProjectName.ShouldBe(projectName);
            }

            [Theory]
            [InlineData("project")]
            public void Should_Set_ProjectName(string projectName)
            {
                // Given
                var fixture = new IssueBuilderFixture();

                // When
                var issue = fixture.IssueBuilder.InProjectOfName(projectName).Create();

                // Then
                issue.ProjectName.ShouldBe(projectName);
            }
        }

        public sealed class TheInProjectMethod
        {
            [Fact]
            public void Should_Handle_Project_Paths_Which_Are_Null()
            {
                // Given
                var fixture = new IssueBuilderFixture();
                string projectPath = null;
                var projectName = "foo";

                // When
                var issue = fixture.IssueBuilder.InProject(projectPath, projectName).Create();

                // Then
                issue.ProjectFileRelativePath.ShouldBe(null);
            }

            [Fact]
            public void Should_Handle_Project_Paths_Which_Are_Empty()
            {
                // Given
                var fixture = new IssueBuilderFixture();
                var projectPath = string.Empty;
                var projectName = "foo";

                // When
                var issue = fixture.IssueBuilder.InProject(projectPath, projectName).Create();

                // Then
                issue.ProjectFileRelativePath.ShouldBe(null);
            }

            [Fact]
            public void Should_Handle_Project_Paths_Which_Are_WhiteSpace()
            {
                // Given
                var fixture = new IssueBuilderFixture();
                var projectPath = " ";
                var projectName = "foo";

                // When
                var issue = fixture.IssueBuilder.InProject(projectPath, projectName).Create();

                // Then
                issue.ProjectFileRelativePath.ShouldBe(null);
            }

            [Fact]
            public void Should_Handle_Project_Names_Which_Are_Null()
            {
                // Given
                var fixture = new IssueBuilderFixture();
                string projectName = null;
                var projectPath = "foo";

                // When
                var issue = fixture.IssueBuilder.InProject(projectPath, projectName).Create();

                // Then
                issue.ProjectName.ShouldBe(projectName);
            }

            [Fact]
            public void Should_Handle_Project_Names_Which_Are_Empty()
            {
                // Given
                var fixture = new IssueBuilderFixture();
                var projectName = string.Empty;
                var projectPath = "foo";

                // When
                var issue = fixture.IssueBuilder.InProject(projectPath, projectName).Create();

                // Then
                issue.ProjectName.ShouldBe(projectName);
            }

            [Fact]
            public void Should_Handle_Project_Names_Which_Are_WhiteSpace()
            {
                // Given
                var fixture = new IssueBuilderFixture();
                var projectName = " ";
                var projectPath = "foo";

                // When
                var issue = fixture.IssueBuilder.InProject(projectPath, projectName).Create();

                // Then
                issue.ProjectName.ShouldBe(projectName);
            }

            [Theory]
            [InlineData(@"src/project.csproj")]
            public void Should_Set_ProjectFileRelativePath(string projectPath)
            {
                // Given
                var fixture = new IssueBuilderFixture();
                var projectName = "foo";

                // When
                var issue = fixture.IssueBuilder.InProject(projectPath, projectName).Create();

                // Then
                issue.ProjectFileRelativePath.ToString().ShouldBe(projectPath);
            }

            [Theory]
            [InlineData("project")]
            public void Should_Set_ProjectName(string projectName)
            {
                // Given
                var fixture = new IssueBuilderFixture();
                var projectPath = "foo";

                // When
                var issue = fixture.IssueBuilder.InProject(projectPath, projectName).Create();

                // Then
                issue.ProjectName.ShouldBe(projectName);
            }
        }

        public sealed class TheInFileMethod
        {
            [Fact]
            public void Should_Handle_File_Paths_Which_Are_Null()
            {
                // Given
                var fixture = new IssueBuilderFixture();

                // When
                var issue = fixture.IssueBuilder.InFile(null).Create();

                // Then
                issue.AffectedFileRelativePath.ShouldBe(null);
            }

            [Fact]
            public void Should_Handle_File_Paths_Which_Are_Empty()
            {
                // Given
                var fixture = new IssueBuilderFixture();

                // When
                var issue = fixture.IssueBuilder.InFile(string.Empty).Create();

                // Then
                issue.AffectedFileRelativePath.ShouldBe(null);
            }

            [Fact]
            public void Should_Handle_File_Paths_Which_Are_WhiteSpace()
            {
                // Given
                var fixture = new IssueBuilderFixture();

                // When
                var issue = fixture.IssueBuilder.InFile(" ").Create();

                // Then
                issue.AffectedFileRelativePath.ShouldBe(null);
            }

            [Theory]
            [InlineData(@"foo", @"foo")]
            [InlineData(@"foo\bar", @"foo/bar")]
            [InlineData(@"foo/bar", @"foo/bar")]
            [InlineData(@"foo\bar\", @"foo/bar")]
            [InlineData(@"foo/bar/", @"foo/bar")]
            [InlineData(@".\foo", @"foo")]
            [InlineData(@"./foo", @"foo")]
            [InlineData(@"foo\..\bar", @"foo/../bar")]
            [InlineData(@"foo/../bar", @"foo/../bar")]
            public void Should_Set_FilePath(string filePath, string expectedFilePath)
            {
                // Given
                var fixture = new IssueBuilderFixture();

                // When
                var issue = fixture.IssueBuilder.InFile(filePath).Create();

                // Then
                issue.AffectedFileRelativePath.ToString().ShouldBe(expectedFilePath);
                issue.AffectedFileRelativePath.IsRelative.ShouldBe(true, "File path was not set as relative.");
            }
        }

        public sealed class TheInFileLineMethod
        {
            [Fact]
            public void Should_Throw_If_Line_Is_Negative()
            {
                // Given
                var fixture = new IssueBuilderFixture();

                // When
                var result = Record.Exception(() =>
                    fixture.IssueBuilder.InFile("foo", -1));

                // Then
                result.IsArgumentOutOfRangeException("line");
            }

            [Fact]
            public void Should_Throw_If_Line_Is_Zero()
            {
                // Given
                var fixture = new IssueBuilderFixture();

                // When
                var result = Record.Exception(() =>
                    fixture.IssueBuilder.InFile("foo", 0));

                // Then
                result.IsArgumentOutOfRangeException("line");
            }

            [Fact]
            public void Should_Handle_Line_Which_Is_Null()
            {
                // Given
                var fixture = new IssueBuilderFixture();

                // When
                var issue = fixture.IssueBuilder.InFile("foo", null).Create();

                // Then
                issue.Line.ShouldBe(null);
            }

            [Theory]
            [InlineData(@"foo", @"foo")]
            [InlineData(@"foo\bar", @"foo/bar")]
            [InlineData(@"foo/bar", @"foo/bar")]
            [InlineData(@"foo\bar\", @"foo/bar")]
            [InlineData(@"foo/bar/", @"foo/bar")]
            [InlineData(@".\foo", @"foo")]
            [InlineData(@"./foo", @"foo")]
            [InlineData(@"foo\..\bar", @"foo/../bar")]
            [InlineData(@"foo/../bar", @"foo/../bar")]
            public void Should_Set_FilePath(string filePath, string expectedFilePath)
            {
                // Given
                var fixture = new IssueBuilderFixture();

                // When
                var issue = fixture.IssueBuilder.InFile(filePath, 10).Create();

                // Then
                issue.AffectedFileRelativePath.ToString().ShouldBe(expectedFilePath);
                issue.AffectedFileRelativePath.IsRelative.ShouldBe(true, "File path was not set as relative.");
            }

            [Theory]
            [InlineData(1)]
            [InlineData(int.MaxValue)]
            public void Should_Set_Line(int line)
            {
                // Given
                var fixture = new IssueBuilderFixture();

                // When
                var issue = fixture.IssueBuilder.InFile("foo", line).Create();

                // Then
                issue.Line.ShouldBe(line);
            }
        }

        public sealed class TheInFileLineColumnMethod
        {
            [Fact]
            public void Should_Throw_If_Line_Is_Negative()
            {
                // Given
                var fixture = new IssueBuilderFixture();

                // When
                var result = Record.Exception(() =>
                    fixture.IssueBuilder.InFile("foo", -1, 50));

                // Then
                result.IsArgumentOutOfRangeException("line");
            }

            [Fact]
            public void Should_Throw_If_Line_Is_Zero()
            {
                // Given
                var fixture = new IssueBuilderFixture();

                // When
                var result = Record.Exception(() =>
                    fixture.IssueBuilder.InFile("foo", 0, 50));

                // Then
                result.IsArgumentOutOfRangeException("line");
            }

            [Fact]
            public void Should_Throw_If_Column_Is_Negative()
            {
                // Given
                var fixture = new IssueBuilderFixture();

                // When
                var result = Record.Exception(() =>
                    fixture.IssueBuilder.InFile("foo", 100, -1));

                // Then
                result.IsArgumentOutOfRangeException("column");
            }

            [Fact]
            public void Should_Throw_If_Column_Is_Zero()
            {
                // Given
                var fixture = new IssueBuilderFixture();

                // When
                var result = Record.Exception(() =>
                    fixture.IssueBuilder.InFile("foo", 100, 0));

                // Then
                result.IsArgumentOutOfRangeException("column");
            }

            [Fact]
            public void Should_Handle_Column_Which_Is_Null()
            {
                // Given
                var fixture = new IssueBuilderFixture();

                // When
                var issue = fixture.IssueBuilder.InFile("foo", 100, null).Create();

                // Then
                issue.Column.ShouldBe(null);
            }

            [Theory]
            [InlineData(@"foo", @"foo")]
            [InlineData(@"foo\bar", @"foo/bar")]
            [InlineData(@"foo/bar", @"foo/bar")]
            [InlineData(@"foo\bar\", @"foo/bar")]
            [InlineData(@"foo/bar/", @"foo/bar")]
            [InlineData(@".\foo", @"foo")]
            [InlineData(@"./foo", @"foo")]
            [InlineData(@"foo\..\bar", @"foo/../bar")]
            [InlineData(@"foo/../bar", @"foo/../bar")]
            public void Should_Set_FilePath(string filePath, string expectedFilePath)
            {
                // Given
                var fixture = new IssueBuilderFixture();

                // When
                var issue = fixture.IssueBuilder.InFile(filePath, 10, 50).Create();

                // Then
                issue.AffectedFileRelativePath.ToString().ShouldBe(expectedFilePath);
                issue.AffectedFileRelativePath.IsRelative.ShouldBe(true, "File path was not set as relative.");
            }

            [Theory]
            [InlineData(1)]
            [InlineData(int.MaxValue)]
            public void Should_Set_Line(int line)
            {
                // Given
                var fixture = new IssueBuilderFixture();

                // When
                var issue = fixture.IssueBuilder.InFile("foo", line, 50).Create();

                // Then
                issue.Line.ShouldBe(line);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(int.MaxValue)]
            public void Should_Set_Column(int column)
            {
                // Given
                var fixture = new IssueBuilderFixture();

                // When
                var issue = fixture.IssueBuilder.InFile("foo", 100, column).Create();

                // Then
                issue.Column.ShouldBe(column);
            }
        }

        public sealed class TheWithPriorityMethod
        {
            [Fact]
            public void Should_Handle_Priority_Which_Is_Null()
            {
                // Given
                var fixture = new IssueBuilderFixture();
                int? priority = null;

                // When
                var issue = fixture.IssueBuilder.WithPriority(priority, "Foo").Create();

                // Then
                issue.Priority.ShouldBe(priority);
            }

            [Fact]
            public void Should_Handle_PriorityNames_Which_Are_Null()
            {
                // Given
                var fixture = new IssueBuilderFixture();
                string priorityName = null;

                // When
                var issue = fixture.IssueBuilder.WithPriority(0, priorityName).Create();

                // Then
                issue.PriorityName.ShouldBe(priorityName);
            }

            [Fact]
            public void Should_Handle_PriorityNames_Which_Are_Empty()
            {
                // Given
                var fixture = new IssueBuilderFixture();
                var priorityName = string.Empty;

                // When
                var issue = fixture.IssueBuilder.WithPriority(0, priorityName).Create();

                // Then
                issue.PriorityName.ShouldBe(priorityName);
            }

            [Fact]
            public void Should_Handle_PriorityNames_Which_Are_WhiteSpace()
            {
                // Given
                var fixture = new IssueBuilderFixture();
                var priorityName = " ";

                // When
                var issue = fixture.IssueBuilder.WithPriority(0, priorityName).Create();

                // Then
                issue.PriorityName.ShouldBe(priorityName);
            }

            [Theory]
            [InlineData(int.MinValue)]
            [InlineData(-1)]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(int.MaxValue)]
            public void Should_Set_Priority(int priority)
            {
                // Given
                var fixture = new IssueBuilderFixture();

                // When
                var issue = fixture.IssueBuilder.WithPriority(priority, "Foo").Create();

                // Then
                issue.Priority.ShouldBe(priority);
            }

            [Theory]
            [InlineData("Warning")]
            public void Should_Set_PriorityName(string priorityName)
            {
                // Given
                var fixture = new IssueBuilderFixture();

                // When
                var issue = fixture.IssueBuilder.WithPriority(0, priorityName).Create();

                // Then
                issue.PriorityName.ShouldBe(priorityName);
            }
        }

        public sealed class TheWithPriorityEnumMethod
        {
            [Theory]
            [InlineData(IssuePriority.Hint, 100, "Hint")]
            [InlineData(IssuePriority.Suggestion, 200, "Suggestion")]
            [InlineData(IssuePriority.Warning, 300, "Warning")]
            [InlineData(IssuePriority.Error, 400, "Error")]
            public void Should_Set_Priority(IssuePriority issuePriority, int expectedPriority, string expectedPriorityName)
            {
                // Given
                var fixture = new IssueBuilderFixture();

                // When
                var issue = fixture.IssueBuilder.WithPriority(issuePriority).Create();

                // Then
                issue.Priority.ShouldBe(expectedPriority);
                issue.PriorityName.ShouldBe(expectedPriorityName);
            }
        }

        public sealed class TheOfRuleMethod
        {
            [Fact]
            public void Should_Handle_Rules_Which_Are_Null()
            {
                // Given
                var fixture = new IssueBuilderFixture();
                string rule = null;

                // When
                var issue = fixture.IssueBuilder.OfRule(rule).Create();

                // Then
                issue.Rule.ShouldBe(rule);
            }

            [Fact]
            public void Should_Handle_Rules_Which_Are_Empty()
            {
                // Given
                var fixture = new IssueBuilderFixture();
                var rule = string.Empty;

                // When
                var issue = fixture.IssueBuilder.OfRule(rule).Create();

                // Then
                issue.Rule.ShouldBe(rule);
            }

            [Fact]
            public void Should_Handle_Rules_Which_Are_WhiteSpace()
            {
                // Given
                var fixture = new IssueBuilderFixture();
                var rule = " ";

                // When
                var issue = fixture.IssueBuilder.OfRule(rule).Create();

                // Then
                issue.Rule.ShouldBe(rule);
            }

            [Theory]
            [InlineData("rule")]
            public void Should_Set_Rule(string rule)
            {
                // Given
                var fixture = new IssueBuilderFixture();

                // When
                var issue = fixture.IssueBuilder.OfRule(rule).Create();

                // Then
                issue.Rule.ShouldBe(rule);
            }
        }

        public sealed class TheOfRuleWithUriMethod
        {
            [Fact]
            public void Should_Handle_Names_Which_Are_Null()
            {
                // Given
                var fixture = new IssueBuilderFixture();
                string rule = null;

                // When
                var issue = fixture.IssueBuilder.OfRule(rule, new Uri("https://google.com")).Create();

                // Then
                issue.Rule.ShouldBe(rule);
            }

            [Fact]
            public void Should_Handle_Name_Which_Are_Empty()
            {
                // Given
                var fixture = new IssueBuilderFixture();
                var rule = string.Empty;

                // When
                var issue = fixture.IssueBuilder.OfRule(rule, new Uri("https://google.com")).Create();

                // Then
                issue.Rule.ShouldBe(rule);
            }

            [Fact]
            public void Should_Handle_Names_Which_Are_WhiteSpace()
            {
                // Given
                var fixture = new IssueBuilderFixture();
                var rule = " ";

                // When
                var issue = fixture.IssueBuilder.OfRule(rule, new Uri("https://google.com")).Create();

                // Then
                issue.Rule.ShouldBe(rule);
            }

            [Fact]
            public void Should_Handle_Rule_Uri_Which_Are_Null()
            {
                // Given
                var fixture = new IssueBuilderFixture();

                // When
                var issue = fixture.IssueBuilder.OfRule("Rule", null).Create();

                // Then
                issue.RuleUrl.ShouldBe(null);
            }

            [Theory]
            [InlineData("rule")]
            public void Should_Set_Rule(string rule)
            {
                // Given
                var fixture = new IssueBuilderFixture();
                var ruleUri = "https://google.com/";

                // When
                var issue = fixture.IssueBuilder.OfRule(rule, new Uri(ruleUri)).Create();

                // Then
                issue.RuleUrl.ToString().ShouldBe(ruleUri);
            }

            [Theory]
            [InlineData("https://google.com/")]
            public void Should_Set_RuleUrl(string ruleUri)
            {
                // Given
                var fixture = new IssueBuilderFixture();

                // When
                var issue = fixture.IssueBuilder.OfRule("Rule", new Uri(ruleUri)).Create();

                // Then
                issue.RuleUrl.ToString().ShouldBe(ruleUri);
            }
        }

        public sealed class TheForRunMethod
        {
            [Fact]
            public void Should_Throw_If_Run_Is_Null()
            {
                // Given
                var fixture = new IssueBuilderFixture();
                string run = null;

                // When
                var result = Record.Exception(() =>
                    fixture.IssueBuilder.ForRun(run));

                // Then
                result.IsArgumentNullException("run");
            }

            [Fact]
            public void Should_Throw_If_Run_Is_Empty()
            {
                // Given
                var fixture = new IssueBuilderFixture();
                string run = string.Empty;

                // When
                var result = Record.Exception(() =>
                    fixture.IssueBuilder.ForRun(run));

                // Then
                result.IsArgumentException("run");
            }

            [Fact]
            public void Should_Throw_If_Run_Is_WhiteSpace()
            {
                // Given
                var fixture = new IssueBuilderFixture();
                string run = " ";

                // When
                var result = Record.Exception(() =>
                    fixture.IssueBuilder.ForRun(run));

                // Then
                result.IsArgumentException("run");
            }

            [Theory]
            [InlineData("run")]
            public void Should_Set_Run(string run)
            {
                // Given
                var fixture = new IssueBuilderFixture();

                // When
                var issue = fixture.IssueBuilder.ForRun(run).Create();

                // Then
                issue.Run.ToString().ShouldBe(run);
            }
        }
    }
}