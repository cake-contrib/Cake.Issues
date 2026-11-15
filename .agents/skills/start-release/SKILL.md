---
name: start-release
description: Start a new Cake Issues release by creating a release branch from develop and preparing it with the news entry, release notes link, version in examples and, for major releases, the Frosting dependency version ranges, each in a separate commit. Use this when asked to start, create or prepare a release or release branch.
---

Use this skill to start a new release of Cake Issues.
It replaces the `git flow release start` workflow previously used for this repository.

See [how to release](../../../docs/input/documentation/contributing/how-to-release.md) for the overall release process
and [AGENTS.md](../../../AGENTS.md) for general repository guidance.

## Inputs

* The version to release in the format `X.Y.Z` (for example `6.2.0`).
  If no version was given, list the open milestones and ask which one should be released:

  ```bash
  gh api repos/cake-contrib/Cake.Issues/milestones --jq '.[] | "\(.title) (open: \(.open_issues), closed: \(.closed_issues))"'
  ```

## Expected result

Release branches follow the structure of previous releases, for example `release/5.9.1`:

```text
* Update Frosting Dependency      <- release/X.Y.Z (major releases only)
* Update version in examples
* Update release notes link
* Add news entry
* <latest commit on develop>      <- branch point
```

For non-major releases the `Update Frosting Dependency` commit is omitted and `Update version in examples`
is the tip of the release branch.

## Steps

### 1. Verify prerequisites

* Working tree must be clean (`git status --porcelain` returns nothing). Otherwise stop and ask the user.
* `gh` must be authenticated (`gh auth status`) with permission to push to `cake-contrib/Cake.Issues`.
* Commands in this skill use `origin` as remote. Verify with `git remote -v` that both fetch and push URLs of
  `origin` point to `cake-contrib/Cake.Issues`. If `origin` is a fork, use the remote pointing to
  `cake-contrib/Cake.Issues` (e.g. `upstream`) instead of `origin` in all commands.
  If no such remote exists, stop and ask the user.
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

Use the [create-github-release-draft](../create-github-release-draft/SKILL.md) skill for
version and milestone `X.Y.Z`, with the local `release/X.Y.Z` branch and its recorded SHA
as source, and `master` as the stable draft target.
The branch is not pushed yet; do not require a remote release branch.

The shared skill retains GitReleaseManager draft generation and applies the release template,
using milestone labels for addin/type grouping and branch history for additional chores.
Review and confirm the completed notes before creating the draft.
If creating or formatting the draft fails, report the error and continue using the milestone
as source for the news entry, explicitly noting that the GitHub draft is incomplete.
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

### 7. Update Frosting dependency version ranges (major releases only)

Only for major releases (`X.0.0`): Use the
[update-frosting-dependencies](../update-frosting-dependencies/SKILL.md) skill and commit the change separately:

```bash
git add nuspec/nuget
git commit -m "Update Frosting Dependency"
```

### 8. Verify

* `git --no-pager log --oneline origin/develop..release/X.Y.Z` must show exactly the three commits above
  (four for major releases).
* Each commit must only contain the files described in the respective step.
* After all preparation commits, refresh the stable release draft using steps 1-5 of the
  [create-github-release-draft](../create-github-release-draft/SKILL.md) skill, with the
  completed `release/X.Y.Z` SHA as the source, `master` as the target and the existing draft
  as the destination. This rechecks requirements against the versions actually prepared,
  including major-release Frosting dependency ranges. Present changed notes and get
  confirmation before updating the draft.
* Optionally build the website to verify the news post renders correctly.

### 9. Push the release branch

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
