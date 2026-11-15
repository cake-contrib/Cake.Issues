---
name: create-github-release-draft
description: Create or update a Cake Issues GitHub release draft using the shared release template, milestone labels and branch history. Use for stable and beta release drafts. Never publishes releases or creates tags.
---

Use this skill from [start-release](../start-release/SKILL.md) and
[create-beta-release](../create-beta-release/SKILL.md), or to prepare a draft independently.
See [how to release](../../../docs/input/documentation/contributing/how-to-release.md)
and [copilot-instructions](../../copilot-instructions.md).

Only create or update **drafts**. Never publish a release, create or push tags, merge branches,
or change release preparation files. Publishing is a manual step that creates the tag.

## Inputs

* Release version: `X.Y.Z` or `X.Y.Z-beta.N`, where `N` is a positive integer without leading zeroes.
* Milestone: always the stable version `X.Y.Z`, including for betas.
* Source ref and its recorded commit SHA: the local `release/X.Y.Z` for `start-release`
  (which has not been pushed yet), or `origin/release/X.Y.Z` for a beta.
* Draft target: `master` for stable releases, `release/X.Y.Z` for betas.
* Previous published tag for the full changelog and history range. For a stable release or
  first beta, use the preceding stable release; for subsequent betas, use the preceding beta
  of this stable version. Verify that the tag is an ancestor of the source commit.
  Milestone sections remain cumulative for the stable version, even for subsequent betas.
* Whether the user authorized updating an existing draft. Never overwrite one without confirmation.

Infer inputs only when unambiguous; otherwise ask. The calling skill owns release-branch,
CI and beta-number checks. When used independently for a beta, use `create-beta-release`
to perform those checks first; do not bypass its version-selection safeguards.

## 1. Verify prerequisites

* Verify `gh auth status` and permission to create releases in `cake-contrib/Cake.Issues`.
* Verify the canonical remote with `git remote -v`; replace `origin` below if needed.
* Require a clean working tree and an existing source ref and milestone.
* List all releases and tags, including drafts, with pagination:

  ```bash
  gh api --paginate repos/cake-contrib/Cake.Issues/releases --jq '.[] | {tag_name,draft,prerelease,target_commitish}'
  git ls-remote --tags origin
  gh api --paginate "repos/cake-contrib/Cake.Issues/milestones?state=all&per_page=100" --jq '.[] | {number,title,html_url}'
  ```

* A published release or remote tag for the requested version must not be reused.
  For an existing draft, read and preserve its content as source material, not as an
  authoritative template. Do not modify the stable draft when preparing a beta.
* Verify the previous tag and source commit locally (fetch the specific tag if missing):

  ```bash
  git merge-base --is-ancestor <previous-tag> <source-commit-sha>
  ```

API or Git failures are errors, not empty results. Report them and stop.

## 2. Gather delivered changes

Retrieve all milestone issues and pull requests, their labels, authors, reviewers and linked
closing issues. Paginate REST requests; if a CLI list reaches its limit, use the paginated API
instead of accepting a truncated list:

```bash
gh issue list --repo cake-contrib/Cake.Issues --milestone X.Y.Z --state all --limit 1000 --json number,title,url,labels,state,author,stateReason
gh pr list --repo cake-contrib/Cake.Issues --search "milestone:X.Y.Z" --state all --limit 1000 --json number,title,url,labels,state,author,mergedAt,mergeCommit,closingIssuesReferences
gh api --paginate "repos/cake-contrib/Cake.Issues/pulls/<pr-number>/reviews?per_page=100" --jq '.[] | {user:.user.login,state}'
git --no-pager log --format="%H %s%n%b" <previous-tag>..<source-commit-sha>
```

* Only describe changes present on the source commit. Verify merged PR commits are ancestors
  of that commit, or verify the equivalent delivered commits for rebases or cherry-picks.
  A closed issue alone is not proof of delivery; check its resolution and linked implementation.
  Exclude unmerged PRs, open issues, duplicates and issues closed as not planned from delivered items.
  Report open items and discrepancies. Stable releases require confirmation to proceed with open
  items; betas may have open items, but must not claim those changes as delivered.
