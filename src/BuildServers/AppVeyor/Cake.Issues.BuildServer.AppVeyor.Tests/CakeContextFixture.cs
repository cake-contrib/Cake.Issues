namespace Cake.Issues.BuildServer.AppVeyor.Tests;

using Cake.Core;
using Cake.Core.Configuration;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Core.Tooling;
using NSubstitute;

internal sealed class CakeContextFixture
{
    public IFileSystem FileSystem { get; set; } = Substitute.For<IFileSystem>();
    public ICakeEnvironment Environment { get; set; } = Substitute.For<ICakeEnvironment>();
    public IGlobber Globber { get; set; } = Substitute.For<IGlobber>();
    public ICakeLog Log { get; set; } = Substitute.For<ICakeLog>();
    public ICakeArguments Arguments { get; set; } = Substitute.For<ICakeArguments>();
    public IProcessRunner ProcessRunner { get; set; } = Substitute.For<IProcessRunner>();
    public IRegistry Registry { get; set; } = Substitute.For<IRegistry>();
    public IToolLocator Tools { get; set; } = Substitute.For<IToolLocator>();
#if NET10_0_OR_GREATER
    public IToolInstaller ToolInstaller { get; set; } = Substitute.For<IToolInstaller>();
    public IServiceProvider ServiceProvider { get; set; } = Substitute.For<IServiceProvider>();
#endif
    public ICakeDataService Data { get; set; } = Substitute.For<ICakeDataService>();
    public ICakeConfiguration Configuration { get; set; } = Substitute.For<ICakeConfiguration>();

    public CakeContext CreateContext() =>
#if NET10_0_OR_GREATER
        new(
            this.FileSystem,
            this.Environment,
            this.Globber,
            this.Log,
            this.Arguments,
            this.ProcessRunner,
            this.Registry,
            this.Tools,
            this.Data,
            this.Configuration,
            this.ToolInstaller,
            this.ServiceProvider);
#else
        new(
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
#endif
}
