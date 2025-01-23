---
title: How to build addins
description: Instructions how to build individual Cake Issues addins.
---

## Building addins

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

| Task      | Description                                                                                                                         |
|-----------|-------------------------------------------------------------------------------------------------------------------------------------|
| `Build`   | Builds all addins                                                                                                                   |
| `Package` | Builds an creates NuGet packages for all addins. The NuGet packages are available in the `BuildArtifacts\Packages\NuGet` directory. |
| `Test`    | Builds all addins and runs unit tests. Coverage report is available in the `BuildArtifacts\TestCoverage` directory.                 |

## Building and running website locally

=== "Using Visual Studio Code Dev Container"

    For building and running website locally a [Visual Studio Code Dev Container]{target="_blank"} can be used.

    Follow [installation instructions]{target="_blank"} to setup prerequisites.

    To build and serve the website follow these steps:

    * Clone and open the repository in Visual Studio Code
    * Open the website Dev Container: ++f1++ :material-arrow-right: `Dev Containers: Reopen in Container`
    * Start website in preview mode: ++f5++

    Website preview will be opened in Chrome.

=== "Running on local machine"

    Ensure the following prerequisites are fulfilled:
    
    * Latest .NET version installed
    * Python 3 installed
    
    To build and serve the website [Cake]{target="_blank"} is used:
    
    === ":material-microsoft-windows: Windows"
    
        ```powershell
        .\build.ps1 --target=website
        ```
    
    === ":material-apple: macOS"
    
        ```bash
        ./build.sh --target=website
        ```
    
    === ":material-linux: Linux"
    
        ```bash
        ./build.sh --target=website
        ```
    
    Website is available on `http://127.0.0.1:8000/`

[Cake]: https://cakebuild.net/
[Visual Studio Code Dev Container]: https://code.visualstudio.com/docs/devcontainers/containers
[installation instructions]: https://code.visualstudio.com/docs/devcontainers/containers#_installation
