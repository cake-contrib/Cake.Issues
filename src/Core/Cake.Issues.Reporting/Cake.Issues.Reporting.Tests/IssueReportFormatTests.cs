namespace Cake.Issues.Reporting.Tests;

public sealed class IssueReportFormatTests
{
    public sealed class TheCtor
    {
        [Fact]
        public void Should_Throw_If_Log_Is_Null()
        {
            // Given / When
            var result = Record.Exception(() => new FakeIssueReportFormat(null));

            // Then
            result.IsArgumentNullException("log");
        }

        [Fact]
        public void Should_Set_Log()
        {
            // Given
            var log = new FakeLog();

            // When
            var reportFormat = new FakeIssueReportFormat(log);

            // Then
            reportFormat.Log.ShouldBe(log);
        }
    }

    public sealed class TheCreateReportMethod
    {
        [Fact]
        public void Should_Throw_If_Issues_Is_Null()
        {
            // Given
            var fixture = new IssueReportFormatFixture();

            // When
            var result = Record.Exception(() => fixture.CreateReport((List<IIssue>)null));

            // Then
            result.IsArgumentNullException("issues");
        }

        [Fact]
        public void Should_Throw_If_Issues_Contains_Null()
        {
            // Given
            var fixture = new IssueReportFormatFixture();

            // When
            var result = Record.Exception(() => fixture.CreateReport(new List<IIssue> { null }));

            // Then
            result.IsArgumentOutOfRangeException("issues");
        }

        [Fact]
        public void Should_Not_Throw_If_Issues_Is_Empty()
        {
            // Given
            var fixture = new IssueReportFormatFixture();

            // When
            var result = fixture.CreateReport(new List<IIssue>());

            // Then
            _ = result.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Throw_If_Settings_Is_Null()
        {
            // Given
            var reportFormat = new FakeIssueReportFormat(new FakeLog());

            // When
            var result = Record.Exception(() => reportFormat.CreateReport([]));

            // Then
            result.IsInvalidOperationException("Initialize needs to be called first.");
        }
    }
}
