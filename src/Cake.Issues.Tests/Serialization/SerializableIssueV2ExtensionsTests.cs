namespace Cake.Issues.Tests.Serialization
{
    using Cake.Issues.Serialization;

    public sealed class SerializableIssueV2ExtensionsTests
    {
        public sealed class TheToIssueExtension
        {
            [Fact]
            public void Should_Throw_If_SerializableIssue_Is_Null()
            {
                // Given
                const SerializableIssueV2 serializableIssue = null;

                // When
                var result = Record.Exception(serializableIssue.ToIssue);

                // Then
                result.IsArgumentNullException("serializableIssue");
            }
        }
    }
}