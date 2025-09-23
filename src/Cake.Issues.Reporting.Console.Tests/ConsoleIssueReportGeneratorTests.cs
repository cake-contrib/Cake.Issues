namespace Cake.Issues.Reporting.Console.Tests;

using System.Text.RegularExpressions;
using Cake.Core.Diagnostics;
using Spectre.Console;
using Spectre.Console.Testing;
using Xunit.Abstractions;

public sealed partial class ConsoleIssueReportGeneratorTests
{
    public sealed class TheCtor
    {
        [Fact]
        public void Should_Throw_If_Console_Is_Null()
        {
            // Given
            IAnsiConsole console = null;
            var log = new FakeLog();
            var settings = new ConsoleIssueReportFormatSettings();

            // When
            var result = Record.Exception(() =>
                new ConsoleIssueReportGenerator(
                    console,
                    log,
                    settings));

            // Then
            result.IsArgumentNullException("console");
        }

        [Fact]
        public void Should_Throw_If_Log_Is_Null()
        {
            // Given
            var console = new TestConsole();
            ICakeLog log = null;
            var settings = new ConsoleIssueReportFormatSettings();

            // When
            var result = Record.Exception(() =>
                new ConsoleIssueReportGenerator(
                    console,
                    log,
                    settings));

            // Then
            result.IsArgumentNullException("log");
        }

        [Fact]
        public void Should_Throw_If_Settings_Are_Null()
        {
            // Given
            var console = new TestConsole();
            var log = new FakeLog();
            ConsoleIssueReportFormatSettings settings = null;

            // When
            var result = Record.Exception(() =>
                new ConsoleIssueReportGenerator(
                    console,
                    log,
                    settings));

            // Then
            result.IsArgumentNullException("settings");
        }
    }

    public sealed partial class TheInternalCreateReportMethod
    {
        public static IEnumerable<object[]> ReportFormatSettingsCombinations =>
            from b1 in boolArray
            from b2 in boolArray
            from b3 in boolArray
            from b4 in boolArray
            from b5 in boolArray
            select new object[] { b1, b2, b3, b4, b5 };

        private static readonly bool[] boolArray = [false, true];

        [Theory]
        [MemberData(nameof(ReportFormatSettingsCombinations))]
        public void Should_Generate_Report(
            bool showDiagnostics,
            bool compact,
            bool groupByRule,
            bool showProviderSummary,
            bool showPrioritySummary)
        {
            // Given
            var fixture = new ConsoleIssueReportFixture
            {
                ConsoleIssueReportFormatSettings =
                {
                    ShowDiagnostics = showDiagnostics,
                    Compact = compact,
                    GroupByRule = groupByRule,
                    ShowProviderSummary = showProviderSummary,
                    ShowPrioritySummary = showPrioritySummary
                }
            };

            // When
            fixture.CreateReportForTestfile(
                "Testfiles.issues.json",
                @"c:\Source\Cake.Issues.Reporting.Console");

            // Then
        }

        [Theory]
        [MemberData(nameof(ReportFormatSettingsCombinations))]
        public void Should_Generate_Report_With_No_Issues(
            bool showDiagnostics,
            bool compact,
            bool groupByRule,
            bool showProviderSummary,
            bool showPrioritySummary)
        {
            // Given
            var fixture = new ConsoleIssueReportFixture
            {
                ConsoleIssueReportFormatSettings =
                {
                    ShowDiagnostics = showDiagnostics,
                    Compact = compact,
                    GroupByRule = groupByRule,
                    ShowProviderSummary = showProviderSummary,
                    ShowPrioritySummary = showPrioritySummary
                }
            };

            // When
            fixture.CreateReport(
                [],
                @"c:\Source\Cake.Issues.Reporting.Console");

            // Then
        }

        public sealed partial class WithShowDiagnosticsEnabled(ITestOutputHelper output)
        {
            // (?<=┌─\[) — positive lookbehind to assert the match is preceded by ┌─[
            // [^\]]+    — matches one or more characters that are not a closing bracket ]
            // (?=\])    — positive lookahead to assert the match is followed by ]
            [GeneratedRegex(@"(?<=┌─\[)[^\]]+(?=\])")]
            private static partial Regex DiagnosticRegEx();

            [Fact]
            public void Should_Filter_Issues_Without_FilePath()
            {
                // Given
                var fixture = new ConsoleIssueReportFixture
                {
                    ConsoleIssueReportFormatSettings =
                    {
                        ShowDiagnostics = true
                    }
                };
                var issues =
                    new List<IIssue>
                    {
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .InFile(@"src\Cake.Issues.Reporting.Console.Tests\ConsoleReportGeneratorTests.cs", 10)
                        .Create(),
                    };

                // When
                fixture.CreateReport(issues, @"c:\Source\Cake.Issues.Reporting.Console");

                // Then
                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered because they either don't belong to a file or the file does not exist.");
            }

