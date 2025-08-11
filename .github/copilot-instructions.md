# Cake Issues Addins for .NET

**ALWAYS reference these instructions first and fallback to search or bash commands only when you encounter unexpected information that does not match the info here.**

This is a .NET based repository containing addins for the Cake Build Automation System. The individual addins are published as NuGet packages. All commands have been validated and timing measured.

## Prerequisites

**CRITICAL**: .NET 9 SDK is required (specified in `src/global.json`):
- Check version: `dotnet --version` (should be 9.0.304 or compatible)
- Restore tools: `dotnet tool restore` (takes 5 seconds)

For documentation building:
- Python 3.12+ is required
- Install dependencies: `cd docs && pip install -r requirements.txt` (takes 48 seconds)

## Working Effectively - Build Commands

**NEVER CANCEL builds or tests - all timing below includes 50% safety buffer:**

### Basic Operations (VALIDATED)
- **Basic build only**: `./build.sh --target=DotNetCore-Build` 
  - Time: 3.5 minutes - NEVER CANCEL, set timeout to 10+ minutes
- **Create NuGet packages**: `./build.sh --target=Create-NuGet-Packages`
  - Time: 2 minutes - NEVER CANCEL, set timeout to 5+ minutes  
- **Run unit tests**: `./build.sh --target=Test`
  - Time: 3 minutes - NEVER CANCEL, set timeout to 10+ minutes
- **Full CI build**: `./build.sh` (build + test + package + issues analysis)
  - Time: 4 minutes - NEVER CANCEL, set timeout to 15+ minutes

### Integration Tests (VALIDATED)
**CRITICAL**: Must create packages FIRST before running integration tests:
1. `./build.sh --target=Create-NuGet-Packages` (2 minutes)
2. `cd tests/<ADDIN-NAME>/script-runner/net9.0 && ./build.sh --verbosity=diagnostic` (15 seconds per test)

Available integration test directories:
- `tests/Cake.Issues.MsBuild/script-runner/net8.0` and `net9.0`
- `tests/Cake.Issues.GitRepository/script-runner/`
- `tests/Cake.Issues.Markdownlint/script-runner/`
- `tests/Cake.Issues.PullRequests.GitHubActions/script-runner/`
- `tests/Cake.Issues.Reporting.Console/script-runner/`
- `tests/Cake.Issues.Reporting.Generic/script-runner/`
- `tests/Cake.Issues.Reporting.Sarif/script-runner/`

### Documentation (VALIDATED but limited)
- Install dependencies: `cd docs && pip install -r requirements.txt` (48 seconds)
- **NOTE**: `mkdocs serve` fails in sandboxed environments due to network restrictions (fonts.gstatic.com access)
- Website target: `./build.sh --target=website` may have similar network issues

### Debug Output
Add `--verbosity=diagnostic` to any build command for detailed output.

## Validation Scenarios

**ALWAYS run these validation steps after making changes:**

### Basic Validation Workflow
1. **Clean build**: `./build.sh --target=DotNetCore-Build` (3.5 min)
2. **Unit tests**: `./build.sh --target=Test` (3 min) 
3. **Package creation**: `./build.sh --target=Create-NuGet-Packages` (2 min)
4. **Integration test**: Pick one from `tests/` and run it (15 sec)
5. **Full CI check**: `./build.sh` (4 min)

### Before Committing Code
- Ensure no warning or error messages from Roslyn analyzers are present
- Ensure Unit Tests are passing: `./build.sh --target=Test`
- Ensure Integration Tests for affected addins are passing
- Run full build: `./build.sh` to catch any issues

## Repository Structure

**Key directories and their purposes:**

### Source Code (`src/`)
- Contains 15+ addins, each in its own subdirectory
- Use consistent naming: `Cake.Issues.*` for main addins
- Each addin has corresponding `.Tests` project
- **Important files**:
  - `src/global.json` - specifies .NET 9 SDK requirement
  - `src/Cake.Issues.slnx` - main solution file
  - `src/Cake.Issues.Testing/` - shared testing utilities
  - `src/Cake.Issues.Testing/IssueChecker.cs` - use for comparing issues in tests

### NuGet Packages (`nuspec/nuget/`)
- Each addin has separate `.nuspec` files for Cake .NET Tool and Cake Frosting
- Each package includes README.md file
- Cake Frosting packages contain targets files for namespace imports

### Integration Tests (`tests/`)
- Each addin has subdirectory with integration tests
- Structure: `tests/<ADDIN-NAME>/script-runner/net8.0` and `net9.0`
- Some addins also have `frosting/` subdirectories
- **CRITICAL**: Run `./build.sh --target=Create-NuGet-Packages` first to create local packages

### Documentation (`docs/`)
- Uses Material for MkDocs (Python-based)
- `docs/requirements.txt` - Python dependencies  
- `docs/mkdocs.yml` - configuration file
- Markdown format with usage examples

### CI/CD
- `.github/workflows/` - GitHub Actions (unit tests, integration tests)
- `.azuredevops/pipelines/` - Azure Pipelines templates
- `azure-pipelines.yml` - main Azure pipeline file

## Build System Details

**Cake.Recipe powered build system:**
- `recipe.cake` - main build configuration
- `build.sh` - entry point script (calls dotnet cake)
- `.config/dotnet-tools.json` - specifies Cake Tool 1.3.0
- Uses GitVersion for semantic versioning

## Common Issues and Troubleshooting

### Build Issues
- **"Tool not found"**: Run `dotnet tool restore` first
- **Package not found**: Ensure NuGet packages created with `./build.sh --target=Create-NuGet-Packages`
- **GitVersion errors**: Ensure you're in git repository with proper remotes

### Integration Test Issues  
- **Package version mismatch**: Delete `~/.nuget/packages/cake.issues*` and rebuild packages
- **Test failures**: Check that you're using correct .NET target framework (net8.0 or net9.0)
- **Permission denied**: Some integration test `build.sh` files may need `chmod +x build.sh` to make executable

### Website Issues
- **Font loading errors**: Expected in sandboxed environments - fonts.gstatic.com blocked
- **mkdocs not found**: Install with `cd docs && pip install -r requirements.txt`

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
# Setup (run once)
dotnet tool restore

# Development cycle (run these in order)
./build.sh --target=DotNetCore-Build          # 3.5 min - build only
./build.sh --target=Test                      # 3 min   - run tests  
./build.sh --target=Create-NuGet-Packages     # 2 min   - create packages

# Integration testing (after packages created)
cd tests/Cake.Issues.MsBuild/script-runner/net9.0
./build.sh --verbosity=diagnostic             # 15 sec  - test specific addin

# Full validation
./build.sh                                    # 4 min   - complete CI build

# Documentation (limited in sandboxed environments)
cd docs && pip install -r requirements.txt   # 48 sec  - install deps
```

**Remember**: NEVER CANCEL long-running builds. Set timeouts appropriately and wait for completion.