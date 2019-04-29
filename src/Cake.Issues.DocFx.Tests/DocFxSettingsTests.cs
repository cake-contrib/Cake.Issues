namespace Cake.Issues.DocFx.Tests
{
    using System;
    using Cake.Core.IO;
    using Cake.Issues.Testing;
    using Shouldly;
    using Xunit;

    public sealed class DocFxSettingsTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_LogFilePath_Is_Null()
            {
                // Given
                FilePath logFilePath = null;
                var docRootPath = @"c:\Source\Cake.Issues\docs";

                // When
                var result = Record.Exception(() =>
                    new DocFxIssuesSettings(logFilePath, docRootPath));

                // Then
                result.IsArgumentNullException("logFilePath");
            }

            [Fact]
            public void Should_Throw_If_DocRootPath_For_LogFilePath_Is_Null()
            {
                // Given
                DirectoryPath docRootPath = null;

                using (var tempFile = new ResourceTempFile("Cake.Issues.DocFx.Tests.Testfiles.docfx.json"))
                {
                    // When
                    var result = Record.Exception(() =>
                        new DocFxIssuesSettings(tempFile.FileName, docRootPath));

                    // Then
                    result.IsArgumentNullException("docRootPath");
                }
            }

            [Fact]
            public void Should_Throw_If_LogFileContent_Is_Null()
            {
                // Given
                byte[] logFileContent = null;
                var docRootPath = @"c:\Source\Cake.Issues\docs";

                // When
                var result = Record.Exception(() => new DocFxIssuesSettings(logFileContent, docRootPath));

                // Then
                result.IsArgumentNullException("logFileContent");
            }

            [Fact]
            public void Should_Throw_If_DocRootPath_For_LogFileContent_Is_Null()
            {
                // Given
                var logFileContent = "foo".ToByteArray();
                DirectoryPath docRootPath = null;

                // When
                var result = Record.Exception(() =>
                    new DocFxIssuesSettings(logFileContent, docRootPath));

                // Then
                result.IsArgumentNullException("docRootPath");
            }

            [Fact]
            public void Should_Set_LogFileContent()
            {
                // Given
                var logFileContent = "Foo".ToByteArray();
                var docRootPath = @"c:\Source\Cake.Issues\docs";

                // When
                var settings = new DocFxIssuesSettings(logFileContent, docRootPath);

                // Then
                settings.LogFileContent.ShouldBe(logFileContent);
            }

            [Fact]
            public void Should_Set_LogFileContent_If_Empty()
            {
                // Given
                byte[] logFileContent = Array.Empty<byte>();
                var docRootPath = @"c:\Source\Cake.Issues\docs";

                // When
                var settings = new DocFxIssuesSettings(logFileContent, docRootPath);

                // Then
                settings.LogFileContent.ShouldBe(logFileContent);
            }

            [Fact]
            public void Should_Set_DocRootPath()
            {
                // Given
                var logFileContent = "Foo".ToByteArray();
                var docRootPath = @"c:/Source/Cake.Issues/docs";

                // When
                var settings = new DocFxIssuesSettings(logFileContent, docRootPath);

                // Then
                settings.DocRootPath.ToString().ShouldBe(docRootPath);
            }

            [Fact]
            public void Should_Set_LogFileContent_From_LogFilePath()
            {
                // Given
                var docRootPath = @"c:\Source\Cake.Issues\docs";
                using (var tempFile = new ResourceTempFile("Cake.Issues.DocFx.Tests.Testfiles.docfx.json"))
                {
                    // When
                    var settings = new DocFxIssuesSettings(tempFile.FileName, docRootPath);

                    // Then
                    settings.LogFileContent.ShouldBe(tempFile.Content);
                }
            }

            [Fact]
            public void Should_Set_DocRootPath_From_LogFilePath()
            {
                // Given
                var docRootPath = @"c:/Source/Cake.Issues/docs";
                using (var tempFile = new ResourceTempFile("Cake.Issues.DocFx.Tests.Testfiles.docfx.json"))
                {
                    // When
                    var settings = new DocFxIssuesSettings(tempFile.FileName, docRootPath);

                    // Then
                    settings.DocRootPath.ToString().ShouldBe(docRootPath);
                }
            }
        }
    }
}
