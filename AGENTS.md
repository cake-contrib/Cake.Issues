# Cake Issues Addins for .NET

This repository contains .NET addins for the Cake Build Automation System.
The individual addins are published as NuGet packages.

## Instruction layout

This file is the shared entry point for coding agents. Read it first, then read
the nested `AGENTS.md` files relevant to the files you inspect or change. Nested
instructions supplement this file within their directory; the closest file
takes precedence for directory-specific guidance.

- [src/AGENTS.md](src/AGENTS.md): addin development, SDK selection, and unit tests.
- [tests/AGENTS.md](tests/AGENTS.md): integration tests and report demos.
- [docs/AGENTS.md](docs/AGENTS.md): website tooling, examples, and generated assets.
- [nuspec/AGENTS.md](nuspec/AGENTS.md): NuGet package specifications.
- [.github/AGENTS.md](.github/AGENTS.md): GitHub Actions and agentic workflows.

Reusable task workflows live in [.agents/skills](.agents/skills), with one
`SKILL.md` per skill and supporting resources alongside it. Use the matching
skill for release preparation, release notes, version updates, CI changes, or
report gallery regeneration. Keep skills in this shared location rather than
duplicating them for each harness.

Keep shared instructions in `AGENTS.md` files and only Copilot-specific behavior
in [.github/copilot-instructions.md](.github/copilot-instructions.md). If a
harness does not automatically discover these files or skills, configure it to
load the shared locations rather than maintaining separate copies.

## Shell and build commands

Run repository build commands from the repository root. Use native shell syntax:

- Linux/macOS or Git Bash: `./build.sh --target=<TARGET>`.
- Windows PowerShell: `.\build.ps1 --target=<TARGET>`.
- Use `/` paths in Bash and `\` paths in PowerShell. In PowerShell, use `;` or
  separate lines to sequence commands.
- Restore missing build tools with `dotnet tool restore`.
- Add `--verbosity=diagnostic` for detailed build output.

The build is powered by Cake.Recipe (`recipe.cake`), Cake Tool
(`.config/dotnet-tools.json`), and GitVersion. The SDK pinned in `src/global.json`
builds the addins; build tools can require additional SDKs. Preserve tool-only
SDKs configured in CI while those tools need them.

| Operation | Bash | PowerShell | Typical duration |
| --- | --- | --- | --- |
| Build | `./build.sh --target=DotNet-Build` | `.\build.ps1 --target=DotNet-Build` | 3.5 min; about 10.5 min on Windows |
| Unit tests | `./build.sh --target=Test` | `.\build.ps1 --target=Test` | 3 min |
| Packages | `./build.sh --target=Create-NuGet-Packages` | `.\build.ps1 --target=Create-NuGet-Packages` | 2 min |
| Full CI | `./build.sh` | `.\build.ps1` | 4 min; at least 10.5 min on Windows |

Never cancel builds or tests merely because they run longer than expected.
Allow at least 15 minutes for build/full CI, 10 minutes for tests, and 5 minutes
for package creation.

## Development and validation

- Follow [Cake addin best practices](https://cakebuild.net/docs/extending/addins/best-practices)
  and the [Cake Issues extension guidance](https://cakeissues.net/latest/documentation/extending/).
- Preserve the existing code structure, naming, and behavior.
- Add unit tests for new or changed behavior, using `Cake.Issues.Testing`.
  Write integration tests for functionality that cannot be unit tested.
- Maintain the relevant documentation in `docs/`; API documentation is generated.
- Test both Cake .NET Tool and Cake Frosting where applicable.
- For code changes, run the build, unit tests, package creation, affected
  integration tests (after package creation), and the full CI build before
  committing. Ensure no Roslyn analyzer warnings or errors remain.
- For documentation-only or agent-instruction changes, use relevant Markdown,
  link, skill, or workflow checks; no .NET build is needed unless build behavior
  also changes. See [docs/AGENTS.md](docs/AGENTS.md) for website validation.
- Report failed or unavailable validation explicitly; do not hide failures.

## CI/CD

GitHub Actions live in `.github/workflows`, Azure templates in
`.azuredevops/pipelines`, and entry points in `azure-pipelines.yml` and
`.appveyor.yml`. Use the `update-ci-pipelines` skill for pipeline changes.
Keep `CiStatus.md` build matrices and status badges synchronized with CI changes.

## Pull requests

- Verify the target repository and base branch before creating a PR. Do not
  assume `origin` is the canonical repository; it may be a personal fork.
- Normally target `cake-contrib/Cake.Issues` on `develop`, unless explicitly
  requested otherwise.
- For fork branches, specify the repository and head explicitly, for example:
  `gh pr create --repo cake-contrib/Cake.Issues --base develop --head <fork-owner>:<branch-name>`.
- Use a Markdown body file when creating or updating PR descriptions with the
  CLI. Verify that backticks, newlines, and lists are preserved.
- Escape backslashes properly in JSON and shell arguments containing Windows
  paths. Verify the stored body has no control or replacement characters.
- Monitor CI and reviewer feedback, address valid findings, and explain fixes
  or disagreements in the relevant review thread.

## Troubleshooting

- Missing tools: run `dotnet tool restore`.
- Missing local packages: run the package creation target before integration tests.
- GitVersion errors: verify the Git repository and its remotes.
- Website setup and integration-test troubleshooting are in the nested guidance.
