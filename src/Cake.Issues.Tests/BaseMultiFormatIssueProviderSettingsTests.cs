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

    public sealed class BaseMultiFormatIssueProviderSettingsTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_LogFilePath_Is_Null()
            {
                // Given
                FilePath logFilePath = null;
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
                byte[] logFileContent = null;
                var format = new FakeLogFileFormat(new FakeLog());

                // When
                var result = Record.Exception(() => new FakeMultiFormatIssueProviderSettings(logFileContent, format));

                // Then
                result.IsArgumentNullException("logFileContent");
            }

            [Fact]
            public void Should_Throw_If_LogContent_Is_Empty()
            {
                // Given
                byte[] logFileContent = Array.Empty<byte>();
                var format = new FakeLogFileFormat(new FakeLog());

                // When
                var result = Record.Exception(() => new FakeMultiFormatIssueProviderSettings(logFileContent, format));

                // Then
                result.IsArgumentException("logFileContent");
            }

            [Fact]
            public void Should_Set_LogContent()
            {
                // Given
                var logFileContent = Encoding.UTF8.GetBytes("Foo");
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
                var logFileContent = Encoding.UTF8.GetBytes("Foo");
                var format = new FakeLogFileFormat(new FakeLog());

                // When
                var settings = new FakeMultiFormatIssueProviderSettings(logFileContent, format);

                // Then
                settings.Format.ShouldBe(format);
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

                    var format = new FakeLogFileFormat(new FakeLog());

                    // When
                    var settings = new FakeMultiFormatIssueProviderSettings(fileName, format);

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

            [Fact]
            public void Should_Set_Format_For_LogFilePath()
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

                    var format = new FakeLogFileFormat(new FakeLog());

                    // When
                    var settings = new FakeMultiFormatIssueProviderSettings(fileName, format);

                    // Then
                    settings.Format.ShouldBe(format);
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
