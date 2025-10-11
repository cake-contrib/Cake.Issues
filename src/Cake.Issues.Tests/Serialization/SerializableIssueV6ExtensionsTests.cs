namespace Cake.Issues.Tests.Serialization;

using Cake.Issues.Serialization;

public sealed class SerializableIssueV6ExtensionsTests
{
    public sealed class TheToIssueExtension
    {
        [Fact]
        public void Should_Throw_If_SerializableIssue_Is_Null()
        {
            // Given
            const SerializableIssueV6 serializableIssue = null;

            // When
            var result = Record.Exception(serializableIssue.ToIssue);

            // Then
            result.IsArgumentNullException("serializableIssue");
        }
    }
}