---
title: How to release addins
description: Instructions how to release individual Cake Issues addins.
---

All Cake Issues addins are released together with the same version.
Releases are created from release branches following the [Git Flow] branching model,
using the [Cake.Recipe release process].

The steps to create a release are automated using [agent skills] available in the
`.github/skills` directory of the repository, which can be used with GitHub Copilot or any
other agent supporting skills.

## Prerequisites

* Permission to push to the `master`, `develop` and `release/*` branches and to create releases in the
  [cake-contrib/Cake.Issues] repository.
* [GitHub CLI] installed and authenticated (`gh auth login`).
* A GitHub milestone named after the version to release (e.g. `6.2.0`) containing all issues and
  pull requests of the release, with the appropriate labels set.

## Start release

Ask the agent to start the release, e.g. `Start release 6.2.0`.
This uses the `start-release` skill, which does the following:

1. Creates a branch `release/<VERSION>` from the latest `develop` branch.
2. Creates a draft release on GitHub with release notes based on the milestone.
3. Adds the following commits to the release branch:
    * `Add news entry`: Adds a news post for the release to the website,
      using the `write-release-notes` skill.
    * `Update release notes link`: Updates the link to the release notes in all NuGet packages,
      using the `update-release-notes-link` skill.
    * `Update version in examples`: Updates the version of the addins used in examples on the website,
      using the `update-version-in-examples` skill.
    * `Update Frosting Dependency` (major releases only): Updates the version ranges of dependencies
      between Cake.Issues packages in the Cake Frosting NuGet packages to the new major version,
      using the `update-frosting-dependencies` skill.
4. Pushes the release branch after confirmation.

Review the news post and the draft release on GitHub and make sure the build of the release
branch is passing.
Fixes for the release can be merged into the release branch.

## Beta release

Betas are published from the existing `release/X.Y.Z` branch before finishing the stable
release. Their versions use `X.Y.Z-beta.N`, where `N` starts at `1` and increases for each
beta, for example `6.2.0-beta.1`, then `6.2.0-beta.2`.
Do not create a branch with the beta suffix, merge branches, or delete the release branch.

Ask the agent to create a beta, e.g. `Create beta release 6.2.0` or
`Create beta release 6.2.0-beta.1`. This uses the `create-beta-release` skill, which:

1. Verifies CI results for the release branch commit across GitHub Actions, Azure Pipelines
   and AppVeyor, accounting for path-filtered builds, and proposes the next unused beta number.
2. Checks the release notes against the branch and stable milestone `X.Y.Z`.
   Open milestone items are allowed, but only delivered changes belong in the beta notes.
3. Creates a GitHub release draft marked as a prerelease, targeting `release/X.Y.Z`,
   after confirmation. The stable draft remains unchanged.
4. Leaves publication to you; it does not create a Git tag.

To create and publish a beta manually:

1. Ensure `release/X.Y.Z` has been pushed and its CI builds pass.
2. Check existing releases (including drafts) and tags on GitHub. Choose
   `X.Y.Z-beta.1` for the first beta, or increment the highest existing beta number.
   Never reuse a published beta version or existing tag.
   Even when updating an older draft, its number must exceed all published betas and tags.
3. On the [GitHub releases page], choose **Draft a new release**.
4. Enter `X.Y.Z-beta.N` as a new tag and release title, and select `release/X.Y.Z`
   as the target, not `master` or `develop`. Do not create or push the Git tag separately.
5. Add reviewed release notes based on the stable milestone and the changes actually
   present on the release branch. Check the news post and stable draft for missing changes.
6. Enable **Set as a pre-release** and choose **Save draft**.
7. Review the draft, confirm the release branch builds still pass, and choose
   **Publish release**, keeping the release branch as target and the prerelease option enabled.
   If the branch changed since the draft was created, recheck CI and the release notes first.

Publishing the beta draft creates the `X.Y.Z-beta.N` Git tag, which triggers the build
publishing beta NuGet packages. Creating the draft alone does not trigger a beta release.
The existing documentation publisher also deploys a beta documentation site. Its installation
examples retain the stable `X.Y.Z` version prepared when starting the release, so use
`X.Y.Z-beta.N` explicitly to install beta packages until the stable packages are available.
Keep the release branch, stable milestone and stable draft for further betas and the final
release; do not run `finish-release` until the stable release is ready.

## Finish release

Ask the agent to finish the release, e.g. `Finish release 6.2.0`.
This uses the `finish-release` skill, which does the following:

1. Checks if the news post and the release notes are still up to date, and reports changes on the
   release branch which aren't mentioned or aren't assigned to the milestone.
2. Merges the release branch into `master` after confirmation.
3. Merges `master` into `develop` after confirmation.
4. Deletes the release branch after confirmation.

## Publish release

Publish the draft release on the [GitHub releases page] with `master` as target.

Publishing the release creates the tag for the release, which triggers a build publishing the
NuGet packages and closing the milestone.

[Git Flow]: https://nvie.com/posts/a-successful-git-branching-model/
[Cake.Recipe release process]: https://cake-contrib.github.io/Cake.Recipe/docs/usage/creating-release
[agent skills]: https://docs.github.com/copilot/concepts/agents/about-agent-skills
[cake-contrib/Cake.Issues]: https://github.com/cake-contrib/Cake.Issues
[GitHub CLI]: https://cli.github.com/
[GitHub releases page]: https://github.com/cake-contrib/Cake.Issues/releases
