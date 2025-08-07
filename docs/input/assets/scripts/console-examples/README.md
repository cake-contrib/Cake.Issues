# Console Report Format Example Scripts

This directory contains scripts used to generate the console output screenshots shown in the Console report format documentation.

## Purpose

These scripts allow reproducing the console output examples to:
- Regenerate screenshots when the addin is updated
- Test different console formatting options
- Validate console output behavior

## Scripts

- `generate-default.cake` - Default console output settings
- `generate-grouped.cake` - Console output grouped by rule
- `generate-summaries.cake` - Console output with provider and priority summaries
- `generate-combined.cake` - Console output with all features enabled
- `sample-issues.json` - Sample issues data used by all scripts

## Usage

To regenerate the console output for screenshots:

1. Ensure the latest Cake.Issues packages are built:
   ```bash
   cd /path/to/Cake.Issues
   ./build.sh --target=Create-NuGet-Packages
   ```

2. Run any of the example scripts:
   ```bash
   cd docs/input/assets/scripts/console-examples
   dotnet tool restore
   dotnet tool run dotnet-cake generate-default.cake --verbosity=minimal
   ```
   
   Or run all examples at once:
   ```bash
   ./run-examples.sh
   ```

3. Capture the console output for documentation screenshots

## Example Output

To see the individual issue details, run with normal verbosity. To see just the summaries (as shown in the screenshots), use `--verbosity=minimal`.

## Sample Data

The `sample-issues.json` file contains realistic issue data representing:
- MSBuild compiler warnings and suggestions
- DupFinder duplicate code detection issues  
- InspectCode style and best practice violations
- markdownlint documentation issues
- Custom script issues

This data produces output similar to what users would see in real projects.