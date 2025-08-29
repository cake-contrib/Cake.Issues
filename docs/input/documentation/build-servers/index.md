---
title: Build Servers
description: Documentation of the different build server implementations.
---

Build server addins implement integration with specific CI servers and allow the
Cake Issues addin to write found issues and issues to the build run.

<div class="grid cards" markdown>

- :material-source-pull: __[AppVeyor]__ – Integration with AppVeyor builds
- :material-source-pull: __[GitHub Actions]__ – Integration with GitHub Actions

</div>

[AppVeyor]: appveyor/index.md
[GitHub Actions]: github-actions/index.md

!!! tip
    See [How to implement support for build server] for instruction on how to implement support for
    additional build servers.

[How to implement support for build server]: ../extending/build-servers/overview.md
