namespace Cake.Issues.GitRepository.Tests
{
    public sealed class GitRunnerTests
    {
        public sealed class TheRunCommandMethod
        {
            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                // Given
                var fixture = new GitRunnerFixture
                {
                    Settings = null,
                };

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                result.IsArgumentNullException("settings");
            }

            [Fact]
            public void Arguments_Should_Be_Added_If_Settings_Are_Passed()
            {
                var fixture = new GitRunnerFixture();
                fixture.Settings.Arguments.Add("Bar");

                var result = fixture.Run();

                result.Args.ShouldBe("Bar");
            }
        }
    }
}