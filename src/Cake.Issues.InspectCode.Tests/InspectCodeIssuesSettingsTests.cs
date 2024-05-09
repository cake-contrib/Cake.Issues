namespace Cake.Issues.InspectCode.Tests
{
    using Cake.Core.IO;

    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class InspectCodeIssuesSettingsTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_LogFilePath_Is_Null()
            {
                // Given
                const FilePath logFilePath = null;

                // When
                var result = Record.Exception(() => new InspectCodeIssuesSettings(logFilePath));

                // Then
                result.IsArgumentNullException("logFilePath");
            }

            [Fact]
            public void Should_Throw_If_LogFileContent_Is_Null()
            {
                // Given
                const byte[] logFileContent = null;

                // When
                var result = Record.Exception(() => new InspectCodeIssuesSettings(logFileContent));

                // Then
                result.IsArgumentNullException("logFileContent");
            }

            [Fact]
            public void Should_Set_LogFileContent()
            {
                // Given
                var logFileContent = "Foo".ToByteArray();

                // When
                var settings = new InspectCodeIssuesSettings(logFileContent);

                // Then
                settings.LogFileContent.ShouldBe(logFileContent);
            }

            [Fact]
            public void Should_Set_LogFileContent_If_Empty()
            {
                // Given
                var logFileContent = Array.Empty<byte>();

                // When
                var settings = new InspectCodeIssuesSettings(logFileContent);

                // Then
                settings.LogFileContent.ShouldBe(logFileContent);
            }

            [Fact]
            public void Should_Set_LogFileContent_From_LogFilePath()
            {
                // Given
                using (var tempFile = new ResourceTempFile("Cake.Issues.InspectCode.Tests.Testfiles.inspectcode.xml"))
                {
                    // When
                    var settings = new InspectCodeIssuesSettings(tempFile.FileName);

                    // Then
                    settings.LogFileContent.ShouldBe(tempFile.Content);
                }
            }
        }
    }
}
