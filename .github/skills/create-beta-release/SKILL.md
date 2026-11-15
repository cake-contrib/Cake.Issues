---
name: create-beta-release
description: Create a Cake Issues beta release draft from an existing release branch, checking release notes and selecting an X.Y.Z-beta.N version. Use when asked to create or prepare a beta release. Never merges branches, creates tags or publishes the release.
---

Use this skill to prepare a beta of a release started with the
[start-release](../start-release/SKILL.md) skill.
It follows the checks of [finish-release](../finish-release/SKILL.md), but does not finish
the stable release.

See [how to release](../../../docs/input/documentation/contributing/how-to-release.md)
for the overall release process and
[copilot-instructions](../../copilot-instructions.md) for general repository guidance.

This skill only creates or updates a GitHub release **draft** marked as a **prerelease**.
Do not merge or delete branches, create or push tags, or publish the release.
Publishing the draft is a manual step which creates the tag and triggers package publishing.

## Inputs

* The stable version `X.Y.Z`, or an explicit beta version `X.Y.Z-beta.N`
  (for example `6.2.0-beta.1`). `N` is a positive integer without leading zeroes, starting at `1`.
* The release branch is always `release/X.Y.Z`, without the beta suffix.
  If no version is given and the current branch is `release/X.Y.Z`, infer it from the branch.
  Otherwise list remote release branches and ask which release to prepare.
* If no beta number is given, propose the next unused number for this stable version:
  `1` if no betas exist, otherwise one greater than the highest number among releases
  (including drafts) and remote tags. Compare numbers numerically, not lexicographically.
  Ask the user to confirm the proposed version.

## Expected result

A GitHub release draft named and tagged `X.Y.Z-beta.N`, with:

* `isDraft: true`.
* `isPrerelease: true`.
* `targetCommitish: release/X.Y.Z`.
* Reviewed release notes describing the changes available on that release branch.

The release branch, `master`, `develop`, and Git tags remain unchanged.
The stable release draft `X.Y.Z` is preserved.
For precedent, `6.0.0-beta.2` was a prerelease targeting `release/6.0.0`.

## Steps

### 1. Verify prerequisites and select the beta version

* Working tree must be clean (`git status --porcelain` returns nothing).
  Otherwise stop and ask the user.
* `gh` must be authenticated (`gh auth status`) with permission to create releases in
  `cake-contrib/Cake.Issues`. No push permission to `master` or `develop` is needed.
* Commands use `origin`. Verify with `git remote -v` that its fetch and push URLs point to
  `cake-contrib/Cake.Issues`. If it is a fork, substitute the canonical remote
  (e.g. `upstream`) in all commands. If none exists, stop and ask the user.
* Fetch and verify the existing release branch. Do not create a new branch:

  ```bash
  git fetch origin --prune
  git rev-parse origin/release/X.Y.Z
  ```

  Record the remote branch's commit SHA for the final check.
  If a local `release/X.Y.Z` exists and differs from the remote branch, stop and ask whether
  unpushed commits need to be pushed first. Do not operate in another session's worktree.
* Inspect all releases and remote tags when choosing the beta number:

  ```bash
  gh api --paginate repos/cake-contrib/Cake.Issues/releases --jq '.[] | {tag_name, draft, prerelease, target_commitish}'
  git ls-remote --tags origin "refs/tags/X.Y.Z-beta.*"
  ```

  Do not treat an authentication or API failure as an empty list.
  A published release or existing remote tag for the selected version must not be reused.
  Even for an explicit version or an existing draft, `N` must exceed the highest number
  among published betas and remote beta tags for `X.Y.Z`. If it does not, stop and ask
  the user to choose a newer beta version.
  If that version already has a draft, ask whether to update it or select the next number.
* Check CI results for the recorded release branch commit, not older successful runs.
  Replace `<release-commit-sha>` with the recorded SHA:

  ```bash
  gh run list --repo cake-contrib/Cake.Issues --branch release/X.Y.Z --commit <release-commit-sha> --limit 100
  gh api --paginate "repos/cake-contrib/Cake.Issues/commits/<release-commit-sha>/check-runs?per_page=100" --jq '.check_runs[] | {name,status,conclusion,html_url}'
  gh api --paginate "repos/cake-contrib/Cake.Issues/commits/<release-commit-sha>/status?per_page=100" --jq '.statuses[] | {context,state,target_url}'
  ```

  Verify the required GitHub Actions, Azure Pipelines and AppVeyor builds for this commit.
  Account for workflows skipped by path filters; do not treat an unexplained missing
  result as success. If required builds are failing, still running, or missing,
  report this and ask whether to continue.

