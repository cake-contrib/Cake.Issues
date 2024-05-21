namespace Cake.Issues.Tests.Serialization;

using Cake.Issues.Serialization;

public sealed class SerializableIssueExtensionsTests
{
    public sealed class TheToIssueExtension
    {
        [Fact]
        public void Should_Throw_If_SerializableIssue_Is_Null()
        {
            // Given
            const SerializableIssue serializableIssue = null;

            // When
            var result = Record.Exception(serializableIssue.ToIssue);

            // Then
            result.IsArgumentNullException("serializableIssue");
        }
    }
}