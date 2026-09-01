---
title: How to release addins
description: Instructions how to release individual Cake Issues addins.
---

## Preparation

* Make sure that a GitHub milestone exists for this release.
* Make sure for all issues and pull requests which should appear in release notes appropriate labels and the correct milestone is set.

## Start release

* Create a release branch (eg. `release/1.2.3`)

    ```bash
    git switch develop
    git pull origin develop
    git checkout -b release/<RELEASE> develop
    ```

* Create release notes draft

    === ":material-microsoft-windows: Windows"

        ```powershell
        $Env:GH_TOKEN="<YOUR_GITHUB_TOKEN>"
        .\build.ps1 --target=releasenotes
        ```

    === ":material-apple: macOS"

        ```bash
        export GH_TOKEN="<YOUR_GITHUB_TOKEN>"
        ./build.sh --target=releasenotes
        ```

    === ":material-linux: Linux"

        ```bash
        export GH_TOKEN="<YOUR_GITHUB_TOKEN>"
        ./build.sh --target=releasenotes
        ```

* Update `releaseNotes` tags in `nuspec\nuget\*.nuspec`
* Add news entry to `docs\input\news\posts`

## Finish release

* Merge release branch

    ```bash
    git switch master
    git pull origin master
    git merge --no-ff release/<RELEASE> -m "Merge branch 'release/5.1.0'"
    git switch develop
    git merge --no-ff master
    ```

Follow instructions for  [Cake.Recipe documentation] how to create a new release of this addin.

[Cake.Recipe documentation]: https://cake-contrib.github.io/Cake.Recipe/docs/usage/creating-release
