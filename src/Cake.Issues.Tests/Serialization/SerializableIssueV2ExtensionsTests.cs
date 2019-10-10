﻿namespace Cake.Issues.Tests.Serialization
{
    using Cake.Issues.Serialization;
    using Cake.Issues.Testing;
    using Xunit;

    public sealed class SerializableIssueV2ExtensionsTests
    {
        public sealed class TheToIssueExtension
        {
            [Fact]
            public void Should_Throw_If_SerializableIssue_Is_Null()
            {
                // Given
                SerializableIssueV2 serializableIssue = null;

                // When
                var result = Record.Exception(() => serializableIssue.ToIssue());

                // Then
                result.IsArgumentNullException("serializableIssue");
            }
        }
    }
}