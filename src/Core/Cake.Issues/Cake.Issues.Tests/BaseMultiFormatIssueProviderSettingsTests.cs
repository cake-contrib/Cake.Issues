namespace Cake.Issues.Tests;

using Cake.Core.IO;

public sealed class BaseMultiFormatIssueProviderSettingsTests
{
    public sealed class TheCtor
    {
        [Fact]
        public void Should_Throw_If_LogFilePath_Is_Null()
        {
            // Given
            const FilePath logFilePath = null;
            var format = new FakeLogFileFormat(new FakeLog());

            // When
            var result = Record.Exception(() => new FakeMultiFormatIssueProviderSettings(logFilePath, format));

            // Then
            result.IsArgumentNullException("logFilePath");
        }

        [Fact]
        public void Should_Throw_If_LogContent_Is_Null()
        {
            // Given
            const byte[] logFileContent = null;
            var format = new FakeLogFileFormat(new FakeLog());

            // When
            var result = Record.Exception(() => new FakeMultiFormatIssueProviderSettings(logFileContent, format));

            // Then
            result.IsArgumentNullException("logFileContent");
        }

        [Fact]
        public void Should_Set_LogContent()
        {
            // Given
            var logFileContent = "Foo".ToByteArray();
            var format = new FakeLogFileFormat(new FakeLog());

            // When
            var settings = new FakeMultiFormatIssueProviderSettings(logFileContent, format);

            // Then
            settings.LogFileContent.ShouldBe(logFileContent);
        }

        [Fact]
        public void Should_Set_LogContent_If_Empty()
        {
            // Given
            var logFileContent = Array.Empty<byte>();
            var format = new FakeLogFileFormat(new FakeLog());

            // When
            var settings = new FakeMultiFormatIssueProviderSettings(logFileContent, format);

            // Then
            settings.LogFileContent.ShouldBe(logFileContent);
        }

        [Fact]
        public void Should_Set_Format_For_LogContent()
        {
            // Given
            var logFileContent = "Foo".ToByteArray();
            var format = new FakeLogFileFormat(new FakeLog());

            // When
            var settings = new FakeMultiFormatIssueProviderSettings(logFileContent, format);

            // Then
            settings.Format.ShouldBe(format);
        }

        [Fact]
        public void Should_Set_LogContent_From_LogFilePath()
        {
            // Given
            var format = new FakeLogFileFormat(new FakeLog());
            using (var tempFile = new ResourceTempFile("Cake.Issues.Tests.Testfiles.Build.log"))
            {
                // When
                var settings = new FakeMultiFormatIssueProviderSettings(tempFile.FileName, format);

                // Then
                settings.LogFileContent.ShouldBe(tempFile.Content);
            }
        }

        [Fact]
        public void Should_Set_Format_For_LogFilePath()
        {
            // Given
            var format = new FakeLogFileFormat(new FakeLog());
            using (var tempFile = new ResourceTempFile("Cake.Issues.Tests.Testfiles.Build.log"))
            {
                // When
                var settings = new FakeMultiFormatIssueProviderSettings(tempFile.FileName, format);

                // Then
                settings.Format.ShouldBe(format);
            }
        }
    }
}