            [Fact]
            public void Should_Filter_Issues_Where_File_Does_Not_Exist()
            {
                // Given
                var fixture = new ConsoleIssueReportFixture
                {
                    ConsoleIssueReportFormatSettings =
                    {
                        ShowDiagnostics = true
                    }
                };
                var issues =
                    new List<IIssue>
                    {
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .InFile(@"src\Cake.Issues.Reporting.Generic.Tests\Foo.cs")
                        .Create(),
                    };

                // When
                fixture.CreateReport(issues, @"c:\Source\Cake.Issues.Reporting.Console");

                // Then
                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered because they either don't belong to a file or the file does not exist.");
            }

            [Fact]
            public void Should_Not_Filter_Issues_With_Existing_File()
            {
                // Given
                using var tempSourceFile = new TemporarySourceFile("Testfiles.TestFile.txt");
                var filePath = tempSourceFile.FilePath;
                output.WriteLine($"File path: {filePath}");
                var directory = Path.GetDirectoryName(filePath)!;
                var fileName = Path.GetFileName(filePath);
                var fixture = new ConsoleIssueReportFixture
                {
                    ConsoleIssueReportFormatSettings =
                    {
                        ShowDiagnostics = true
                    }
                };
                var issues =
                    new List<IIssue>
                    {
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .InFile(fileName)
                        .Create(),
                    };

                // When
                fixture.CreateReport(issues, directory);

                // Then
                fixture.Log.Entries.ShouldContain(x => x.Message == "0 issue(s) were filtered because they either don't belong to a file or the file does not exist.");
            }

            [Fact]
            public void Should_Work_Without_Priority()
            {
                // Given
                using var tempSourceFile = new TemporarySourceFile("Testfiles.TestFile.txt");
                var filePath = tempSourceFile.FilePath;
                output.WriteLine($"File path: {filePath}");
                var directory = Path.GetDirectoryName(filePath)!;
                var fileName = Path.GetFileName(filePath);
                var fixture = new ConsoleIssueReportFixture
                {
                    ConsoleIssueReportFormatSettings =
                    {
                        ShowDiagnostics = true
                    }
                };
                var issues =
                    new List<IIssue>
                    {
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .InFile(fileName, 1, 1)
                        .Create(),
                    };
                // When
                fixture.CreateReport(issues, directory);

                // Then
            }

            [Fact]
            public void Should_Work_With_Priority()
            {
                // Given
                using var tempSourceFile = new TemporarySourceFile("Testfiles.TestFile.txt");
                var filePath = tempSourceFile.FilePath;
                output.WriteLine($"File path: {filePath}");
                var directory = Path.GetDirectoryName(filePath)!;
                var fileName = Path.GetFileName(filePath);
                var fixture = new ConsoleIssueReportFixture
                {
                    ConsoleIssueReportFormatSettings =
                    {
                        ShowDiagnostics = true
                    }
                };
                var issues =
                    new List<IIssue>
                    {
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .InFile(fileName, 1, 1)
                        .WithPriority(IssuePriority.Error)
                        .Create(),
                    };
                // When
                fixture.CreateReport(issues, directory);

                // Then
            }

            [Fact]
            public Task Should_Work_With_Issue_On_End_Of_Line()
            {
                // Given
                using var tempSourceFile = new TemporarySourceFile("Testfiles.TestFile.txt");
                var filePath = tempSourceFile.FilePath;
                output.WriteLine($"File path: {filePath}");
                var directory = Path.GetDirectoryName(filePath)!;
                var fileName = Path.GetFileName(filePath);
                var fixture = new ConsoleIssueReportFixture
                {
                    ConsoleIssueReportFormatSettings =
                    {
                        ShowDiagnostics = true
                    }
                };
                var issues =
                    new List<IIssue>
                    {
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .InFile(fileName, 1, 57) // Position after the last character on line 1
                        .Create(),
                    };
                // When
                fixture.CreateReport(issues, directory);

                // Then
                // Add a scrubber that replaces the dynamic ID in the output
                var settings = new VerifySettings();
                settings.AddScrubber(builder =>
                {
                    var updated = DiagnosticRegEx().Replace(builder.ToString(), "<DYNAMIC_ID>");

                    _ = builder.Clear().Append(updated);
                });
                return Verify(fixture.Console.Output, settings);
            }
        }
    }
}
