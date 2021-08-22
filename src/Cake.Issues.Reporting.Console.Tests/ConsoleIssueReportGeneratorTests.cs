namespace Cake.Issues.Reporting.Console.Tests
{
    using Cake.Core.IO;
    using Cake.Issues.Testing;
    using Cake.Testing;
    using Xunit;

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
            [Fact]
            public void Should_Generate_Report()
            {
                // Given

                // When

                // Then
            }
        }
    }
}
