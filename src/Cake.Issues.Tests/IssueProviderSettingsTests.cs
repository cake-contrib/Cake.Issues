namespace Cake.Issues.Tests
{
    using System;
    using System.IO;
    using System.Text;
    using Cake.Core.IO;
    using Cake.Issues.Testing;
    using Cake.Testing;
    using Shouldly;
    using Xunit;

    public sealed class IssueProviderSettingsTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_LogFilePath_Is_Null()
            {
                // Given
                FilePath logFilePath = null;

                // When
                var result = Record.Exception(() => new IssueProviderSettings(logFilePath));

                // Then
                result.IsArgumentNullException("logFilePath");
            }

            [Fact]
            public void Should_Throw_If_LogContent_Is_Null()
            {
                // Given
                byte[] logFileContent = null;

                // When
                var result = Record.Exception(() => new IssueProviderSettings(logFileContent));

                // Then
                result.IsArgumentNullException("logFileContent");
            }

            [Fact]
            public void Should_Throw_If_LogContent_Is_Empty()
            {
                // Given
                byte[] logFileContent = Array.Empty<byte>();

                // When
                var result = Record.Exception(() => new IssueProviderSettings(logFileContent));

                // Then
                result.IsArgumentException("logFileContent");
            }

            [Fact]
            public void Should_Set_LogContent()
            {
                // Given
                var logFileContent = Encoding.UTF8.GetBytes("Foo");

                // When
                var settings = new IssueProviderSettings(logFileContent);

                // Then
                settings.LogFileContent.ShouldBe(logFileContent);
            }

            [Fact]
            public void Should_Set_LogContent_From_LogFilePath()
            {
                var fileName = System.IO.Path.GetTempFileName();
                try
                {
                    // Given
                    byte[] expected;
                    using (var ms = new MemoryStream())
                    using (var stream = this.GetType().Assembly.GetManifestResourceStream("Cake.Issues.Tests.Testfiles.Build.log"))
                    {
                        stream.CopyTo(ms);
                        expected = ms.ToArray();

                        using (var file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                        {
                            file.Write(expected, 0, expected.Length);
                        }
                    }

                    // When
                    var settings = new IssueProviderSettings(fileName);

                    // Then
                    settings.LogFileContent.ShouldBe(expected);
                }
                finally
                {
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }
                }
            }
        }
    }
}
