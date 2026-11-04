namespace Cake.Issues.Tests;

using Cake.Core;
using Cake.Core.Configuration;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Core.Tooling;
using NSubstitute;

internal sealed class CakeContextFixture
{
    public IFileSystem FileSystem { get; set; }
    public ICakeEnvironment Environment { get; set; }
    public IGlobber Globber { get; set; }
    public ICakeLog Log { get; set; }
    public ICakeArguments Arguments { get; set; }
    public IProcessRunner ProcessRunner { get; set; }
    public IRegistry Registry { get; set; }
    public IToolLocator Tools { get; set; }
    public ICakeDataService Data { get; set; }
    public ICakeConfiguration Configuration { get; set; }

    public CakeContextFixture()
    {
        this.FileSystem = Substitute.For<IFileSystem>();
        this.Environment = Substitute.For<ICakeEnvironment>();
        this.Globber = Substitute.For<IGlobber>();
        this.Log = Substitute.For<ICakeLog>();
        this.Arguments = Substitute.For<ICakeArguments>();
        this.ProcessRunner = Substitute.For<IProcessRunner>();
        this.Registry = Substitute.For<IRegistry>();
        this.Tools = Substitute.For<IToolLocator>();
        this.Data = Substitute.For<ICakeDataService>();
        this.Configuration = Substitute.For<ICakeConfiguration>();
    }

    public CakeContext CreateContext() => new(
        this.FileSystem,
        this.Environment,
        this.Globber,
        this.Log,
        this.Arguments,
        this.ProcessRunner,
        this.Registry,
        this.Tools,
        this.Data,
        this.Configuration);
}
