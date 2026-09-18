---
name: update-ci-pipelines
description: Update Cake Issues CI pipelines across GitHub Actions, Azure Pipelines, and AppVeyor. Use this when changing .NET SDKs or TFMs, build images, test matrices, reusable CI templates/actions, or CI status badges.
---

Use this skill for coordinated changes to:

- GitHub Actions in `.github/workflows/` and `.github/actions/`
- Azure Pipelines in `azure-pipelines.yml` and `.azuredevops/pipelines/`
- AppVeyor in `.appveyor.yml`
- The build and test status documentation in `CiStatus.md`

See [copilot-instructions](../../copilot-instructions.md) for repository build commands, validation requirements, and pull request handling.

## Goals

- Keep all CI providers aligned with the versions and platforms supported by the repository.
- Provide useful compatibility coverage without creating an unnecessarily large matrix.
- Reuse existing actions and templates instead of duplicating setup or test steps.
- Keep `CiStatus.md` synchronized with every pipeline change that affects jobs, matrices, images, stages, or badges.

## Establish the sources of truth

Before editing pipelines:

1. Read `src/Directory.Build.props` to determine the product-supported target frameworks. The first TFM is normally the lowest supported TFM and the last is normally the highest.
2. Read `src/global.json` to determine the SDK version used to build the addin projects under `src/`. This does not cover the SDKs the root build tooling needs (see the next step).
3. Inspect the matching directories below `tests/` to confirm which TFMs and runners actually have integration tests.
4. Read `.azuredevops/pipelines/templates/steps/install-required-dotnet-versions-for-building.yml`, `.github/workflows/*.yml`, and `.appveyor.yml` to identify additional SDKs required by build tools such as GitVersion, Codecov, and Cake.Recipe.
5. Read `azure-pipelines.yml` to see the Azure entry point: branch/PR triggers, path filters, and the stage templates it includes. Pipeline changes that add, remove, or reorder stages must be reflected here.
6. Check the CI provider's current official image documentation when adding, replacing, or removing an image. Treat the lowest available image as the oldest stable, non-deprecated explicit image supported by that provider, and the highest available image as the newest stable explicit image.

Do not confuse product-supported TFMs with SDKs installed only for tools such as GitVersion, Codecov, or Cake.Recipe. Preserve tool-only SDKs while they are required, and keep the reason documented next to their installation.

## Choose the test matrix deliberately

Use the smallest matrix that proves the supported behavior:

1. **Default coverage:** Test the lowest supported TFM on the lowest available explicit OS image for each relevant operating-system family.
2. **Boundary or critical coverage:** Also test the highest supported TFM on the highest available explicit OS image when:
   - changing framework or runtime support;
   - changing build infrastructure or shared setup;
   - covering a known compatibility issue or regression;
   - validating a critical publishing, packaging, reporting, or repository integration path.
3. **Intermediate coverage:** Add a specific intermediate TFM or image only when it has unique behavior, dependencies, or a known issue that is not covered by the boundaries.
4. **Full cross-product:** Test every TFM and image only in exceptional cases where each combination has a concrete compatibility risk. Document that reason in the workflow or pull request.

Prefer boundary pairs over a full cross-product. Do not expand matrices merely because versions are available.
When reducing an existing matrix, confirm the removed combinations are redundant and explain the coverage change in the pull request. Do not silently reduce unrelated coverage.

Use explicit versioned images such as `ubuntu-24.04` instead of `ubuntu-latest` for build and test compatibility matrices. Preserve an existing `*-latest` choice only when the job intentionally validates the moving default or the exact image is irrelevant.

## Reuse existing CI building blocks

Before adding steps or jobs, search for and reuse:

- `.github/actions/prepare-integration-test`
- `.github/actions/install-markdownlint`
- Azure step templates in `.azuredevops/pipelines/templates/steps/`
- Azure stage templates in `.azuredevops/pipelines/templates/stages/`

If the same new setup is needed by multiple jobs, add or extend a reusable local action or template rather than copying steps.

