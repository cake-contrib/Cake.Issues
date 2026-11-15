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
2. Creates a draft release on GitHub using the `create-github-release-draft` skill,
   with release notes based on the milestone and branch history.
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

After starting the release, ask the agent to create a beta, e.g. `Create beta release 6.2.0`.
This uses the `create-beta-release` skill, which does the following:

1. Checks CI and release notes for the release branch.
2. Proposes the next beta version, starting with `6.2.0-beta.1`.
3. Creates a prerelease draft targeting `release/6.2.0` after confirmation, using the
   `create-github-release-draft` skill without merging branches.

Review and publish the draft on the [GitHub releases page], keeping the release branch as its target
and **Set as a pre-release** enabled. Publishing creates the tag and triggers a build publishing
the beta NuGet packages. If the branch changes before publication, recheck CI and release notes.

Beta documentation currently shows the stable version in its installation examples;
use the published prerelease NuGet package version instead.

## GitHub release draft format

Stable and beta drafts use the shared template in the `create-github-release-draft` skill.
It includes the delivered milestone issue count, reviewers and reporters, versioned website
and NuGet links, and current requirements. Changes are grouped by addin (or `General` for
changes affecting all or most addins), then by label-derived type: breaking changes, features,
improvements, bugs, dependencies and documentation.

Additional maintenance changes from Git history appear under Chores, excluding changes
already covered by the milestone and Renovate updates. A full changelog comparison link
ends the draft. Review the requirements especially for major releases and changed dependencies.

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
