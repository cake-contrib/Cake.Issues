# Cake Issues Addins for .NET

**ALWAYS reference these instructions first and fallback to search or shell commands only when you encounter unexpected information that does not match the info here.**

This is a .NET based repository containing addins for the Cake Build Automation System. The individual addins are published as NuGet packages. All commands have been validated and timing measured.

## Prerequisites

**CRITICAL**: .NET 10 SDK is required (specified in `src/global.json`):
- Check version: `dotnet --version` (should be 10.0.401 or compatible)

For documentation building:
- Python 3.12+ is required
- Install dependencies:
  - Linux/macOS: `cd docs && pip install -r requirements.txt` (takes 48 seconds)
  - Windows PowerShell: `Push-Location docs; pip install -r requirements.txt; Pop-Location`

## Cross-Platform Shell Usage

This repository provides both Bash (`build.sh`) and PowerShell (`build.ps1`) entry points. Use the script that matches the environment:

- Linux/macOS or Git Bash: `./build.sh --target=<TARGET>`
- Windows PowerShell: `.\build.ps1 --target=<TARGET>`

When changing directories or using paths, use the native syntax for the current shell:

- Linux/macOS paths use `/`, for example `tests/Cake.Issues.MsBuild/script-runner/net9.0`.
- Windows PowerShell paths use `\`, for example `tests\Cake.Issues.MsBuild\script-runner\net9.0`.
- Bash command chaining with `&&` is fine in Bash. In PowerShell, prefer separate commands with `;` or run each command on its own line.

## Working Effectively - Build Commands

**NEVER CANCEL builds or tests - timings can vary by operating system and all timeout guidance below includes a safety buffer:**

### Basic Operations (VALIDATED)
- **Basic build only**:
  - Linux/macOS: `./build.sh --target=DotNet-Build`
  - Windows PowerShell: `.\build.ps1 --target=DotNet-Build`
  - Time: 3.5 minutes on Linux/macOS, about 10.5 minutes on Windows - NEVER CANCEL, set timeout to 15+ minutes
- **Create NuGet packages**:
  - Linux/macOS: `./build.sh --target=Create-NuGet-Packages`
  - Windows PowerShell: `.\build.ps1 --target=Create-NuGet-Packages`
  - Time: 2 minutes - NEVER CANCEL, set timeout to 5+ minutes  
- **Run unit tests**:
  - Linux/macOS: `./build.sh --target=Test`
  - Windows PowerShell: `.\build.ps1 --target=Test`
  - Time: 18 minutes - NEVER CANCEL, set timeout to 30+ minutes
- **Full CI build** (build + test + package + issues analysis):
  - Linux/macOS: `./build.sh`
  - Windows PowerShell: `.\build.ps1`
  - Time: 35 minutes - NEVER CANCEL, set timeout to 60+ minutes

### Integration Tests (VALIDATED)
**CRITICAL**: Must create packages FIRST before running integration tests:
1. Create local NuGet packages (2 minutes):
   - Linux/macOS: `./build.sh --target=Create-NuGet-Packages`
   - Windows PowerShell: `.\build.ps1 --target=Create-NuGet-Packages`
2. Run the integration test from the selected test directory (15 seconds per test):
   - Linux/macOS: `cd tests/<ADDIN-NAME>/<RUNNER>/<TFM> && ./build.sh --verbosity=diagnostic`
   - Windows PowerShell: `Push-Location tests\<ADDIN-NAME>\<RUNNER>\<TFM>; .\build.ps1 --verbosity=diagnostic; Pop-Location`

### Documentation (VALIDATED)
- Install dependencies:
  - Linux/macOS: `cd docs && pip install -r requirements.txt` (48 seconds)
  - Windows PowerShell: `Push-Location docs; pip install -r requirements.txt; Pop-Location`
- **Development server** (builds in ~10 seconds, serves on http://127.0.0.1:8000):
  - Linux/macOS: `cd docs && mkdocs serve`
  - Windows PowerShell: `Push-Location docs; mkdocs serve; Pop-Location`
- **Build static site** (10 seconds):
  - Linux/macOS: `cd docs && mkdocs build --site-dir ../BuildArtifacts/temp/_PublishedDocumentation`
  - Windows PowerShell: `Push-Location docs; mkdocs build --site-dir ..\BuildArtifacts\temp\_PublishedDocumentation; Pop-Location`
- **Builds and serves documentation**:
  - Linux/macOS: `./build.sh --target=website`
  - Windows PowerShell: `.\build.ps1 --target=website`

### Debug Output
Add `--verbosity=diagnostic` to any build command for detailed output.

## Validation Scenarios

**ALWAYS run these validation steps after making changes:**

### Basic Validation Workflow
1. **Clean build**: `./build.sh --target=DotNet-Build` or `.\build.ps1 --target=DotNet-Build` (3.5 min on Linux/macOS, about 10.5 min on Windows)
2. **Unit tests**: `./build.sh --target=Test` or `.\build.ps1 --target=Test` (3 min)
3. **Package creation**: `./build.sh --target=Create-NuGet-Packages` or `.\build.ps1 --target=Create-NuGet-Packages` (2 min)
4. **Integration test**: Pick one from `tests/` and run it (15 sec)
5. **Full CI check**: `./build.sh` or `.\build.ps1` (4 min on Linux/macOS; at least 10.5 min on Windows)

### Before Committing Code
- Ensure no warning or error messages from Roslyn analyzers are present
- Ensure Unit Tests are passing: `./build.sh --target=Test` or `.\build.ps1 --target=Test`
- Ensure Integration Tests for affected addins are passing
- Run full build: `./build.sh` or `.\build.ps1` to catch any issues

## Repository Structure

**Key directories and their purposes:**

### Source Code (`src/`)
- Contains 15+ addins, each in its own subdirectory
- Use consistent naming: `Cake.Issues.*` for main addins
- Each addin has corresponding `.Tests` project
- **Important files**:
  - `src/global.json` - specifies .NET 10 SDK requirement
  - `src/Cake.Issues.slnx` - main solution file
  - `src/Cake.Issues.Testing/` - shared testing utilities
  - `src/Cake.Issues.Testing/IssueChecker.cs` - use for comparing issues in tests

### NuGet Packages (`nuspec/nuget/`)
- Each addin has separate `.nuspec` files for Cake .NET Tool and Cake Frosting
- Each package includes README.md file
- Cake Frosting packages contain targets files for namespace imports

### Integration Tests (`tests/`)
- Each addin has subdirectory with integration tests
- Structure: `tests/<ADDIN-NAME>/script-runner/<TFM>` on Linux/macOS or `tests\<ADDIN-NAME>\script-runner\<TFM>` on Windows, where `<TFM>` is the target framework folder used by the selected integration test (for example, `net8.0`, `net9.0`, or `net10.0`)
- Some addins also have `frosting/` subdirectories
- **CRITICAL**: Run `./build.sh --target=Create-NuGet-Packages` or `.\build.ps1 --target=Create-NuGet-Packages` first to create local packages

### Documentation (`docs/`)
- Uses Material for MkDocs (Python-based)
- `docs/requirements.txt` - Python dependencies  
- `docs/mkdocs.yml` - configuration file
- Markdown format with usage examples

### CI/CD
- `.github/workflows/` - GitHub Actions (unit tests, integration tests)
- `.azuredevops/pipelines/` - Azure Pipelines templates
- `azure-pipelines.yml` - main Azure pipeline file
- When changing any CI pipeline or workflow, update `CiStatus.md` to keep its build matrices and status badges in sync

### Pull Requests
- Before creating a pull request, verify the intended target repository and base branch. Do not assume that the `origin` remote is the repository where the pull request should be opened; local environments may use `origin` for a personal fork and `upstream` for the canonical repository, or may use different remote names.
- For this repository, pull requests should normally target `cake-contrib/Cake.Issues` on the `develop` branch unless the user explicitly asks for a different target.
- If the working branch is pushed to a fork, create the upstream pull request with an explicit repository and head, for example: `gh pr create --repo cake-contrib/Cake.Issues --base develop --head <fork-owner>:<branch-name>`.
- Create or update pull request descriptions using a Markdown body file instead of inline shell text, and verify the resulting description renders backticks, newlines, and lists correctly
- When PR descriptions, comments, or tool-call JSON contain Windows paths or commands with backslashes, avoid accidental escape sequences such as `\b`, `\t`, `\n`, `\r`, `\f`, or `\u`. Escape backslashes as `\\`, construct them in the shell, or use `/` where the target tool accepts it.
- After creating or updating a PR description that includes Windows paths or commands, verify the stored body does not contain control characters or U+FFFD replacement characters.
- Copilot code reviews are enabled for this repository.
- After creating a pull request, monitor build status and Copilot code review comments in parallel.
- Analyze any Copilot code review comments and fix them when the suggested change makes sense.
- Always reply to Copilot code review comments. If a comment is addressed with a fix, resolve the comment after posting the reply.

## Build System Details

**Cake Frosting powered build system:**
- `build/Build.csproj` - build application and dependency definition
- `build/*.cs` - build context and task definitions
- `build.sh` and `build.ps1` - entry point scripts
- Uses `Cake.Frosting.Issues.Recipe` for issue management
- Uses GitVersion for semantic versioning

## Common Issues and Troubleshooting

### Build Issues
- **"Tool not found"**: Build tools are restored automatically by Cake Frosting
- **Package not found**: Ensure NuGet packages created with `./build.sh --target=Create-NuGet-Packages` or `.\build.ps1 --target=Create-NuGet-Packages`
- **GitVersion errors**: Ensure you're in git repository with proper remotes

### Integration Test Issues  
- **Package version mismatch**: Delete `~/.nuget/packages/cake.issues*` on Linux/macOS or `$env:USERPROFILE\.nuget\packages\cake.issues*` on Windows, then rebuild packages
- **Test failures**: Check that you're using the correct .NET target framework folder for the selected integration test
- **Permission denied**: On Linux/macOS, some integration test `build.sh` files may need `chmod +x build.sh` to make executable

### Website Issues
- **mkdocs not found**: Install with `cd docs && pip install -r requirements.txt` on Linux/macOS or `Push-Location docs; pip install -r requirements.txt; Pop-Location` on Windows PowerShell

## Key Guidelines

**Development Best Practices:**
1. Follow Cake addin best practices: https://cakebuild.net/docs/extending/addins/best-practices
2. For issue providers/report formats: https://cakeissues.net/latest/documentation/extending/
3. **Always write unit tests** using helpers from `src/Cake.Issues.Testing`
4. **Use `IssueChecker.cs`** to compare issues against expected results in tests
5. Write integration tests for functionality that can't be unit tested
6. Maintain documentation in `docs/` (basic docs only, API docs auto-generated)
7. Keep existing code structure and organization
8. Test changes with both Cake .NET Tool and Cake Frosting when applicable

## Frequently Used Commands Summary

```bash
# Linux/macOS development cycle (run these in order)
./build.sh --target=DotNet-Build              # 3.5 min - build only
./build.sh --target=Test                      # 18 min  - run tests
./build.sh --target=Create-NuGet-Packages     # 2 min   - create packages

# Integration testing (after packages created)
cd tests/Cake.Issues.MsBuild/script-runner/net9.0
./build.sh --verbosity=diagnostic             # 15 sec  - test specific addin

# Full validation
./build.sh                                    # 18.5 min - complete CI build

# Documentation
cd docs && pip install -r requirements.txt   # 48 sec  - install deps
cd docs && mkdocs serve                       # 10 sec  - dev server on http://127.0.0.1:8000
cd docs && mkdocs build                       # 10 sec  - build static site
```

```powershell
# Windows PowerShell development cycle (run these in order)
.\build.ps1 --target=DotNet-Build              # ~10.5 min - build only
.\build.ps1 --target=Test                      # 18 min  - run tests
.\build.ps1 --target=Create-NuGet-Packages     # 2 min   - create packages

# Integration testing (after packages created)
Push-Location tests\Cake.Issues.MsBuild\script-runner\net9.0
.\build.ps1 --verbosity=diagnostic             # 15 sec  - test specific addin
Pop-Location

# Full validation
.\build.ps1                                    # 18.5 min - complete CI build

# Documentation
Push-Location docs; pip install -r requirements.txt; Pop-Location   # 48 sec - install deps
Push-Location docs; mkdocs serve; Pop-Location                      # 10 sec - dev server on http://127.0.0.1:8000
Push-Location docs; mkdocs build; Pop-Location                      # 10 sec - build static site
```

**Remember**: NEVER CANCEL long-running builds. Set timeouts appropriately and wait for completion.