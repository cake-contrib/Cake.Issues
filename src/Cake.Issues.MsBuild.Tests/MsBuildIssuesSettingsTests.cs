namespace Cake.Issues.MsBuild.Tests
{
    using System;
    using Cake.Core.IO;
    using Cake.Issues.MsBuild.LogFileFormat;
    using Cake.Issues.Testing;
    using Cake.Testing;
    using Shouldly;
    using Xunit;

    public sealed class MsBuildIssuesSettingsTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_LogFilePath_Is_Null()
            {
                // Given
                const FilePath logFilePath = null;
                var format = new XmlFileLoggerLogFileFormat(new FakeLog());

                // When
                var result = Record.Exception(() => new MsBuildIssuesSettings(logFilePath, format));

                // Then
                result.IsArgumentNullException("logFilePath");
            }

            [Fact]
            public void Should_Throw_If_Format_For_LogFilePath_Is_Null()
            {
                // Given
                const BaseMsBuildLogFileFormat format = null;

                using (var tempFile = new ResourceTempFile("Cake.Issues.MsBuild.Tests.Testfiles.XmlFileLoggerLogFileFormat.FullLog.xml"))
                {
                    // When
                    var result = Record.Exception(() =>
                        new MsBuildIssuesSettings(tempFile.FileName, format));

                    // Then
                    result.IsArgumentNullException("format");
                }
            }

            [Fact]
            public void Should_Throw_If_LogFileContent_Is_Null()
            {
                // Given
                const byte[] logFileContent = null;
                var format = new XmlFileLoggerLogFileFormat(new FakeLog());

                // When
                var result = Record.Exception(() => new MsBuildIssuesSettings(logFileContent, format));

                // Then
                result.IsArgumentNullException("logFileContent");
            }

            [Fact]
            public void Should_Throw_If_Format_For_LogFileContent_Is_Null()
            {
                // Given
                var logFileContent = "foo".ToByteArray();
                const BaseMsBuildLogFileFormat format = null;

                // When
                var result = Record.Exception(() =>
                    new MsBuildIssuesSettings(logFileContent, format));

                // Then
                result.IsArgumentNullException("format");
            }

            [Fact]
            public void Should_Set_LogFileContent()
            {
                // Given
                var logFileContent = "Foo".ToByteArray();
                var format = new XmlFileLoggerLogFileFormat(new FakeLog());

                // When
                var settings = new MsBuildIssuesSettings(logFileContent, format);

                // Then
                settings.LogFileContent.ShouldBe(logFileContent);
            }

            [Fact]
            public void Should_Set_LogFileContent_If_Empty()
            {
                // Given
                var logFileContent = Array.Empty<byte>();
                var format = new XmlFileLoggerLogFileFormat(new FakeLog());

                // When
                var settings = new MsBuildIssuesSettings(logFileContent, format);

                // Then
                settings.LogFileContent.ShouldBe(logFileContent);
            }

            [Fact]
            public void Should_Set_LogFileContent_From_LogFilePath()
            {
                // Given
                var format = new XmlFileLoggerLogFileFormat(new FakeLog());
                using (var tempFile = new ResourceTempFile("Cake.Issues.MsBuild.Tests.Testfiles.XmlFileLoggerLogFileFormat.FullLog.xml"))
                {
                    // When
                    var settings = new MsBuildIssuesSettings(tempFile.FileName, format);

                    // Then
                    settings.LogFileContent.ShouldBe(tempFile.Content);
                }
            }
        }
    }
}
