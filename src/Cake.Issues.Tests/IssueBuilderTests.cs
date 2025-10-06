namespace Cake.Issues.Tests;

public sealed class IssueBuilderTests
{
    public sealed class TheNewIssueMethodWithMessageAsIdentifier
    {
        [Fact]
        public void Should_Throw_If_Message_Is_Null()
        {
            // Given
            const string message = null;
            const string providerType = "ProviderType";
            const string providerName = "ProviderName";

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
            const string providerType = "ProviderType";
            const string providerName = "ProviderName";

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
            const string message = " ";
            const string providerType = "ProviderType";
            const string providerName = "ProviderName";

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
            const string message = "Message";
            const string providerType = null;
            const string providerName = "ProviderName";

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
            const string message = "Message";
            var providerType = string.Empty;
            const string providerName = "ProviderName";

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
            const string message = "Message";
            const string providerType = " ";
            const string providerName = "ProviderName";

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
            const string message = "Message";
            const string providerType = "ProviderType";
            const string providerName = null;

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
            const string message = "Message";
            const string providerType = "ProviderType";
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
            const string message = "Message";
            const string providerType = "ProviderType";
            const string providerName = " ";

            // When
            var result = Record.Exception(() =>
                IssueBuilder.NewIssue(message, providerType, providerName));

            // Then
            result.IsArgumentOutOfRangeException("providerName");
        }

        [Fact]
        public void Should_Set_Identifier()
        {
            // Given
            const string message = "Message";
            const string providerType = "ProviderType";
            const string providerName = "ProviderName";

            // When
            var result =
                IssueBuilder
                    .NewIssue(message, providerType, providerName)
                    .Create();

            // Then
            result.Identifier.ShouldBe(message);
        }

        [Fact]
        public void Should_Set_Message()
        {
            // Given
            const string message = "Message";
            const string providerType = "ProviderType";
            const string providerName = "ProviderName";

            // When
            var result =
                IssueBuilder
                    .NewIssue(message, providerType, providerName)
                    .Create();

            // Then
            result.MessageText.ShouldBe(message);
        }

        [Fact]
        public void Should_Set_ProviderType()
        {
            // Given
            const string message = "Message";
            const string providerType = "ProviderType";
            const string providerName = "ProviderName";

            // When
            var result =
                IssueBuilder
                    .NewIssue(message, providerType, providerName)
                    .Create();

            // Then
            result.ProviderType.ShouldBe(providerType);
        }

        [Fact]
        public void Should_Set_ProviderName()
        {
            // Given
            const string message = "Message";
            const string providerType = "ProviderType";
            const string providerName = "ProviderName";

            // When
            var result =
                IssueBuilder
                    .NewIssue(message, providerType, providerName)
                    .Create();

            // Then
            result.ProviderName.ShouldBe(providerName);
        }
    }

    public sealed class TheNewIssueMethod
    {
        [Fact]
        public void Should_Throw_If_Identifier_Is_Null()
        {
            // Given
            const string identifier = null;
            const string message = "Message";
            const string providerType = "ProviderType";
            const string providerName = "ProviderName";

            // When
            var result = Record.Exception(() =>
                IssueBuilder.NewIssue(identifier, message, providerType, providerName));

            // Then
            result.IsArgumentNullException("identifier");
        }

        [Fact]
        public void Should_Throw_If_Identifier_Is_Empty()
        {
            // Given
            var identifier = string.Empty;
            const string message = "Message";
            const string providerType = "ProviderType";
            const string providerName = "ProviderName";

            // When
            var result = Record.Exception(() =>
                IssueBuilder.NewIssue(identifier, message, providerType, providerName));

            // Then
            result.IsArgumentOutOfRangeException("identifier");
        }

        [Fact]
        public void Should_Throw_If_Identifier_Is_WhiteSpace()
        {
            // Given
            const string identifier = " ";
            const string message = "Message";
            const string providerType = "ProviderType";
            const string providerName = "ProviderName";

            // When
            var result = Record.Exception(() =>
                IssueBuilder.NewIssue(identifier, message, providerType, providerName));

            // Then
            result.IsArgumentOutOfRangeException("identifier");
        }

        [Fact]
        public void Should_Throw_If_Message_Is_Null()
        {
            // Given
            const string identifier = "Identifier";
            const string message = null;
            const string providerType = "ProviderType";
            const string providerName = "ProviderName";

            // When
            var result = Record.Exception(() =>
                IssueBuilder.NewIssue(identifier, message, providerType, providerName));

            // Then
            result.IsArgumentNullException("message");
        }

        [Fact]
        public void Should_Throw_If_Message_Is_Empty()
        {
            // Given
            const string identifier = "Identifier";
            var message = string.Empty;
            const string providerType = "ProviderType";
            const string providerName = "ProviderName";

            // When
            var result = Record.Exception(() =>
                IssueBuilder.NewIssue(identifier, message, providerType, providerName));

            // Then
            result.IsArgumentOutOfRangeException("message");
        }

        [Fact]
        public void Should_Throw_If_Message_Is_WhiteSpace()
        {
            // Given
            const string identifier = "Identifier";
            const string message = " ";
            const string providerType = "ProviderType";
            const string providerName = "ProviderName";

            // When
            var result = Record.Exception(() =>
                IssueBuilder.NewIssue(identifier, message, providerType, providerName));

            // Then
            result.IsArgumentOutOfRangeException("message");
        }

