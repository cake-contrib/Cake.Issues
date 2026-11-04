namespace Cake.Issues.Tap.Tests;

using Cake.Core.IO;
using Cake.Issues.Tap.LogFileFormat;

public sealed class TapIssuesSettingsTests
{
    public sealed class TheCtor
    {
        [Fact]
        public void Should_Throw_If_LogFilePath_Is_Null()
        {
            // Given
            FilePath logFilePath = null;
            var format = new GenericLogFileFormat(new FakeLog());

            // When
            var result = Record.Exception(() =>
                new TapIssuesSettings(logFilePath, format));

            // Then
            result.IsArgumentNullException("logFilePath");
        }

        [Fact]
        public void Should_Throw_If_Format_For_LogFilePath_Is_Null()
        {
            // Given
            BaseTapLogFileFormat format = null;

            using (var tempFile = new ResourceTempFile("Cake.Issues.Tap.Tests.Testfiles.GenericLogFileFormat.specification.tap"))
            {
                // When
                var result = Record.Exception(() =>
                    new TapIssuesSettings(tempFile.FileName, format));

                // Then
                result.IsArgumentNullException("format");
            }
        }

        [Fact]
        public void Should_Throw_If_LogFileContent_Is_Null()
        {
            // Given
            byte[] logFileContent = null;
            var format = new GenericLogFileFormat(new FakeLog());

            // When
            var result = Record.Exception(() =>
                new TapIssuesSettings(logFileContent, format));

            // Then
            result.IsArgumentNullException("logFileContent");
        }

        [Fact]
        public void Should_Throw_If_Format_For_LogFileContent_Is_Null()
        {
            // Given
            var logFileContent = "foo".ToByteArray();
            BaseTapLogFileFormat format = null;

            // When
            var result = Record.Exception(() =>
                new TapIssuesSettings(logFileContent, format));

            // Then
            result.IsArgumentNullException("format");
        }

        [Fact]
        public void Should_Set_LogFileContent()
        {
            // Given
            var logFileContent = "Foo".ToByteArray();
            var format = new GenericLogFileFormat(new FakeLog());

            // When
            var settings = new TapIssuesSettings(logFileContent, format);

            // Then
            settings.LogFileContent.ShouldBe(logFileContent);
        }

        [Fact]
        public void Should_Set_LogFileContent_If_Empty()
        {
            // Given
            byte[] logFileContent = [];
            var format = new GenericLogFileFormat(new FakeLog());

            // When
            var settings = new TapIssuesSettings(logFileContent, format);

            // Then
            settings.LogFileContent.ShouldBe(logFileContent);
        }

        [Fact]
        public void Should_Set_LogFileContent_From_LogFilePath()
        {
            // Given
            var format = new GenericLogFileFormat(new FakeLog());
            using (var tempFile = new ResourceTempFile("Cake.Issues.Tap.Tests.Testfiles.GenericLogFileFormat.specification.tap"))
            {
                // When
                var settings = new TapIssuesSettings(tempFile.FileName, format);

                // Then
                settings.LogFileContent.ShouldBe(tempFile.Content);
            }
        }
    }
}
