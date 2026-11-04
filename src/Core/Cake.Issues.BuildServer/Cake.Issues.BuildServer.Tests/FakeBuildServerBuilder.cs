namespace Cake.Issues.BuildServer.Tests;

using Cake.Core.Diagnostics;

/// <summary>
/// Class to create instances of <see cref="FakeBuildServer"/> with a fluent API.
/// </summary>
public class FakeBuildServerBuilder
{
    private readonly ICakeLog log;

    /// <summary>
    /// Initializes a new instance of the <see cref="FakeBuildServerBuilder"/> class.
    /// </summary>
    /// <param name="log">The Cake log context.</param>
    private FakeBuildServerBuilder(ICakeLog log)
    {
        log.NotNull();

        this.log = log;
    }

    /// <summary>
    /// Initiates the creation of a new <see cref="FakeBuildServer"/>.
    /// </summary>
    /// <param name="log">The Cake log context.</param>
    /// <returns>Builder class for creating a new build server.</returns>
    public static FakeBuildServerBuilder NewBuildServer(ICakeLog log)
    {
        log.NotNull();

        return new FakeBuildServerBuilder(log);
    }

    /// <summary>
    /// Creates a new <see cref="FakeBuildServer"/>.
    /// </summary>
    /// <returns>New build server.</returns>
    public FakeBuildServer Create() => new(this.log);
}