        [Fact]
        public void Should_Throw_If_ProviderType_Is_Null()
        {
            // Given
            const string identifier = "Identifier";
            const string message = "Message";
            const string providerType = null;
            const string providerName = "ProviderName";

            // When
            var result = Record.Exception(() =>
                IssueBuilder.NewIssue(identifier, message, providerType, providerName));

            // Then
            result.IsArgumentNullException("providerType");
        }

        [Fact]
        public void Should_Throw_If_ProviderType_Is_Empty()
        {
            // Given
            const string identifier = "Identifier";
            const string message = "Message";
            var providerType = string.Empty;
            const string providerName = "ProviderName";

            // When
            var result = Record.Exception(() =>
                IssueBuilder.NewIssue(identifier, message, providerType, providerName));

            // Then
            result.IsArgumentOutOfRangeException("providerType");
        }

        [Fact]
        public void Should_Throw_If_ProviderType_Is_WhiteSpace()
        {
            // Given
            const string identifier = "Identifier";
            const string message = "Message";
            const string providerType = " ";
            const string providerName = "ProviderName";

            // When
            var result = Record.Exception(() =>
                IssueBuilder.NewIssue(identifier, message, providerType, providerName));

            // Then
            result.IsArgumentOutOfRangeException("providerType");
        }

        [Fact]
        public void Should_Throw_If_ProviderName_Is_Null()
        {
            // Given
            const string identifier = "Identifier";
            const string message = "Message";
            const string providerType = "ProviderType";
            const string providerName = null;

            // When
            var result = Record.Exception(() =>
                IssueBuilder.NewIssue(identifier, message, providerType, providerName));

            // Then
            result.IsArgumentNullException("providerName");
        }

        [Fact]
        public void Should_Throw_If_ProviderName_Is_Empty()
        {
            // Given
            const string identifier = "Identifier";
            const string message = "Message";
            const string providerType = "ProviderType";
            var providerName = string.Empty;

            // When
            var result = Record.Exception(() =>
                IssueBuilder.NewIssue(identifier, message, providerType, providerName));

            // Then
            result.IsArgumentOutOfRangeException("providerName");
        }

        [Fact]
        public void Should_Throw_If_ProviderName_Is_WhiteSpace()
        {
            // Given
            const string identifier = "Identifier";
            const string message = "Message";
            const string providerType = "ProviderType";
            const string providerName = " ";

            // When
            var result = Record.Exception(() =>
                IssueBuilder.NewIssue(identifier, message, providerType, providerName));

            // Then
            result.IsArgumentOutOfRangeException("providerName");
        }

        [Fact]
        public void Should_Set_Identifier()
        {
            // Given
            const string identifier = "Identifier";
            const string message = "Message";
            const string providerType = "ProviderType";
            const string providerName = "ProviderName";

            // When
            var result =
                IssueBuilder
                    .NewIssue(identifier, message, providerType, providerName)
                    .Create();

            // Then
            result.Identifier.ShouldBe(identifier);
        }

        [Fact]
        public void Should_Set_Message()
        {
            // Given
            const string identifier = "Identifier";
            const string message = "Message";
            const string providerType = "ProviderType";
            const string providerName = "ProviderName";

            // When
            var result =
                IssueBuilder
                    .NewIssue(identifier, message, providerType, providerName)
                    .Create();

            // Then
            result.MessageText.ShouldBe(message);
        }

        [Fact]
        public void Should_Set_ProviderType()
        {
            // Given
            const string identifier = "Identifier";
            const string message = "Message";
            const string providerType = "ProviderType";
            var providerName = "ProviderName";

            // When
            var result =
                IssueBuilder
                    .NewIssue(identifier, message, providerType, providerName)
                    .Create();

            // Then
            result.ProviderType.ShouldBe(providerType);
        }

        [Fact]
        public void Should_Set_ProviderName()
        {
            // Given
            const string identifier = "Identifier";
            const string message = "Message";
            const string providerType = "ProviderType";
            const string providerName = "ProviderName";

            // When
            var result =
                IssueBuilder
                    .NewIssue(identifier, message, providerType, providerName)
                    .Create();

            // Then
            result.ProviderName.ShouldBe(providerName);
        }
    }

    public sealed class TheNewIssueOfTMethodWithMessageAsIdentifier
    {
        [Fact]
        public void Should_Throw_If_Message_Is_Null()
        {
            // Given
            const string message = null;
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
            const string message = " ";
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
            const string message = "Message";
            const IIssueProvider issueProvider = null;

            // When
            var result = Record.Exception(() =>
                IssueBuilder.NewIssue(message, issueProvider));

            // Then
            result.IsArgumentNullException("issueProvider");
        }

        [Fact]
        public void Should_Set_Identifier()
        {
            // Given
            const string message = "Message";
            var issueProvider = new FakeIssueProvider(new FakeLog());

            // When
            var result =
                IssueBuilder
                    .NewIssue(message, issueProvider)
                    .Create();

            // Then
            result.Identifier.ShouldBe(message);
        }

        [Fact]
        public void Should_Set_Message()
        {
            // Given
            const string message = "Message";
            var issueProvider = new FakeIssueProvider(new FakeLog());

            // When
            var result =
                IssueBuilder
                    .NewIssue(message, issueProvider)
                    .Create();

            // Then
            result.MessageText.ShouldBe(message);
        }

        [Fact]
        public void Should_Set_ProviderType()
        {
            // Given
            const string message = "Message";
            var issueProvider = new FakeIssueProvider(new FakeLog());

            // When
            var result =
                IssueBuilder
                    .NewIssue(message, issueProvider)
                    .Create();

            // Then
            result.ProviderType.ShouldBe(issueProvider.ProviderType);
        }

