---
title: How to build and run website
description: Instructions how to build and run website locally
---

=== "Running in GitHub Codespaces"

    Website can be built and run in [GitHub Codespaces].

    Follow [Creating a codespace for a repository] to start codespace.

    To build and serve the website follow these steps:

    * Start website in preview mode: ++f5++

    Website preview can be opened in browser.

=== "Using Visual Studio Code Dev Container"

    For building and running website locally a [Visual Studio Code Dev Container] can be used.

    Follow [installation instructions] to setup prerequisites.

    To build and serve the website follow these steps:

    * Clone and open the repository in Visual Studio Code
    * Open the website Dev Container: ++f1++ :material-arrow-right: `Dev Containers: Reopen in Container`
    * Start website in preview mode: ++f5++

    Website preview will be opened in browser.

=== "Running on local machine"

    Ensure the following prerequisites are fulfilled:
    
    * Latest .NET version installed
    * Python 3 installed
    
    To build and serve the website [Cake] is used:
    
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
[GitHub Codespaces]: https://docs.github.com/en/codespaces
[Creating a codespace for a repository]: https://docs.github.com/en/codespaces/developing-in-a-codespace/creating-a-codespace-for-a-repository
