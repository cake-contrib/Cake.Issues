namespace Cake.Issues.Tests;

using Cake.Core.Diagnostics;

public sealed class BaseLogFileFormatTests
{
    public sealed class TheCtor
    {
        [Fact]
        public void Should_Throw_If_Log_Is_Null()
        {
            // Given
            const ICakeLog log = null;

            // When
            var result = Record.Exception(() => new FakeLogFileFormat(log));

            // Then
            result.IsArgumentNullException("log");
        }

        [Fact]
        public void Should_Set_Log()
        {
            // Given
            var log = new FakeLog();

            // When
            var result = new FakeLogFileFormat(log);

            // Then
            result.Log.ShouldBe(log);
        }
    }
}
