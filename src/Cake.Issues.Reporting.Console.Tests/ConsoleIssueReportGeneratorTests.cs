namespace Cake.Issues.Reporting.Console.Tests;

using Xunit.Abstractions;

public sealed class ConsoleIssueReportGeneratorTests
{
    public sealed class TheCtor
    {
        [Fact]
        public void Should_Throw_If_Log_Is_Null()
        {
            // Given / When
            var result = Record.Exception(() =>
                new ConsoleIssueReportGenerator(
                    null,
                    new ConsoleIssueReportFormatSettings()));

            // Then
            result.IsArgumentNullException("log");
        }

        [Fact]
        public void Should_Throw_If_Settings_Are_Null()
        {
            // Given / When
            var result = Record.Exception(() =>
                new ConsoleIssueReportGenerator(
                    new FakeLog(),
                    null));

            // Then
            result.IsArgumentNullException("settings");
        }
    }

    public sealed class TheInternalCreateReportMethod
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
            _ = fixture.CreateReportForTestfile(
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
            _ = fixture.CreateReport(
                [],
                @"c:\Source\Cake.Issues.Reporting.Console");

            // Then
        }

        public sealed class WithShowDiagnosticsEnabled(ITestOutputHelper output)
        {
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
                _ = fixture.CreateReport(issues, @"c:\Source\Cake.Issues.Reporting.Console");

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
                _ = fixture.CreateReport(issues, @"c:\Source\Cake.Issues.Reporting.Console");

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
                _ = fixture.CreateReport(issues, directory);

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
                _ = fixture.CreateReport(issues, directory);

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
                _ = fixture.CreateReport(issues, directory);

                // Then
            }
        }
    }
}