* Deduplicate linked issues and PRs representing the same change. Prefer the milestone issue
  as the entry and use the implementation PR for evidence, labels and attribution.
  Count distinct delivered milestone entries after this deduplication for the introductory
  issue count (a standalone milestone PR counts as one). Do not count chores.
* Collect distinct reviewers from submitted reviews of delivered PRs, not requested reviewers,
  and issue reporters from the delivered issues' authors. Include linked implementation PRs
  even when only the issue is assigned to the milestone. Replace attribution on every release;
  do not carry names forward or attribute bot accounts as human reporters/reviewers.
  If there are no identifiable people, use `None identified` rather than inventing names.

### Grouping from labels

Use issue and PR labels together. Inspect current labels and the source tree to resolve addin names:

```bash
gh label list --repo cake-contrib/Cake.Issues --limit 200 --json name,description
```

* Changes affecting all or most addins go in `General`, first.
* Map `Area:IssueProvider:<name>` to `Cake.Issues.<name>`,
  `Area:Reporting:<name>` to `Cake.Issues.Reporting.<name>`, and
  `Area:PullRequestSystem:AzureDevOps` to `Cake.Issues.PullRequests.AzureDevOps`.
  The legacy `Area:PullRequestSystem:AppVeyor` and `Area:PullRequestSystem:GitHubActions`
  labels refer to `Cake.Issues.BuildServer.AppVeyor` and `Cake.Issues.BuildServer.GitHubActions`.
* Core labels identify `Cake.Issues`, `Cake.Issues.PullRequests`, `Cake.Issues.Reporting` or
  `Cake.Issues.BuildServer`; use `General` instead when the impact spans all or most addins.
* A change affecting several specific addins may appear in each affected addin section,
  but is counted once in the introduction and must not also appear under `General`.
  Order addins alphabetically after `General`.
* Within each group use these bold headings, in exactly this order, omitting empty types:

  | Label | Heading |
  | --- | --- |
  | `Breaking change` | **⛓️‍💥 Breaking changes** |
  | `Feature` | **✨ Features** |
  | `Improvement` | **📈 Improvements** |
  | `Bug` | **🐞 Bugs** |
  | `dependencies` | **🔗 Dependencies** |
  | `Documentation` | **📖 Documentation** |

* When multiple type labels apply, choose the first applicable type in this ordering to
  avoid duplicate entries. Missing or conflicting area/type information must be resolved
  from the implementation or raised with the user, not guessed.
* Exclude changes with only the `Build` label from user-facing addin sections, consistent with
  the release news checks. Delivered milestone build changes may go in `🧹 Chores`.

### Additional chores from history

Inspect `<previous-tag>..<source-commit-sha>` for additional maintenance changes, including
PRs not assigned to the milestone and direct commits. Resolve PRs associated with commits
when a number is not present in the message:

```bash
gh api --paginate "repos/cake-contrib/Cake.Issues/commits/<commit-sha>/pulls?per_page=100"
```

* Add additional changes in a final `## 🧹 Chores` section, linking the PR, issue or direct commit.
* Exclude anything already represented through the milestone, including alternate PR numbers,
  closing issues, squash commits and merge commits for the same change. Do not list both a
  PR and its commits. A milestone build chore appears only once, not again as a history chore.
* Exclude **all Renovate updates from history-derived chores**. Check PR authors and bot
  identity as well as commit metadata; title matching alone is insufficient.
  Milestone dependency updates remain in the relevant addin's Dependencies section.
* If history reveals a missing user-facing milestone assignment, report it to the user and
  resolve the discrepancy before finalizing, rather than silently disguising it as a chore.

## 3. Apply the release template

Use [release-template.md](templates/release-template.md) for every draft, including updates.
Replace every placeholder; remove empty addin/type sections and an empty Chores section.
Repeat the addin block for each affected addin.

