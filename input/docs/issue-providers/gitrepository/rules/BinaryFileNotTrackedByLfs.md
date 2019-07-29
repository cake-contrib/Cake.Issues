---
Title: BinaryFileNotTrackedByLfs
Description: A binary file is not tracked by Git LFS.
---

<?# Table Class=table HeaderRows=0 ?>
<?*
"Rule Id" BinaryFileNotTrackedByLfs
Priority Warning
?>
<?#/ Table ?>

## Cause

A binary file in the repository is not tracked by [Git Large File System].

## Rule description

By its nature Git repositories cannot handle binary files well and will keep a full copy of that file in the repository every time a change to that file is committed.
Considering that you always clone the full history of a repository, and not only the latest version, using binary files in a repository considerably slow downs the operation.
[Git Large File System] replaces large files with small text pointers inside the Git repository, while storing the file contents on a remote server.

## How to fix violations

Track the file with [Git Large File System].

[Git Large File System]: https://git-lfs.github.com/
