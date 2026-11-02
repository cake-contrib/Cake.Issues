---
name: update-release-notes-link
description: Update the release notes link in all Cake Issues NuGet package specifications to a new release version. Use this when preparing a release branch or when asked to update the release notes link in the nuspec files.
---

Use this skill to point the `<releaseNotes>` element of every NuGet package to the GitHub release of the version which is being released.

## Inputs

* The version being released in the format `X.Y.Z` (for example `6.2.0`).
  If the current branch is `release/X.Y.Z` take the version from the branch name, otherwise ask for it.

## Files to update

All `.nuspec` files in [nuspec/nuget](../../../nuspec/nuget) contain a line like:

```xml
<releaseNotes>https://github.com/cake-contrib/Cake.Issues/releases/tag/6.1.0</releaseNotes>
```

Replace the version at the end of the URL with the new version in **every** file.
Both Cake .NET Tool (`Cake.Issues.*.nuspec`) and Cake Frosting (`Cake.Frosting.Issues.*.nuspec`) packages need to be updated.

Example for PowerShell:

```powershell
$version = "X.Y.Z"
Get-ChildItem nuspec\nuget\*.nuspec | ForEach-Object {
    # Preserve UTF-8 byte order mark if present
    $bytes = [System.IO.File]::ReadAllBytes($_.FullName)
    $hasBom = $bytes.Length -ge 3 -and $bytes[0] -eq 0xEF -and $bytes[1] -eq 0xBB -and $bytes[2] -eq 0xBF
    $content = [System.IO.File]::ReadAllText($_.FullName)
    $updated = $content -replace '(<releaseNotes>https://github\.com/cake-contrib/Cake\.Issues/releases/tag/)[^<]+(</releaseNotes>)', "`${1}$version`${2}"
    if ($updated -ne $content) { [System.IO.File]::WriteAllText($_.FullName, $updated, [System.Text.UTF8Encoding]::new($hasBom)) }
}
```

Example for Bash:

```bash
version="X.Y.Z"
sed -i -E "s#(<releaseNotes>https://github\.com/cake-contrib/Cake\.Issues/releases/tag/)[^<]+(</releaseNotes>)#\1${version}\2#" nuspec/nuget/*.nuspec
```

## Validation

* Every `.nuspec` file in `nuspec/nuget` must contain exactly the new release notes link.
  Count files containing `releases/tag/X.Y.Z` and compare it with the total number of `.nuspec` files.
* `git diff` must only show changes to the `<releaseNotes>` lines. Preserve existing line endings and encoding.

## Commit

When used as part of the release process, commit the change as a separate commit with the message:

```text
Update release notes link
```