* The website URL must use the **full release version**, including the beta suffix.
  Keep the NuGet search URL unchanged.
* Verify requirements against the **source ref**, not the current checkout or old release:
  Cake package references, `src/Directory.Build.props`, NuGet specifications and dependency
  documentation. In particular, update Cake and TFMs for major releases and addin-specific
  requirements when relevant dependencies change, such as Cake.AzureDevOps.
  Retain unchanged requirements only after checking them.
* Use factual change summaries with issue/PR links. Keep milestone entries in their mapped
  addin/type sections; put only the separately reviewed maintenance changes in Chores.
* End with a **Full changelog** link comparing the verified previous tag to the release version.
  The new tag and versioned documentation may not exist until manual publication.
* Save the completed notes as UTF-8 in the session artifact directory, not the repository.
  Ask for confirmation of version, target and reviewed notes before any creation/update.

## 4. Create or update the draft

Recheck releases and remote tags immediately before writing. Verify the source SHA is unchanged;
for a beta fetch again and compare with the recorded remote SHA. If it changed, repeat the
calling skill's CI and release-note checks. For betas, repeat the eligibility checks from
step 1 of `create-beta-release`: the selected number must exceed every published beta
and remote beta tag for `X.Y.Z`. Do not recursively invoke the calling skill.
Never replace a release that became published in the meantime.

### Stable release

For a new stable draft, retain the existing GitReleaseManager milestone-generation workflow.
Run from the local `release/X.Y.Z` branch. `GITHUB_PAT` needs release permissions:

```powershell
$env:GITHUB_PAT = gh auth token
.\build.ps1 --target=releasenotes
```

```bash
GITHUB_PAT=$(gh auth token) ./build.sh --target=releasenotes
```

Verify it created the expected draft before replacing its generated body with the reviewed
template. For an authorized existing draft, skip generation. Verify it is still a draft,
then apply the reviewed notes (replace `<release-notes-file>` with the UTF-8 file):

```bash
gh release view X.Y.Z --repo cake-contrib/Cake.Issues --json tagName,isDraft,body
gh release edit X.Y.Z --repo cake-contrib/Cake.Issues --draft --prerelease=false --target master --title X.Y.Z --notes-file <release-notes-file>
```

If generation fails, report the error; `start-release` may continue using the milestone for
the news entry, but must report that the draft is incomplete. Never claim an unformatted
GitReleaseManager draft satisfies this skill.

### Beta release

For a new beta:

```bash
gh release create X.Y.Z-beta.N --repo cake-contrib/Cake.Issues --draft --prerelease --latest=false --target release/X.Y.Z --title X.Y.Z-beta.N --notes-file <release-notes-file>
```

For an authorized existing beta draft, verify it is still a draft and then:

```bash
gh release edit X.Y.Z-beta.N --repo cake-contrib/Cake.Issues --draft --prerelease --latest=false --target release/X.Y.Z --title X.Y.Z-beta.N --notes-file <release-notes-file>
```

Always keep `--draft` and, for betas, `--prerelease`. Do not use `--verify-tag`, which requires
a tag before publication. Report creation/update failures and stop this skill.

## 5. Verify and hand off

```bash
gh release view <release-version> --repo cake-contrib/Cake.Issues --json name,tagName,isDraft,isPrerelease,targetCommitish,body,url
git ls-remote --tags origin "refs/tags/<release-version>"
git status --porcelain
```

Verify the title/tag, draft and prerelease flags, target, complete stored body, issue count,
attribution, versioned website, unchanged NuGet URL, requirements, ordered groups,
deduplicated chores without Renovate history updates, and final comparison link.
Ensure there are no placeholders, control characters (other than newlines/tabs) or U+FFFD
replacement characters. Verify no tag was created and the working tree is unchanged.
Report mismatches instead of claiming success. Return the draft URL to the calling skill;
leave publication and its stable/beta-specific handoff to that skill.
