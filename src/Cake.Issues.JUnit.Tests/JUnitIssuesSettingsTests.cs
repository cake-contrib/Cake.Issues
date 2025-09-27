namespace Cake.Issues.JUnit.Tests;

using Cake.Core.IO;
using Cake.Issues.JUnit.LogFileFormat;

public sealed class JUnitIssuesSettingsTests
{
    public sealed class TheCtor
    {
        [Fact]
        public void Should_Throw_If_LogFilePath_Is_Null()
        {
            // Given / When
            var result = Record.Exception(() =>
                new JUnitIssuesSettings((FilePath)null, new GenericJUnitLogFileFormat(new FakeLog())));

            // Then
            result.IsArgumentNullException("logFilePath");
        }

        [Fact]
        public void Should_Throw_If_LogFileContent_Is_Null()
        {
            // Given / When
            var result = Record.Exception(() =>
                new JUnitIssuesSettings((byte[])null, new GenericJUnitLogFileFormat(new FakeLog())));

            // Then
            result.IsArgumentNullException("logFileContent");
        }

        [Fact]
        public void Should_Set_LogFileContent()
        {
            // Given
            var logFileContent = "foo".ToByteArray();

            // When
            var settings = new JUnitIssuesSettings(logFileContent, new GenericJUnitLogFileFormat(new FakeLog()));

            // Then
            settings.LogFileContent.ShouldBe(logFileContent);
        }
    }
}