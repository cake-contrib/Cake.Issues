---
title: BinaryFileNotTrackedByLfs
description: A binary file is not tracked by Git LFS.
---

| Metadata     |                           |
|--------------|---------------------------|
| Rule Id      | BinaryFileNotTrackedByLfs |
| Priority     | Warning                   |
| Available in | 0.7.0 or higher           |

## Cause

A binary file in the repository is not tracked by [Git Large File Storage](https://git-lfs.github.com/){target="_blank"}.

## Rule description

By its nature Git repositories cannot handle binary files well and will keep a full copy of that file in the repository every time a change to that file is committed.
Considering that you always clone the full history of a repository, and not only the latest version, using binary files in a repository considerably slow downs the operation.
[Git Large File Storage](https://git-lfs.github.com/){target="_blank"} replaces large files with small text pointers inside the Git repository, while storing the file contents on a remote server.

!!! info
    The rule assumes that all files, which are not text files are binary files.
    This also includes for example empty files.

## How to fix violations

Track the file with [Git Large File Storage](https://git-lfs.github.com/){target="_blank"}.
