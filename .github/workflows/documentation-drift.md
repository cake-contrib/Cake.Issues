---
name: Documentation drift
description: Checks recent repository changes for documentation drift and proposes fixes in a draft pull request
on:
  schedule: weekly on monday
  workflow_dispatch:
permissions: read-all
engine: copilot
model: copilot/gpt-5.3-codex
strict: true
timeout-minutes: 30
max-ai-credits: 750
checkout:
  fetch-depth: 0
runtimes:
  python:
    version: "3.12"
network:
  allowed:
    - defaults
    - github
    - python
tools:
  bash:
    - "*"
  edit:
  github:
    toolsets: [default]
safe-outputs:
  create-pull-request:
    base-branch: develop
    draft: true
    labels: [documentation]
    title-prefix: "[Documentation drift] "
    if-no-changes: ignore
    protected-files: allowed
    excluded-files:
      - .appveyor.yml
      - .github/**
      - .azuredevops/**
      - .config/**
      - .devcontainer/**
      - .vscode/**
      - src/**
      - tests/**
      - BuildArtifacts/**
      - GitReleaseManager.yaml
      - LICENSE
      - azure-pipelines.yml
      - build.ps1
      - build.sh
      - codecov.yml
      - recipe.cake
      - nuspec/**/*.nuspec
      - nuspec/**/*.targets
      - docs/input/news/**
      - docs/input/documentation/report-formats/generic/templates/**
      - docs/site/**
      - "**/*.lock.yml"
---

# Check Cake Issues documentation for drift

Review implementation and configuration changes merged during the last seven
days and correct documentation that no longer describes the repository
accurately.

## Documentation sources

The maintained documentation surfaces are:

- `docs/input/documentation/**` for the Cake Issues website;
- `README.md` and `CONTRIBUTING.md` for repository-level guidance;
- `nuspec/nuget/*.md` for package README files; and
- `CiStatus.md` for build and test matrices and status badges.

Treat source code, tests, build scripts, project files, package specifications,
and workflow files as evidence only. Do not edit them.

Do not edit historical release posts under `docs/input/news/**`. Do not edit the
generated report template galleries under
`docs/input/documentation/report-formats/generic/templates/**`; their source
templates and regeneration process are handled separately.

## Required process

1. Use `git log --since="7 days ago"` and `git diff` to identify user-visible
   changes merged during the review window. Focus on public APIs, aliases,
   options, defaults, supported tools and versions, setup steps, examples,
   build/test matrices, and contributor workflows.
2. Read `.github/copilot-instructions.md` before assessing changes. Follow the
   repository's established documentation structure, terminology, examples,
   and validation commands.
3. Compare each user-visible change with all relevant maintained documentation
   surfaces. Search for renamed or removed APIs and for duplicated examples or
   option lists that must remain consistent.
4. Make only corrections that are directly supported by repository evidence.
   Do not speculate, redesign documentation, update dependencies, regenerate
   API reference content, or make unrelated editorial changes.
5. Preserve existing Markdown style and links. Use Cake Script and Cake
   Frosting examples where the surrounding documentation supports both.
6. If files under `docs/**` change, install `docs/requirements.txt` and run
   `mkdocs build --strict` from the `docs` directory. Fix documentation errors
   caused by the proposed changes. Do not hide or broadly suppress failures.
7. Review the final diff and confirm it contains documentation changes only.

## Output

When corrections are needed, create one focused draft pull request against
`develop`. Explain:

- which merged changes caused the drift;
- which documentation was corrected;
- which validation commands were run; and
- any uncertainty that requires human review.

If no documentation drift is supported by repository evidence, make no changes
and call `noop` with a concise summary of what was reviewed.
