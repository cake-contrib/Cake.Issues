namespace Cake.Issues.BuildServer.Tests;

using Cake.Core.Diagnostics;

/// <summary>
/// Class to create instances of <see cref="FakeBuildServerSystem"/> with a fluent API.
/// </summary>
public class FakeBuildServerSystemBuilder
{
    private readonly ICakeLog log;

    /// <summary>
    /// Initializes a new instance of the <see cref="FakeBuildServerSystemBuilder"/> class.
    /// </summary>
    /// <param name="log">The Cake log context.</param>
    private FakeBuildServerSystemBuilder(ICakeLog log)
    {
        log.NotNull();

        this.log = log;
    }

    /// <summary>
    /// Initiates the creation of a new <see cref="FakeBuildServerSystem"/>.
    /// </summary>
    /// <param name="log">The Cake log context.</param>
    /// <returns>Builder class for creating a new build server system.</returns>
    public static FakeBuildServerSystemBuilder NewBuildServerSystem(ICakeLog log)
    {
        log.NotNull();

        return new FakeBuildServerSystemBuilder(log);
    }

    /// <summary>
    /// Creates a new <see cref="FakeBuildServerSystem"/>.
    /// </summary>
    /// <returns>New build server system.</returns>
    public FakeBuildServerSystem Create() => new(this.log);
}
