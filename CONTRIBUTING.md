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

* .NET 10 SDK

For building website:

* Python 3.12 or later

### Building addins

On Windows PowerShell run:

```powershell
./build.ps1
```

On OSX/Linux run:

```bash
./build.sh
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

Release and deployment automation is intentionally kept outside the repository build.

[GitFlow]: (https://nvie.com/posts/a-successful-git-branching-model/)