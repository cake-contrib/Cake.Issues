namespace Cake.Issues.Reporting.Console.Tests;

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
    }
}
