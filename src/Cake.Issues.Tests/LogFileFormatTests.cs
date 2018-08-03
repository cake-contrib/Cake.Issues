namespace Cake.Issues.Tests
{
    using Cake.Core.Diagnostics;
    using Cake.Issues.Testing;
    using Xunit;

    public sealed class LogFileFormatTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given
                ICakeLog log = null;

                // When
                var result = Record.Exception(() => new FakeLogFileFormat(log));

                // Then
                result.IsArgumentNullException("log");
            }
        }
    }
}
