# Contribution Guidelines

This repository uses [GitFlow] with default configuration.
Development is happening on `develop` branch.

To contribute:

* Fork this repository.
* Create a feature branch from `develop`.
* Implement your changes.
* Push your feature branch.
* Create a pull request.

## Building

### Prerequisites

General requirements:

* .NET Core 2.1, .NET Core 3.0,  .NET Core 3.1, .NET 5 or .NET 6 for running the build

For building addins:

* .NET 9 SKD

For building website:

* Python 3

### Building addins

On Windows PowerShell run:

```powershell
./build.ps1
```

On OSX/Linux run:

```bash
.\build.sh
```

### Start website

On Windows PowerShell run:

```powershell
./build.ps1 --target=website
```

On OSX/Linux run:

```bash
./build.sh --target=website
```

## Release

See [Cake.Recipe documentation] how to create a new release of this addin.

[GitFlow]: (https://nvie.com/posts/a-successful-git-branching-model/)
[Cake.Recipe documentation]: https://cake-contrib.github.io/Cake.Recipe/docs/usage/creating-release