### 2. Check that the release notes are up to date

Use the stable milestone `X.Y.Z`, not a separate beta milestone.
Follow the release-note checks in step 2 of `finish-release`:

1. Find the news post on the release branch and list commits in
   `origin/master..origin/release/X.Y.Z`, including changes since the news entry.
2. Check referenced pull requests and their closed issues for milestone `X.Y.Z`.
   Exclude changes with only the `Build` label from the release notes.
3. Compare the news post, milestone, and stable GitHub release draft:

   ```bash
   gh issue list --repo cake-contrib/Cake.Issues --milestone X.Y.Z --state all --limit 1000 --json number,title,labels,state
   gh pr list --repo cake-contrib/Cake.Issues --search "milestone:X.Y.Z" --state all --limit 1000 --json number,title,labels,state
   gh release view X.Y.Z --repo cake-contrib/Cake.Issues --json body,isDraft,targetCommitish
   ```

4. Report missing user-facing changes, missing milestone assignments and open milestone items.
   Unlike a stable release, open milestone items are allowed for a beta.
   Do not describe open or unmerged changes as delivered just because they are in the milestone.
   Include contributors and reporters for the delivered changes.
5. Ask how to proceed if there are discrepancies. Do not automatically edit the news post,
   stable draft, package specifications or example versions as part of preparing a beta.

Prepare the reviewed beta release notes using steps 1-3 of the
[create-github-release-draft](../create-github-release-draft/SKILL.md) skill, with
version `X.Y.Z-beta.N`, milestone `X.Y.Z`, source `origin/release/X.Y.Z` and its recorded
SHA, and target `release/X.Y.Z`. Use the stable draft as source material, but apply the
shared template with beta-specific attribution, requirements and website link.
Retain only changes present on the release branch and add missing delivered changes.
If the stable draft is missing, report that and ask to prepare notes from the milestone
and branch history instead. Do not silently substitute GitHub's generated notes.

### 3. Create the beta release draft

Ask for confirmation of the beta version, target branch and reviewed notes before creating
or updating the draft. Fetch again and compare the release branch SHA with the recorded SHA.
If it changed, repeat the CI and release-note checks before continuing.
Recheck releases and remote tags for the selected beta version to avoid overwriting
a release created in the meantime. Also repeat the requirement that the selected beta
number exceeds every published beta and remote beta tag for `X.Y.Z`.

Use steps 4-5 of [create-github-release-draft](../create-github-release-draft/SKILL.md)
to create or update the beta draft with the reviewed notes file.
Pass the confirmed choice to create a new draft or update the existing beta draft.
The shared skill owns the GitHub commands and template verification; always keep the draft
marked as a prerelease, preserve the stable draft, and stop on creation/update errors.

### 4. Verify and hand off

```bash
gh release view X.Y.Z-beta.N --repo cake-contrib/Cake.Issues --json name,tagName,isDraft,isPrerelease,targetCommitish,body,url
git ls-remote --tags origin "refs/tags/X.Y.Z-beta.N"
git status --porcelain
```

Verify the expected result, the notes, and that no remote beta tag exists.
Report any mismatch rather than claiming success.

Tell the user to review and publish the beta draft on the
[GitHub releases page](https://github.com/cake-contrib/Cake.Issues/releases),
keeping `release/X.Y.Z` as target and **Set as a pre-release** enabled.
Publishing creates tag `X.Y.Z-beta.N` and triggers the build publishing beta NuGet packages.
If the branch changes before publication, repeat the CI and release-note checks.
Warn that the existing documentation publisher also deploys a beta documentation site,
whose installation examples retain the stable version prepared by `start-release`.
When installing the beta, users must select the published NuGet prerelease version,
which can differ from the tag: `6.0.0-beta.2` published packages as `6.0.0-beta0002`.
Changing the documentation publisher is not part of this skill.
Keep the release branch and stable draft for subsequent betas and the eventual stable release.
