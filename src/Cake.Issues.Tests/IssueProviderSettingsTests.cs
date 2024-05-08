namespace Cake.Issues.Tests
{
    using Cake.Core.IO;

    public sealed class IssueProviderSettingsTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_LogFilePath_Is_Null()
            {
                // Given
                const FilePath logFilePath = null;

                // When
                var result = Record.Exception(() => new IssueProviderSettings(logFilePath));

                // Then
                result.IsArgumentNullException("logFilePath");
            }

            [Fact]
            public void Should_Throw_If_LogContent_Is_Null()
            {
                // Given
                const byte[] logFileContent = null;

                // When
                var result = Record.Exception(() => new IssueProviderSettings(logFileContent));

                // Then
                result.IsArgumentNullException("logFileContent");
            }

            [Fact]
            public void Should_Set_LogContent()
            {
                // Given
                var logFileContent = "Foo".ToByteArray();

                // When
                var settings = new IssueProviderSettings(logFileContent);

                // Then
                settings.LogFileContent.ShouldBe(logFileContent);
            }

            [Fact]
            public void Should_Set_If_LogContent_If_Empty()
            {
                // Given
                var logFileContent = Array.Empty<byte>();

                // When
                var settings = new IssueProviderSettings(logFileContent);

                // Then
                settings.LogFileContent.ShouldBe(logFileContent);
            }

            [Fact]
            public void Should_Set_LogContent_From_LogFilePath()
            {
                using (var tempFile = new ResourceTempFile("Cake.Issues.Tests.Testfiles.Build.log"))
                {
                    // When
                    var settings = new IssueProviderSettings(tempFile.FileName);

                    // Then
                    settings.LogFileContent.ShouldBe(tempFile.Content);
                }
            }
        }
    }
}
