---
name: start-release
description: Start a new Cake Issues release by creating a release branch from develop and preparing it with the news entry, release notes link and version in examples, each in a separate commit. Use this when asked to start, create or prepare a release or release branch.
---

Use this skill to start a new release of Cake Issues.
It replaces the `git flow release start` workflow previously used for this repository.

See [how to release](../../../docs/input/documentation/contributing/how-to-release.md) for the overall release process
and [copilot-instructions](../../copilot-instructions.md) for general repository guidance.

## Inputs

* The version to release in the format `X.Y.Z` (for example `6.2.0`).
  If no version was given, list the open milestones and ask which one should be released:

  ```bash
  gh api repos/cake-contrib/Cake.Issues/milestones --jq '.[] | "\(.title) (open: \(.open_issues), closed: \(.closed_issues))"'
  ```

## Expected result

Release branches follow the structure of previous releases, for example `release/5.9.1`:

```text
* Update version in examples      <- release/X.Y.Z
* Update release notes link
* Add news entry
* <latest commit on develop>      <- branch point
```

## Steps

### 1. Verify prerequisites

* Working tree must be clean (`git status --porcelain` returns nothing). Otherwise stop and ask the user.
* `gh` must be authenticated (`gh auth status`) with permission to push to `cake-contrib/Cake.Issues`.
* A GitHub milestone named `X.Y.Z` must exist. If it doesn't, stop and ask the user.
* Check the milestone for open issues or pull requests and report them to the user:

  ```bash
  gh issue list --repo cake-contrib/Cake.Issues --milestone X.Y.Z --state open --limit 1000
  gh pr list --repo cake-contrib/Cake.Issues --search "milestone:X.Y.Z" --state open --limit 1000
  ```

  Ask whether to continue if there are open items.
* The branch `release/X.Y.Z` must not exist yet, neither locally nor on `origin`.

### 2. Create the release branch

Always branch from the latest `develop` on `origin`.
Use `--no-track` so the release branch doesn't track `develop`.
This also works if `develop` is checked out in another worktree.

```bash
git fetch origin --prune
git switch --no-track -c release/X.Y.Z origin/develop
```

### 3. Create the GitHub release draft

The release notes on GitHub are created as a draft from the milestone using GitReleaseManager.
`GITHUB_PAT` needs to contain a token with permissions to create releases.

```powershell
$env:GITHUB_PAT = gh auth token
.\build.ps1 --target=releasenotes
```

```bash
GITHUB_PAT=$(gh auth token) ./build.sh --target=releasenotes
```

Verify the draft exists with `gh release view X.Y.Z --repo cake-contrib/Cake.Issues`.
If creating the draft fails, report the error, and continue using the milestone as source for the news entry.
Never publish the draft release. Publishing the release creates the Git tag and is a manual step after the release has been finished.

### 4. Add news entry

Use the [write-release-notes](../write-release-notes/SKILL.md) skill to create the release news post for version `X.Y.Z`.
Use the date of the planned release in the file name and front matter (today unless the user says otherwise).

Differences to the standalone use of the skill:

* Don't create a pull request. Commit directly to the release branch.
* Commit only the new news post:

  ```bash
  git add docs/input/news/posts/YYYY-MM-DD-cake-issues-vX.Y.Z-released.md
  git commit -m "Add news entry"
  ```

### 5. Update release notes link

Use the [update-release-notes-link](../update-release-notes-link/SKILL.md) skill and commit the change separately:

```bash
git add nuspec/nuget
git commit -m "Update release notes link"
```

### 6. Update version in examples

Use the [update-version-in-examples](../update-version-in-examples/SKILL.md) skill and commit the change separately:

```bash
git add docs/mkdocs.yml
git commit -m "Update version in examples"
```

### 7. Verify

* `git --no-pager log --oneline origin/develop..release/X.Y.Z` must show exactly the three commits above.
* Each commit must only contain the files described in the respective step.
* Optionally build the website to verify the news post renders correctly.

### 8. Push the release branch

Ask the user for confirmation before pushing. Then push the branch:

```bash
git push -u origin release/X.Y.Z
```

After pushing, check that the CI builds for the release branch start and report their status:

```bash
gh run list --repo cake-contrib/Cake.Issues --branch release/X.Y.Z
```

## Next steps

Tell the user that:

* The release branch is ready for review and CI validation.
* Fixes for the release can be merged into the release branch. Each change should have an issue or pull request
  assigned to the milestone `X.Y.Z`.
* The release can be finished using the [finish-release](../finish-release/SKILL.md) skill.