For third-party GitHub Actions, follow the repository convention of pinning the action to a full commit SHA and retaining the release version in a comment. Do not replace local actions or templates with duplicated inline steps.

## Update .NET versions

When adding, updating, or removing a supported TFM or SDK:

1. Update the product TFM source of truth and `src/global.json` when they are part of the requested change.
2. Update or add Azure SDK installation step templates and wire them through the shared installer.
3. Update GitHub Actions SDK installation lists for build and unit-test jobs.
4. Update integration-test `dotnet` matrices only with SDK versions that represent supported product TFMs with an existing `tests/<addin>/.../<tfm>` directory. Do not add tool-only SDK versions (for example .NET 5 for Codecov or .NET 7 for Cake.Recipe) to these matrices: `.github/actions/prepare-integration-test` derives `TFM=net<major>.0` from the matrix value and would target a non-existent test directory.
5. Update AppVeyor SDK installation commands.
6. Confirm `.github/actions/prepare-integration-test` derives the expected TFM from each SDK matrix value you add or change.
7. Update integration-test working directories only when the corresponding directories exist below `tests/`.
8. Keep template names, comments, display names, and installed versions consistent.
9. Search the repository for the old and new version strings to find package metadata, examples, documentation, and pipeline references that must stay aligned.
10. Remove obsolete SDK setup only after confirming no build tool, target framework, or test runner still requires it.

## Update build images

When adding, replacing, or removing an OS image:

1. Update every affected GitHub Actions matrix or `runs-on` value.
2. Update Azure matrix keys, `imageName` values, and image-specific conditions.
3. Update the AppVeyor `image` only when AppVeyor's own Windows build image coverage is intentionally changing. AppVeyor image names (for example `Visual Studio 2022`) are a separate, provider-specific catalog from GitHub/Azure image labels (for example `windows-2022`); changing a GitHub or Azure Windows image does not by itself require or imply an AppVeyor change.
4. Review image-specific prerequisites such as Mono installation and adjust conditions without broadening them unnecessarily.
5. Search for the old image name across workflows, templates, comments, and `CiStatus.md`.

Keep matrix keys stable when possible because Azure status badge configuration names depend on them.

## Keep CiStatus.md accurate

Always review and update `CiStatus.md` when a pipeline changes.

Ensure that:

- listed operating systems, TFMs, and runners match the effective matrices;
- GitHub Actions badges point to existing workflow files and only claim branch coverage for branches that trigger the workflow;
- Azure badge `stageName` and `jobName` values match the pipeline stage and job `displayName` values; `configuration` matches the job's `displayName` followed by a space and the matrix key (for example `Test Cake Scripting Windows_Server_2022`), not the matrix key alone;
- AppVeyor badges and image labels match `.appveyor.yml`;
- removed jobs, TFMs, or images no longer appear;
- newly added build or test coverage is represented.

Do not update only the visible label of a badge. Validate the encoded query parameters as well.

## Validation

After making changes:

1. Search for stale TFM, SDK, image, stage, job, and configuration names.
2. Review the effective low and high boundary coverage for each affected integration-test runner.
3. Verify all referenced local actions, templates, working directories, and workflow files exist.
4. Run `git diff --check`.
5. Run the smallest existing build or test command that exercises the changed pipeline behavior. For broad shared setup changes, run the full CI build described in `copilot-instructions.md`.
6. If GitHub Actions YAML changed, inspect the workflow diff for valid expressions, indentation, triggers, and pinned action SHAs.
7. If Azure Pipelines YAML changed, verify template paths, parameters, stage dependencies, matrix keys, and image-specific conditions.
8. If AppVeyor changed, verify PowerShell quoting, SDK installation order, image selection, and branch filters.
9. Compare `CiStatus.md` against the final effective pipelines once more.

In the pull request description, explain:

- which TFM or image boundaries are covered;
- why any intermediate or full-matrix coverage is necessary;
- which reusable actions or templates were used or changed;
- how `CiStatus.md` was synchronized;
- which validation commands were run.
