---
name: update-version-in-examples
description: Update the Cake Issues version used in examples on the documentation website to a new release version. Use this when preparing a release branch or when asked to update the version shown in documentation examples.
---

Use this skill to update the version of Cake Issues addins which is used in all examples of the documentation website.

## Inputs

* The version being released in the format `X.Y.Z` (for example `6.2.0`).
  If the current branch is `release/X.Y.Z` take the version from the branch name, otherwise ask for it.

## File to update

Examples in the documentation don't contain hard-coded versions.
They use the `cake_issues_version` variable defined in the `extra` section of
[docs/mkdocs.yml](../../../docs/mkdocs.yml):

```yaml
  # renovate: datasource=nuget depName=Cake.Issues versioning=loose
  cake_issues_version: 6.1.0
```

Update the value of `cake_issues_version` to the new version.
Keep the `renovate` comment above the variable unchanged.

Do not update other version references, for example:

* Versions of other tools and addins in `docs/mkdocs.yml` (`cake_version`, `cake_git_version`, ...).
  They are maintained by Renovate.
* `Supported since` versions in the CSV tables under `docs/input/documentation/assets/tables`.
* Versions mentioned in existing news posts.

## Validation

* `git diff` must only show the change of the `cake_issues_version` line.
* Optionally build the website to verify the examples render with the new version (see
  [how to run website](../../../docs/input/documentation/contributing/how-to-run-website.md)).

## Commit

When used as part of the release process, commit the change as a separate commit with the message:

```text
Update version in examples
```
