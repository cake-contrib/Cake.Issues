---
name: update-frosting-dependencies
description: Update the version ranges of dependencies between Cake.Issues packages in the Cake Frosting NuGet package specifications to a new major version. Use this when preparing a major release or when asked to update the Frosting dependency version ranges.
---

Use this skill to point the dependencies between Cake.Issues packages in the Cake Frosting NuGet packages
to the major version which is being released.

This is only required for major releases (`X.0.0`), since the version ranges allow any version within
the current major version.

## Inputs

* The major version being released in the format `X.0.0` (for example `7.0.0`).
  If the current branch is `release/X.0.0` take the version from the branch name, otherwise ask for it.

## Files to update

The Cake Frosting packages (`Cake.Frosting.Issues.*.nuspec`) in [nuspec/nuget](../../../nuspec/nuget)
reference `Cake.Issues` or other `Cake.Frosting.Issues.*` packages with a version range limited to the
current major version:

```xml
<dependency id="Cake.Issues" version="[6.0.0-beta0001,7.0)" exclude="Build,Analyzers" />
```

Update the version range to the new major version in **every** file.
For example for release `7.0.0` replace `[6.0.0-beta0001,7.0)` with `[7.0.0-beta0001,8.0)`.
Don't change `Cake.Core` or other non-Cake.Issues dependencies.

Example for PowerShell:

```powershell
$major = 7 # major version being released
$newRange = "[$major.0.0-beta0001,$($major + 1).0)"
Get-ChildItem nuspec\nuget\Cake.Frosting.Issues.*.nuspec | ForEach-Object {
    # Preserve UTF-8 byte order mark if present
    $bytes = [System.IO.File]::ReadAllBytes($_.FullName)
    $hasBom = $bytes.Length -ge 3 -and $bytes[0] -eq 0xEF -and $bytes[1] -eq 0xBB -and $bytes[2] -eq 0xBF
    $content = [System.IO.File]::ReadAllText($_.FullName)
    $updated = [regex]::Replace($content, '(<dependency id="Cake\.(?:Frosting\.)?Issues[^"]*" version=")\[[^)]+\)', "`${1}$newRange")
    if ($updated -ne $content) { [System.IO.File]::WriteAllText($_.FullName, $updated, [System.Text.UTF8Encoding]::new($hasBom)) }
}
```

Example for Bash:

```bash
major=7 # major version being released
next=$((major + 1))
sed -i.bak -E "s#(<dependency id=\"Cake\.(Frosting\.)?Issues[^\"]*\" version=\")\[[^)]+\)#\1[${major}.0.0-beta0001,${next}.0)#" nuspec/nuget/Cake.Frosting.Issues.*.nuspec \
  && rm nuspec/nuget/Cake.Frosting.Issues.*.nuspec.bak
```

## Validation

* No `Cake.Issues` or `Cake.Frosting.Issues.*` dependency with the old version range must remain in
  `nuspec/nuget`.
* `git diff` must only show changes to the `<dependency>` lines of Cake.Issues packages.
  Preserve existing line endings and encoding.

## Commit

When used as part of the release process, commit the change as a separate commit with the message:

```text
Update Frosting Dependency
```