        [Fact]
        public void Should_Set_ProviderName()
        {
            // Given
            const string message = "Message";
            var issueProvider = new FakeIssueProvider(new FakeLog());

            // When
            var result =
                IssueBuilder
                    .NewIssue(message, issueProvider)
                    .Create();

            // Then
            result.ProviderName.ShouldBe(issueProvider.ProviderName);
        }
    }

    public sealed class TheNewIssueOfTMethod
    {
        [Fact]
        public void Should_Throw_If_Identifier_Is_Null()
        {
            // Given
            const string identifier = null;
            const string message = "Message";
            var issueProvider = new FakeIssueProvider(new FakeLog());

            // When
            var result = Record.Exception(() =>
                IssueBuilder.NewIssue(identifier, message, issueProvider));

            // Then
            result.IsArgumentNullException("identifier");
        }

        [Fact]
        public void Should_Throw_If_Identifier_Is_Empty()
        {
            // Given
            var identifier = string.Empty;
            const string message = "Message";
            var issueProvider = new FakeIssueProvider(new FakeLog());

            // When
            var result = Record.Exception(() =>
                IssueBuilder.NewIssue(identifier, message, issueProvider));

            // Then
            result.IsArgumentOutOfRangeException("identifier");
        }

        [Fact]
        public void Should_Throw_If_Identifier_Is_WhiteSpace()
        {
            // Given
            const string identifier = " ";
            const string message = "Message";
            var issueProvider = new FakeIssueProvider(new FakeLog());

            // When
            var result = Record.Exception(() =>
                IssueBuilder.NewIssue(identifier, message, issueProvider));

            // Then
            result.IsArgumentOutOfRangeException("identifier");
        }

        [Fact]
        public void Should_Throw_If_Message_Is_Null()
        {
            // Given
            const string identifier = "Identifier";
            const string message = null;
            var issueProvider = new FakeIssueProvider(new FakeLog());

            // When
            var result = Record.Exception(() =>
                IssueBuilder.NewIssue(identifier, message, issueProvider));

            // Then
            result.IsArgumentNullException("message");
        }

        [Fact]
        public void Should_Throw_If_Message_Is_Empty()
        {
            // Given
            const string identifier = "Identifier";
            var message = string.Empty;
            var issueProvider = new FakeIssueProvider(new FakeLog());

            // When
            var result = Record.Exception(() =>
                IssueBuilder.NewIssue(identifier, message, issueProvider));

            // Then
            result.IsArgumentOutOfRangeException("message");
        }

        [Fact]
        public void Should_Throw_If_Message_Is_WhiteSpace()
        {
            // Given
            const string identifier = "Identifier";
            const string message = " ";
            var issueProvider = new FakeIssueProvider(new FakeLog());

            // When
            var result = Record.Exception(() =>
                IssueBuilder.NewIssue(identifier, message, issueProvider));

            // Then
            result.IsArgumentOutOfRangeException("message");
        }

        [Fact]
        public void Should_Throw_If_IssueProvider_Is_Null()
        {
            // Given
            const string identifier = "Identifier";
            const string message = "Message";
            const IIssueProvider issueProvider = null;

            // When
            var result = Record.Exception(() =>
                IssueBuilder.NewIssue(identifier, message, issueProvider));

            // Then
            result.IsArgumentNullException("issueProvider");
        }

        [Fact]
        public void Should_Set_Identifier()
        {
            // Given
            const string identifier = "Identifier";
            const string message = "Message";
            var issueProvider = new FakeIssueProvider(new FakeLog());

            // When
            var result =
                IssueBuilder
                    .NewIssue(identifier, message, issueProvider)
                    .Create();

            // Then
            result.Identifier.ShouldBe(identifier);
        }

        [Fact]
        public void Should_Set_Message()
        {
            // Given
            const string identifier = "Identifier";
            const string message = "Message";
            var issueProvider = new FakeIssueProvider(new FakeLog());

            // When
            var result =
                IssueBuilder
                    .NewIssue(identifier, message, issueProvider)
                    .Create();

            // Then
            result.MessageText.ShouldBe(message);
        }

        [Fact]
        public void Should_Set_ProviderType()
        {
            // Given
            const string identifier = "Identifier";
            const string message = "Message";
            var issueProvider = new FakeIssueProvider(new FakeLog());

            // When
            var result =
                IssueBuilder
                    .NewIssue(identifier, message, issueProvider)
                    .Create();

            // Then
            result.ProviderType.ShouldBe(issueProvider.ProviderType);
        }

        [Fact]
        public void Should_Set_ProviderName()
        {
            // Given
            const string identifier = "Identifier";
            const string message = "Message";
            var issueProvider = new FakeIssueProvider(new FakeLog());

            // When
            var result =
                IssueBuilder
                    .NewIssue(identifier, message, issueProvider)
                    .Create();

            // Then
            result.ProviderName.ShouldBe(issueProvider.ProviderName);
        }
    }

    public sealed class TheInProjectFileMethod
    {
        [Fact]
        public void Should_Handle_Project_Paths_Which_Are_Null()
        {
            // Given
            var fixture = new IssueBuilderFixture();
            const string projectPath = null;

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
            const string projectPath = " ";

            // When
            var issue = fixture.IssueBuilder.InProjectFile(projectPath).Create();

            // Then
            issue.ProjectFileRelativePath.ShouldBe(null);
        }

        [Theory]
        [InlineData("src/project.csproj")]
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
            const string projectName = null;

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
            const string projectName = " ";

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
            const string projectPath = null;
            const string projectName = "foo";

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
            const string projectName = "foo";

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
            const string projectPath = " ";
            const string projectName = "foo";

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
            const string projectName = null;
            const string projectPath = "foo";

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
            const string projectPath = "foo";

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
            const string projectName = " ";
            const string projectPath = "foo";

