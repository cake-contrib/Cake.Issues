---
title: How to build addins
description: Instructions how to build individual Cake Issues addins.
---

Ensure the following prerequisites are fulfilled:

* Latest .NET version installed

To build the addins and run unit tests [Cake]{target="_blank"} is used:

=== ":material-microsoft-windows: Windows"

    ```powershell
    .\build.ps1
    ```

=== ":material-apple: macOS"

    ```bash
    ./build.sh
    ```

=== ":material-linux: Linux"

    ```bash
    ./build.sh
    ```

To run only part of the build a task can be passed using the `--target=<TASK>` syntax:

| Task                    | Description                                                                                                                         |
|-------------------------|-------------------------------------------------------------------------------------------------------------------------------------|
| `DotNetCore-Build`      | Builds all addins                                                                                                                   |
| `Create-NuGet-Packages` | Builds an creates NuGet packages for all addins. The NuGet packages are available in the `BuildArtifacts\Packages\NuGet` directory. |
| `Test`                  | Builds all addins and runs unit tests. Coverage report is available in the `BuildArtifacts\TestCoverage` directory.                 |

[Cake]: https://cakebuild.net/
