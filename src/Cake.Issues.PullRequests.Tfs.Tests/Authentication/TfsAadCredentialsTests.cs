﻿namespace Cake.Issues.PullRequests.Tfs.Tests.Authentication
{
    using Shouldly;
    using Tfs.Authentication;
    using Xunit;

    public class TfsAadCredentialsTests
    {
        public sealed class TheCtor
        {
            [Theory]
            [InlineData("foo")]
            [InlineData(null)]
            [InlineData("")]
            [InlineData(" ")]
            public void Should_Set_User_Name(string userName)
            {
                // When
                var credentials = new TfsAadCredentials(userName, "bar");

                // Then
                credentials.UserName.ShouldBe(userName);
            }

            [Theory]
            [InlineData("bar")]
            [InlineData(null)]
            [InlineData("")]
            [InlineData(" ")]
            public void Should_Set_Password_Name(string password)
            {
                // When
                var credentials = new TfsAadCredentials("foo", password);

                // Then
                credentials.Password.ShouldBe(password);
            }
        }
    }
}
