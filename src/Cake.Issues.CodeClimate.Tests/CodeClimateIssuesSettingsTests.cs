namespace Cake.Issues.CodeClimate.Tests;

using Cake.Core.IO;

public class CodeClimateIssuesSettingsTests
{
    public sealed class TheCtor
    {
        [Fact]
        public void Should_Throw_If_LogFilePath_Is_Null()
        {
            // Given / When
            var result = Record.Exception(() => new CodeClimateIssuesSettings((FilePath)null!));

            // Then
            result.IsArgumentNullException("logFilePath");
        }

        [Fact]
        public void Should_Throw_If_LogFileContent_Is_Null()
        {
            // Given / When
            var result = Record.Exception(() => new CodeClimateIssuesSettings((byte[])null!));

            // Then
            result.IsArgumentNullException("logFileContent");
        }

        [Fact]
        public void Should_Set_LogFileContent_If_Empty()
        {
            // Given
            byte[] logFileContent = [];

            // When
            var settings = new CodeClimateIssuesSettings(logFileContent);

            // Then
            settings.LogFileContent.ShouldBe(logFileContent);
        }

        [Fact]
        public void Should_Set_LogFileContent_From_ByteArray()
        {
            // Given
            var logFileContent = "test content"u8.ToArray();

            // When
            var settings = new CodeClimateIssuesSettings(logFileContent);

            // Then
            settings.LogFileContent.ShouldBe(logFileContent);
        }

        [Fact]
        public void Should_Set_LogFileContent()
        {
            // Given
            var logFileContent = "foo"u8.ToArray();

            // When
            var settings = new CodeClimateIssuesSettings(logFileContent);

            // Then
            settings.LogFileContent.ShouldBe(logFileContent);
        }
    }
}