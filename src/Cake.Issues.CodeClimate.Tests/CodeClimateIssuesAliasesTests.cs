namespace Cake.Issues.CodeClimate.Tests;

public class CodeClimateIssuesAliasesTests
{
    public sealed class TheCodeClimateIssuesProviderTypeNameProperty
    {
        [Fact]
        public void Should_Return_Correct_Value()
        {
            // Given
            var context = new FakeCakeContext();

            // When
            var result = context.CodeClimateIssuesProviderTypeName();

            // Then
            result.ShouldBe("Cake.Issues.CodeClimate.CodeClimateIssuesProvider");
        }
    }

    public sealed class TheCodeClimateIssuesFromFilePathMethod
    {
        [Fact]
        public void Should_Throw_If_Context_Is_Null()
        {
            // Given / When
            var result = Record.Exception(() =>
                CodeClimateIssuesAliases.CodeClimateIssuesFromFilePath(
                    null!,
                    @"C:\build\log.json"));

            // Then
            result.IsArgumentNullException("context");
        }

        [Fact]
        public void Should_Throw_If_LogFilePath_Is_Null()
        {
            // Given
            var context = new FakeCakeContext();

            // When
            var result = Record.Exception(() =>
                context.CodeClimateIssuesFromFilePath(null!));

            // Then
            result.IsArgumentNullException("logFilePath");
        }

        [Fact]
        public void Should_Return_Issue_Provider()
        {
            // Given
            var context = new FakeCakeContext();
            var filePath = @"C:\build\log.json";

            // When
            var result = context.CodeClimateIssuesFromFilePath(filePath);

            // Then
            result.ShouldBeOfType<CodeClimateIssuesProvider>();
        }
    }

    public sealed class TheCodeClimateIssuesFromContentMethod  
    {
        [Fact]
        public void Should_Throw_If_Context_Is_Null()
        {
            // Given / When
            var result = Record.Exception(() =>
                CodeClimateIssuesAliases.CodeClimateIssuesFromContent(
                    null!,
                    "content"));

            // Then
            result.IsArgumentNullException("context");
        }

        [Fact]
        public void Should_Throw_If_LogFileContent_Is_Null()
        {
            // Given
            var context = new FakeCakeContext();

            // When
            var result = Record.Exception(() =>
                context.CodeClimateIssuesFromContent(null!));

            // Then
            result.IsArgumentNullException("logFileContent");
        }

        [Fact]
        public void Should_Throw_If_LogFileContent_Is_Empty()
        {
            // Given
            var context = new FakeCakeContext();

            // When
            var result = Record.Exception(() =>
                context.CodeClimateIssuesFromContent(string.Empty));

            // Then
            result.IsArgumentOutOfRangeException("logFileContent");
        }

        [Fact]
        public void Should_Throw_If_LogFileContent_Is_WhiteSpace()
        {
            // Given
            var context = new FakeCakeContext();

            // When
            var result = Record.Exception(() =>
                context.CodeClimateIssuesFromContent(" "));

            // Then
            result.IsArgumentOutOfRangeException("logFileContent");
        }

        [Fact]
        public void Should_Return_Issue_Provider()
        {
            // Given
            var context = new FakeCakeContext();
            var content = "[]";

            // When
            var result = context.CodeClimateIssuesFromContent(content);

            // Then
            result.ShouldBeOfType<CodeClimateIssuesProvider>();
        }
    }

    public sealed class TheCodeClimateIssuesMethod
    {
        [Fact]
        public void Should_Throw_If_Context_Is_Null()
        {
            // Given / When
            var result = Record.Exception(() =>
                CodeClimateIssuesAliases.CodeClimateIssues(
                    null!,
                    new CodeClimateIssuesSettings(@"C:\build\log.json")));

            // Then
            result.IsArgumentNullException("context");
        }

        [Fact]
        public void Should_Throw_If_Settings_Are_Null()
        {
            // Given
            var context = new FakeCakeContext();

            // When
            var result = Record.Exception(() =>
                context.CodeClimateIssues(null!));

            // Then
            result.IsArgumentNullException("settings");
        }

        [Fact]
        public void Should_Return_Issue_Provider()
        {
            // Given
            var context = new FakeCakeContext();
            var settings = new CodeClimateIssuesSettings(@"C:\build\log.json");

            // When
            var result = context.CodeClimateIssues(settings);

            // Then
            result.ShouldBeOfType<CodeClimateIssuesProvider>();
        }
    }
}