            // When
            var issue = fixture.IssueBuilder.InProject(projectPath, projectName).Create();

            // Then
            issue.ProjectName.ShouldBe(projectName);
        }

        [Theory]
        [InlineData("src/project.csproj")]
        public void Should_Set_ProjectFileRelativePath(string projectPath)
        {
            // Given
            var fixture = new IssueBuilderFixture();
            const string projectName = "foo";

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
            const string projectPath = "foo";

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
        [InlineData("foo", "foo")]
        [InlineData(@"foo\bar", "foo/bar")]
        [InlineData("foo/bar", "foo/bar")]
        [InlineData(@"foo\bar\", "foo/bar")]
        [InlineData("foo/bar/", "foo/bar")]
        [InlineData(@".\foo", "foo")]
        [InlineData("./foo", "foo")]
        [InlineData(@"foo\..\bar", "foo/../bar")]
        [InlineData("foo/../bar", "foo/../bar")]
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
        [InlineData("foo", "foo")]
        [InlineData(@"foo\bar", "foo/bar")]
        [InlineData("foo/bar", "foo/bar")]
        [InlineData(@"foo\bar\", "foo/bar")]
        [InlineData("foo/bar/", "foo/bar")]
        [InlineData(@".\foo", "foo")]
        [InlineData("./foo", "foo")]
        [InlineData(@"foo\..\bar", "foo/../bar")]
        [InlineData("foo/../bar", "foo/../bar")]
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
        public void Should_Handle_Line_Which_Is_Null()
        {
            // Given
            var fixture = new IssueBuilderFixture();

            // When
            var issue = fixture.IssueBuilder.InFile("foo", null, null).Create();

            // Then
            issue.Line.ShouldBe(null);
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
        [InlineData("foo", "foo")]
        [InlineData(@"foo\bar", "foo/bar")]
        [InlineData("foo/bar", "foo/bar")]
        [InlineData(@"foo\bar\", "foo/bar")]
        [InlineData("foo/bar/", "foo/bar")]
        [InlineData(@".\foo", "foo")]
        [InlineData("./foo", "foo")]
        [InlineData(@"foo\..\bar", "foo/../bar")]
        [InlineData("foo/../bar", "foo/../bar")]
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

    public sealed class TheInFileLineRangeColumnRangeMethod
    {
        [Fact]
        public void Should_Throw_If_StartLine_Is_Negative()
        {
            // Given
            var fixture = new IssueBuilderFixture();

            // When
            var result = Record.Exception(() =>
                fixture.IssueBuilder.InFile("foo", -1, 50, 1, 10));

            // Then
            result.IsArgumentOutOfRangeException("startLine");
        }

        [Fact]
        public void Should_Throw_If_StartLine_Is_Zero()
        {
            // Given
            var fixture = new IssueBuilderFixture();

            // When
            var result = Record.Exception(() =>
                fixture.IssueBuilder.InFile("foo", 0, 50, 1, 10));

            // Then
            result.IsArgumentOutOfRangeException("startLine");
        }

        [Fact]
        public void Should_Throw_If_EndLine_Is_Negative()
        {
            // Given
            var fixture = new IssueBuilderFixture();

            // When
            var result = Record.Exception(() =>
                fixture.IssueBuilder.InFile("foo", 5, -1, 1, 10));

            // Then
            result.IsArgumentOutOfRangeException("endLine");
        }

        [Fact]
        public void Should_Throw_If_EndLine_Is_Zero()
        {
            // Given
            var fixture = new IssueBuilderFixture();

            // When
            var result = Record.Exception(() =>
                fixture.IssueBuilder.InFile("foo", 5, 0, 1, 10));

            // Then
            result.IsArgumentOutOfRangeException("endLine");
        }

        [Fact]
        public void Should_Throw_If_StartColumn_Is_Negative()
        {
            // Given
            var fixture = new IssueBuilderFixture();

            // When
            var result = Record.Exception(() =>
                fixture.IssueBuilder.InFile("foo", 5, 50, -1, 10));

            // Then
            result.IsArgumentOutOfRangeException("startColumn");
        }

        [Fact]
        public void Should_Throw_If_StartColumn_Is_Zero()
        {
            // Given
            var fixture = new IssueBuilderFixture();

            // When
            var result = Record.Exception(() =>
                fixture.IssueBuilder.InFile("foo", 5, 50, 0, 10));

            // Then
            result.IsArgumentOutOfRangeException("startColumn");
        }

        [Fact]
        public void Should_Throw_If_EndColumn_Is_Negative()
        {
            // Given
            var fixture = new IssueBuilderFixture();

            // When
            var result = Record.Exception(() =>
                fixture.IssueBuilder.InFile("foo", 5, 50, 1, -1));

            // Then
            result.IsArgumentOutOfRangeException("endColumn");
        }

        [Fact]
        public void Should_Throw_If_EndColumn_Is_Zero()
        {
            // Given
            var fixture = new IssueBuilderFixture();

            // When
            var result = Record.Exception(() =>
                fixture.IssueBuilder.InFile("foo", 5, 50, 1, 0));

            // Then
            result.IsArgumentOutOfRangeException("endColumn");
        }

        [Fact]
        public void Should_Handle_StartLine_Which_Is_Null()
        {
            // Given
            var fixture = new IssueBuilderFixture();

            // When
            var issue = fixture.IssueBuilder.InFile("foo", null, null, null, null).Create();

            // Then
            issue.Line.ShouldBe(null);
        }

        [Fact]
        public void Should_Handle_EndLine_Which_Is_Null()
        {
            // Given
            var fixture = new IssueBuilderFixture();

            // When
            var issue = fixture.IssueBuilder.InFile("foo", 5, null, null, null).Create();

            // Then
            issue.EndLine.ShouldBe(null);
        }

        [Fact]
        public void Should_Handle_StartColumn_Which_Is_Null()
        {
            // Given
            var fixture = new IssueBuilderFixture();

            // When
            var issue = fixture.IssueBuilder.InFile("foo", 5, 50, null, null).Create();

            // Then
            issue.Column.ShouldBe(null);
        }

        [Fact]
        public void Should_Handle_EndColumn_Which_Is_Null()
        {
            // Given
            var fixture = new IssueBuilderFixture();

            // When
            var issue = fixture.IssueBuilder.InFile("foo", 5, 50, 1, null).Create();

            // Then
            issue.EndColumn.ShouldBe(null);
        }

        [Theory]
        [InlineData("foo", "foo")]
        [InlineData(@"foo\bar", "foo/bar")]
        [InlineData("foo/bar", "foo/bar")]
        [InlineData(@"foo\bar\", "foo/bar")]
        [InlineData("foo/bar/", "foo/bar")]
        [InlineData(@".\foo", "foo")]
        [InlineData("./foo", "foo")]
        [InlineData(@"foo\..\bar", "foo/../bar")]
        [InlineData("foo/../bar", "foo/../bar")]
        public void Should_Set_FilePath(string filePath, string expectedFilePath)
        {
            // Given
            var fixture = new IssueBuilderFixture();

            // When
            var issue = fixture.IssueBuilder.InFile(filePath, 5, 50, 1, 10).Create();

            // Then
            issue.AffectedFileRelativePath.ToString().ShouldBe(expectedFilePath);
            issue.AffectedFileRelativePath.IsRelative.ShouldBe(true, "File path was not set as relative.");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(int.MaxValue)]
        public void Should_Set_StartLine(int startLine)
        {
            // Given
            var fixture = new IssueBuilderFixture();

            // When
            var issue = fixture.IssueBuilder.InFile("foo", startLine, null, 1, 10).Create();

            // Then
            issue.Line.ShouldBe(startLine);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(int.MaxValue)]
        public void Should_Set_EndLine(int endLine)
        {
            // Given
            var fixture = new IssueBuilderFixture();

            // When
            var issue = fixture.IssueBuilder.InFile("foo", 1, endLine, 1, 10).Create();

            // Then
            issue.EndLine.ShouldBe(endLine);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(int.MaxValue)]
        public void Should_Set_StartColumn(int startColumn)
        {
            // Given
            var fixture = new IssueBuilderFixture();

            // When
            var issue = fixture.IssueBuilder.InFile("foo", 5, 50, startColumn, null).Create();

            // Then
            issue.Column.ShouldBe(startColumn);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(int.MaxValue)]
        public void Should_Set_EndColumn(int endColumn)
        {
            // Given
            var fixture = new IssueBuilderFixture();

            // When
            var issue = fixture.IssueBuilder.InFile("foo", 5, 50, 1, endColumn).Create();

            // Then
            issue.EndColumn.ShouldBe(endColumn);
        }
    }

    public sealed class TheWithFileLinkMethod
    {
        [Fact]
        public void Should_Throw_If_FileLink_Is_Null()
        {
            // Given
            var fixture = new IssueBuilderFixture();
            const Uri fileLink = null;

            // When
            var result = Record.Exception(() =>
                fixture.IssueBuilder.WithFileLink(fileLink));

            // Then
            result.IsArgumentNullException("fileLink");
        }

        [Theory]
        [InlineData("https://google.com/")]
        public void Should_Set_FileLink(string fileLink)
        {
            // Given
            var fixture = new IssueBuilderFixture();

            // When
            var issue = fixture.IssueBuilder.WithFileLink(new Uri(fileLink)).Create();

            // Then
            issue.FileLink.ToString().ShouldBe(fileLink);
        }
    }

    public sealed class TheWithFileLinkSettingsMethod
    {
        [Fact]
        public void Should_Throw_If_FileLinkSettings_Are_Null()
        {
            // Given
            var fixture = new IssueBuilderFixture();
            const FileLinkSettings fileLinkSettings = null;

            // When
            var result = Record.Exception(() =>
                fixture.IssueBuilder.WithFileLinkSettings(fileLinkSettings));

            // Then
            result.IsArgumentNullException("fileLinkSettings");
        }

        [Fact]
        public void Should_Resolve_FileLink_When_FileLinkSettings_Were_Provided()
        {
            // Given
            var fixture = new IssueBuilderFixture();
            var fileLinkSettings =
                FileLinkSettings
                    .ForAzureDevOps(new Uri("https://test.com/tfs/myrepo"))
                    .Branch("dev");

            // When
            var issue =
                fixture.IssueBuilder
                    .InFile("fooPath", 10, 20, 1, 10)
                    .WithFileLinkSettings(fileLinkSettings).Create();

            // Then
            issue.FileLink.ShouldBe(new Uri("https://test.com/tfs/myrepo?path=/fooPath&version=GBdev&line=10&lineEnd=20&lineStartColumn=1&lineEndColumn=10"));
        }

        [Fact]
        public void Should_Use_FileLink_When_Both_FileLink_And_FileLinkSettings_Were_Provided()
        {
            // Given
            var fixture = new IssueBuilderFixture();
            var fileLinkSettings =
                FileLinkSettings
                    .ForAzureDevOps(new Uri("https://test.com/tfs/myrepo"))
                    .Branch("dev");

            // When
            var issue =
                fixture.IssueBuilder
                    .InFile("fooPath", 10, 20, 1, 10)
                    .WithFileLink(new Uri("https://this-will-win.com/"))
                    .WithFileLinkSettings(fileLinkSettings)
                    .Create();

            // Then
            issue.FileLink.ShouldBe(new Uri("https://this-will-win.com/"));
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
            const string priorityName = null;

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

    public sealed class TheOfRuleWithRuleIdMethod
    {
        [Fact]
        public void Should_Handle_RulesIds_Which_Are_Null()
        {
            // Given
            var fixture = new IssueBuilderFixture();
            const string ruleId = null;

            // When
            var issue = fixture.IssueBuilder.OfRule(ruleId).Create();

            // Then
            issue.RuleId.ShouldBe(ruleId);
        }

        [Fact]
        public void Should_Handle_RulesIds_Which_Are_Empty()
        {
            // Given
            var fixture = new IssueBuilderFixture();
            var ruleId = string.Empty;

            // When
            var issue = fixture.IssueBuilder.OfRule(ruleId).Create();

            // Then
            issue.RuleId.ShouldBe(ruleId);
        }

        [Fact]
        public void Should_Handle_RulesIds_Which_Are_WhiteSpace()
        {
            // Given
            var fixture = new IssueBuilderFixture();
            const string ruleId = " ";

            // When
            var issue = fixture.IssueBuilder.OfRule(ruleId).Create();

            // Then
            issue.RuleId.ShouldBe(ruleId);
        }

        [Theory]
        [InlineData("rule")]
        public void Should_Set_RuleId(string ruleId)
        {
            // Given
            var fixture = new IssueBuilderFixture();

            // When
            var issue = fixture.IssueBuilder.OfRule(ruleId).Create();

            // Then
            issue.RuleId.ShouldBe(ruleId);
        }
    }

    public sealed class TheOfRuleWithRuleIdAndNameMethod
    {
        [Fact]
        public void Should_Handle_RuleIds_Which_Are_Null()
        {
            // Given
            var fixture = new IssueBuilderFixture();
            const string ruleId = null;

            // When
            var issue = fixture.IssueBuilder.OfRule(ruleId, "Some Rule").Create();

            // Then
            issue.RuleId.ShouldBe(ruleId);
        }

        [Fact]
        public void Should_Handle_RuleIds_Which_Are_Empty()
        {
            // Given
            var fixture = new IssueBuilderFixture();
            var ruleId = string.Empty;

            // When
            var issue = fixture.IssueBuilder.OfRule(ruleId, "Some Rule").Create();

            // Then
            issue.RuleId.ShouldBe(ruleId);
        }

        [Fact]
        public void Should_Handle_RuleIds_Which_Are_WhiteSpace()
        {
            // Given
            var fixture = new IssueBuilderFixture();
            const string ruleId = " ";

            // When
            var issue = fixture.IssueBuilder.OfRule(ruleId, "Some Rule").Create();

            // Then
            issue.RuleId.ShouldBe(ruleId);
        }

        [Fact]
        public void Should_Handle_RuleNames_Which_Are_Null()
        {
            // Given
            var fixture = new IssueBuilderFixture();
            const string ruleName = null;

            // When
            var issue = fixture.IssueBuilder.OfRule("RuleId", ruleName).Create();

            // Then
            issue.RuleName.ShouldBe(ruleName);
        }

        [Fact]
        public void Should_Handle_RuleNames_Which_Are_Empty()
        {
            // Given
            var fixture = new IssueBuilderFixture();
            var ruleName = string.Empty;

            // When
            var issue = fixture.IssueBuilder.OfRule("RuleId", ruleName).Create();

            // Then
            issue.RuleName.ShouldBe(ruleName);
        }

        [Fact]
        public void Should_Handle_RuleNames_Which_Are_WhiteSpace()
        {
            // Given
            var fixture = new IssueBuilderFixture();
            const string ruleName = " ";

            // When
            var issue = fixture.IssueBuilder.OfRule("RuleId", ruleName).Create();

            // Then
            issue.RuleName.ShouldBe(ruleName);
        }

        [Theory]
        [InlineData("ruleId")]
        public void Should_Set_Rule(string ruleId)
        {
            // Given
            var fixture = new IssueBuilderFixture();
            const string ruleName = "Some Rule";

            // When
            var issue = fixture.IssueBuilder.OfRule(ruleId, ruleName).Create();

            // Then
            issue.RuleId.ShouldBe(ruleId);
        }

        [Theory]
        [InlineData("Rule Name")]
        public void Should_Set_RuleName(string ruleName)
        {
            // Given
            var fixture = new IssueBuilderFixture();

            // When
            var issue = fixture.IssueBuilder.OfRule("Rule", ruleName).Create();

            // Then
            issue.RuleName.ShouldBe(ruleName);
        }
    }

    public sealed class TheOfRuleWithRuleIdAndUriMethod
    {
        [Fact]
        public void Should_Handle_RuleIds_Which_Are_Null()
        {
            // Given
            var fixture = new IssueBuilderFixture();
            const string ruleId = null;

            // When
            var issue = fixture.IssueBuilder.OfRule(ruleId, new Uri("https://google.com")).Create();

            // Then
            issue.RuleId.ShouldBe(ruleId);
        }

        [Fact]
        public void Should_Handle_RuleIds_Which_Are_Empty()
        {
            // Given
            var fixture = new IssueBuilderFixture();
            var ruleIds = string.Empty;

            // When
            var issue = fixture.IssueBuilder.OfRule(ruleIds, new Uri("https://google.com")).Create();

            // Then
            issue.RuleId.ShouldBe(ruleIds);
        }

        [Fact]
        public void Should_Handle_RuleIds_Which_Are_WhiteSpace()
        {
            // Given
            var fixture = new IssueBuilderFixture();
            const string ruleIds = " ";

            // When
            var issue = fixture.IssueBuilder.OfRule(ruleIds, new Uri("https://google.com")).Create();

            // Then
            issue.RuleId.ShouldBe(ruleIds);
        }

        [Fact]
        public void Should_Handle_Rule_Uri_Which_Are_Null()
        {
            // Given
            var fixture = new IssueBuilderFixture();

            // When
            var issue = fixture.IssueBuilder.OfRule("Rule", (Uri)null).Create();

            // Then
            issue.RuleUrl.ShouldBe(null);
        }

        [Theory]
        [InlineData("ruleId")]
        public void Should_Set_Rule(string ruleId)
        {
            // Given
            var fixture = new IssueBuilderFixture();
            const string ruleUri = "https://google.com/";

            // When
            var issue = fixture.IssueBuilder.OfRule(ruleId, new Uri(ruleUri)).Create();

            // Then
            issue.RuleId.ShouldBe(ruleId);
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

    public sealed class TheOfRuleWithRuleIdAndNameAndUriMethod
    {
        [Fact]
        public void Should_Handle_RuleIds_Which_Are_Null()
        {
            // Given
            var fixture = new IssueBuilderFixture();
            const string ruleId = null;

            // When
            var issue = fixture.IssueBuilder.OfRule(ruleId, "Some Rule", new Uri("https://google.com")).Create();

            // Then
            issue.RuleId.ShouldBe(ruleId);
        }

        [Fact]
        public void Should_Handle_RuleIds_Which_Are_Empty()
        {
            // Given
            var fixture = new IssueBuilderFixture();
            var ruleId = string.Empty;

            // When
            var issue = fixture.IssueBuilder.OfRule(ruleId, "Some Rule", new Uri("https://google.com")).Create();

            // Then
            issue.RuleId.ShouldBe(ruleId);
        }

        [Fact]
        public void Should_Handle_RuleIds_Which_Are_WhiteSpace()
        {
            // Given
            var fixture = new IssueBuilderFixture();
            const string ruleId = " ";

            // When
            var issue = fixture.IssueBuilder.OfRule(ruleId, "Some Rule", new Uri("https://google.com")).Create();

            // Then
            issue.RuleId.ShouldBe(ruleId);
        }

        [Fact]
        public void Should_Handle_RuleNames_Which_Are_Null()
        {
            // Given
            var fixture = new IssueBuilderFixture();
            const string ruleName = null;

            // When
            var issue = fixture.IssueBuilder.OfRule("RuleId", ruleName, new Uri("https://google.com")).Create();

            // Then
            issue.RuleName.ShouldBe(ruleName);
        }

        [Fact]
        public void Should_Handle_RuleNames_Which_Are_Empty()
        {
            // Given
            var fixture = new IssueBuilderFixture();
            var ruleName = string.Empty;

            // When
            var issue = fixture.IssueBuilder.OfRule("RuleId", ruleName, new Uri("https://google.com")).Create();

            // Then
            issue.RuleName.ShouldBe(ruleName);
        }

        [Fact]
        public void Should_Handle_RuleNames_Which_Are_WhiteSpace()
        {
            // Given
            var fixture = new IssueBuilderFixture();
            const string ruleName = " ";

            // When
            var issue = fixture.IssueBuilder.OfRule("RuleId", ruleName, new Uri("https://google.com")).Create();

            // Then
            issue.RuleName.ShouldBe(ruleName);
        }

        [Fact]
        public void Should_Handle_Rule_Uri_Which_Are_Null()
        {
            // Given
            var fixture = new IssueBuilderFixture();
            const Uri ruleUri = null;

            // When
            var issue = fixture.IssueBuilder.OfRule("RuleId", "RuleName", ruleUri).Create();

            // Then
            issue.RuleUrl.ShouldBe(null);
        }

        [Theory]
        [InlineData("ruleId")]
        public void Should_Set_Rule(string ruleId)
        {
            // Given
            var fixture = new IssueBuilderFixture();
            const string ruleName = "Some Rule";

            // When
            var issue = fixture.IssueBuilder.OfRule(ruleId, ruleName, new Uri("https://google.com")).Create();

            // Then
            issue.RuleId.ShouldBe(ruleId);
        }

        [Theory]
        [InlineData("Rule Name")]
        public void Should_Set_RuleName(string ruleName)
        {
            // Given
            var fixture = new IssueBuilderFixture();

            // When
            var issue = fixture.IssueBuilder.OfRule("Rule", ruleName, new Uri("https://google.com")).Create();

            // Then
            issue.RuleName.ShouldBe(ruleName);
        }

        [Theory]
        [InlineData("https://google.com/")]
        public void Should_Set_RuleUrl(string ruleUri)
        {
            // Given
            var fixture = new IssueBuilderFixture();
            const string ruleName = "Some Rule";

            // When
            var issue = fixture.IssueBuilder.OfRule("Rule", ruleName, new Uri(ruleUri)).Create();

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
            const string run = null;

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
            var run = string.Empty;

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
            const string run = " ";

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
            issue.Run.ShouldBe(run);
        }
    }

    public sealed class TheWithAdditionalInformationMethodThatTakesKeyValuePairs
    {
        [Fact]
        public void Should_Set_Additional_Information()
        {
            // Given
            var fixture = new IssueBuilderFixture();

            // When
            var issue = fixture.IssueBuilder.WithAdditionalInformation("dupe-cost", "974").Create();

            // Then
            issue.AdditionalInformation.ShouldContain(new KeyValuePair<string, string>("dupe-cost", "974"));
        }

        [Fact]
        public void Should_Throw_When_Key_Is_Used_Multiple_Times()
        {
            // Given
            var fixture = new IssueBuilderFixture();

            // When
            var issueBuilder = fixture.IssueBuilder.WithAdditionalInformation("dupe-cost", "974");
            var result = Record.Exception(() => issueBuilder.WithAdditionalInformation("dupe-cost", "1037"));

            // Then
            result.IsArgumentException("key");
        }

        [Fact]
        public void Should_Throw_If_Key_Is_Null()
        {
            // Given
            var fixture = new IssueBuilderFixture();
            const string key = null;

            // When
            var result = Record.Exception(() =>
                fixture.IssueBuilder.WithAdditionalInformation(key, "123"));

            // Then
            result.IsArgumentNullException("key");
        }

        [Fact]
        public void Should_Throw_If_Key_Is_Empty()
        {
            // Given
            var fixture = new IssueBuilderFixture();
            var key = string.Empty;

            // When
            var result = Record.Exception(() =>
                fixture.IssueBuilder.WithAdditionalInformation(key, "123"));

            // Then
            result.IsArgumentException("key");
        }

        [Fact]
        public void Should_Throw_If_Key_Is_WhiteSpace()
        {
            // Given
            var fixture = new IssueBuilderFixture();
            const string key = " ";

            // When
            var result = Record.Exception(() =>
                fixture.IssueBuilder.WithAdditionalInformation(key, "123"));

            // Then
            result.IsArgumentException("key");
        }
    }

    public sealed class TheWithAdditionalInformationMethodThatTakesAWholeDictionary
    {
        [Fact]
        public void Should_Set_Additional_Information()
        {
            // Given
            var fixture = new IssueBuilderFixture();

            // When
            var issue = fixture.IssueBuilder.WithAdditionalInformation(new Dictionary<string, string>
            {
                { "dupe-cost", "974" },
            }).Create();

            // Then
            issue.AdditionalInformation.Count.ShouldBe(1);
            issue.AdditionalInformation.ShouldContain(new KeyValuePair<string, string>("dupe-cost", "974"));
        }

        [Fact]
        public void Should_Throw_If_AdditionalInformation_Is_Null()
        {
            // Given
            var fixture = new IssueBuilderFixture();

            // When
            var result = Record.Exception(() =>
                fixture.IssueBuilder.WithAdditionalInformation(null));

            // Then
            result.IsArgumentNullException("additionalInformation");
        }
    }

    public sealed class TheInFileAtOffsetMethod
    {
        [Fact]
        public void Should_Set_Offset()
        {
            // Given
            var fixture = new IssueBuilderFixture();

            // When
            var issue = fixture.IssueBuilder.InFileAtOffset("foo", 42).Create();

            // Then
            issue.AffectedFileRelativePath.ToString().ShouldBe("foo");
            issue.Offset.ShouldBe(42);
        }

        [Fact]
        public void Should_Set_Offset_Range()
        {
            // Given
            var fixture = new IssueBuilderFixture();

            // When
            var issue = fixture.IssueBuilder.InFileAtOffset("foo", 10, 20).Create();

            // Then
            issue.AffectedFileRelativePath.ToString().ShouldBe("foo");
            issue.Offset.ShouldBe(10);
            issue.EndOffset.ShouldBe(20);
        }

        [Fact]
        public void Should_Handle_Null_Offset()
        {
            // Given
            var fixture = new IssueBuilderFixture();

            // When
            var issue = fixture.IssueBuilder.InFileAtOffset("foo", null).Create();

            // Then
            issue.AffectedFileRelativePath.ToString().ShouldBe("foo");
            issue.Offset.ShouldBeNull();
        }

        [Fact]
        public void Should_Throw_If_Offset_Is_Zero()
        {
            // Given
            var fixture = new IssueBuilderFixture();

            // When
            var result = Record.Exception(() =>
                fixture.IssueBuilder.InFileAtOffset("foo", 0));

            // Then
            result.IsArgumentOutOfRangeException("offset");
        }

        [Fact]
        public void Should_Throw_If_Offset_Is_Negative()
        {
            // Given
            var fixture = new IssueBuilderFixture();

            // When
            var result = Record.Exception(() =>
                fixture.IssueBuilder.InFileAtOffset("foo", -1));

            // Then
            result.IsArgumentOutOfRangeException("offset");
        }

        [Fact]
        public void Should_Throw_If_EndOffset_Is_Zero()
        {
            // Given
            var fixture = new IssueBuilderFixture();

            // When
            var result = Record.Exception(() =>
                fixture.IssueBuilder.InFileAtOffset("foo", 1, 0));

            // Then
            result.IsArgumentOutOfRangeException("endOffset");
        }

        [Fact]
        public void Should_Throw_If_EndOffset_Is_Negative()
        {
            // Given
            var fixture = new IssueBuilderFixture();

            // When
            var result = Record.Exception(() =>
                fixture.IssueBuilder.InFileAtOffset("foo", 1, -1));

            // Then
            result.IsArgumentOutOfRangeException("endOffset");
        }
    }
}