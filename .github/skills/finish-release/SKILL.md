---
name: finish-release
description: Finish a Cake Issues release by verifying that the release notes are still up to date and merging the release branch into master and develop. Use this when asked to finish, complete or merge a release or release branch.
---

Use this skill to finish a release which has been started with the [start-release](../start-release/SKILL.md) skill.
It replaces the `git flow release finish` workflow previously used for this repository.

See [how to release](../../../docs/input/documentation/contributing/how-to-release.md) for the overall release process
and [copilot-instructions](../../copilot-instructions.md) for general repository guidance.

This skill does **not** create Git tags.
Tags are created by publishing the GitHub release after the release has been finished.

## Inputs

* The version to release in the format `X.Y.Z`.
  If the current branch is `release/X.Y.Z` take the version from the branch name.
  Otherwise list the release branches with `git branch -r --list "origin/release/*"` and ask which one to finish.

## Expected result

The history follows previous releases, for example `5.9.1`:

```text
*   Merge branch 'master' into develop    <- develop
|\
| *   Merge branch 'release/X.Y.Z'        <- master (tagged X.Y.Z when the GitHub release is published)
| |\
| | * Update version in examples
| | * Update release notes link
| | * Add news entry
```

## Steps

### 1. Verify prerequisites

* Working tree must be clean (`git status --porcelain` returns nothing). Otherwise stop and ask the user.
* `gh` must be authenticated (`gh auth status`) with permission to push to `master` and `develop`.
* Commands in this skill use `origin` as remote. Verify with `git remote -v` that both fetch and push URLs of
  `origin` point to `cake-contrib/Cake.Issues`. If `origin` is a fork, use the remote pointing to
  `cake-contrib/Cake.Issues` (e.g. `upstream`) instead of `origin` in all commands.
  If no such remote exists, stop and ask the user.
* Fetch the latest state. All following steps use `origin/release/X.Y.Z`,
  so they also work if the release branch is checked out in another worktree:

  ```bash
  git fetch origin --prune
  git rev-parse origin/release/X.Y.Z
  ```

  If a local `release/X.Y.Z` branch exists and differs from `origin/release/X.Y.Z`
  (`git rev-parse release/X.Y.Z`), stop and ask the user whether unpushed commits need to be pushed first.
* Check the latest CI runs of the release branch.
  If builds are failing or still running, report it and ask whether to continue.

  ```bash
  gh run list --repo cake-contrib/Cake.Issues --branch release/X.Y.Z --limit 10
  ```

### 2. Check that the release notes are up to date

Before merging, verify that the news post and the GitHub release notes still describe all changes of the release.
Changes might have been added to the release branch after it was started.

1. Find the news post added on the release branch:

   ```bash
   git --no-pager log --format="%h %s" --diff-filter=A --name-only origin/master..origin/release/X.Y.Z -- docs/input/news/posts
   ```

2. List all commits which are part of the release:

   ```bash
   git --no-pager log --no-merges --format="%h %ad %s" --date=short origin/master..origin/release/X.Y.Z
   ```

3. List commits added after the news entry, ignoring the `Update release notes link` and `Update version in examples` commits:

   ```bash
   git --no-pager log --no-merges --format="%h %s" <news-entry-commit>..origin/release/X.Y.Z
   ```

   Also check whether `develop` was merged into the release branch after the news entry.

4. For every pull request referenced in the commits of the release (`(#1234)` in the commit subject) check that it,
   or the issue it closes, is assigned to milestone `X.Y.Z`:

   ```bash
   gh pr view 1234 --repo cake-contrib/Cake.Issues --json number,title,labels,milestone,closingIssuesReferences
   ```

   Changes with only the `Build` label are excluded from the release notes and don't need to be mentioned.

5. Compare the milestone with the news post and the GitHub release draft:

   ```bash
   gh issue list --repo cake-contrib/Cake.Issues --milestone X.Y.Z --state all --limit 1000 --json number,title,labels,state
   gh pr list --repo cake-contrib/Cake.Issues --search "milestone:X.Y.Z" --state all --limit 1000 --json number,title,labels,state
   gh release view X.Y.Z --repo cake-contrib/Cake.Issues
   ```

   * All issues and pull requests in the milestone must be closed.
   * Every user facing change (labels `Breaking change`, `Feature`, `Improvement`, `Bug`) should be covered by the news post.
     Dependency and documentation updates only need to be mentioned if they are relevant for users.
   * People working on and reporting the changes must be listed in the news post.

6. Report the findings to the user in a table listing changes which are not mentioned, changes which are missing a milestone,
   and open milestone items. If there are no findings, state that the release notes are up to date.

If there are findings, ask the user how to proceed before merging.
Before editing, committing or regenerating the release draft, make sure to work in a clean checkout of
`release/X.Y.Z` which is identical to `origin/release/X.Y.Z`. If the branch is checked out in another worktree
(see `git worktree list`), work in that worktree. Otherwise run `git switch release/X.Y.Z`
(or `git switch --track origin/release/X.Y.Z` if no local branch exists).

Possible actions are:

* Update the news post using the [write-release-notes](../write-release-notes/SKILL.md) skill and commit the change
  with the message `Update news entry`.
* Assign missing issues or pull requests to the milestone.
* Regenerate the GitHub release draft from the milestone:

  ```powershell
  $env:GITHUB_PAT = gh auth token
  .\build.ps1 --target=releasenotes
  ```

  ```bash
  GITHUB_PAT=$(gh auth token) ./build.sh --target=releasenotes
  ```

Push any additional commits to the release branch, wait for CI to pass, and repeat this step before continuing.

### 3. Merge release branch into master

Ask the user for confirmation before merging and pushing.

Use a detached `HEAD` so this also works if `master` is checked out in another worktree:

```bash
git fetch origin
git switch --detach origin/master
git merge --no-ff origin/release/X.Y.Z -m "Merge branch 'release/X.Y.Z'"
git push origin HEAD:master
```

### 4. Merge master into develop

```bash
git fetch origin
git switch --detach origin/develop
git merge --no-ff origin/master -m "Merge branch 'master' into develop"
git push origin HEAD:develop
```

If there are merge conflicts, typically in `docs/mkdocs.yml`, `nuspec/nuget/*.nuspec` or lock files caused by
dependency updates on `develop`, keep the dependency updates from `develop` and the release versions from the release branch.
Ask the user if a conflict can't be resolved unambiguously.

### 5. Delete the release branch

Ask the user for confirmation, then delete the release branch on `origin` and, if it exists and isn't
checked out in another worktree, locally:

```bash
git push origin --delete release/X.Y.Z
git branch -D release/X.Y.Z
```

## Next steps

Tell the user to finish the release by publishing the GitHub release draft `X.Y.Z` on
<https://github.com/cake-contrib/Cake.Issues/releases> with `master` as target.
Publishing the release creates the tag `X.Y.Z`, which triggers the build publishing the packages to NuGet.

Do not publish the release or create the tag as part of this skill.
