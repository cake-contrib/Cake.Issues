# Console Report Format Example Scripts

This directory contains scripts used to generate the console output screenshots shown in the Console report format documentation.

## Purpose

These scripts allow reproducing the console output examples to:
- Regenerate screenshots when the addin is updated
- Test different console formatting options
- Validate console output behavior

## Scripts

- `generate-default.cake` - Default console output settings with individual diagnostics
- `generate-grouped.cake` - Console output grouped by rule
- `generate-summaries.cake` - Console output with provider and priority summaries
- `generate-combined.cake` - Console output with all features enabled
- `sample-issues.json` - Sample issues data used by all scripts
- `generate-screenshot.sh` - Helper script to generate PNG screenshots from console output
- `run-examples.sh` - Script to generate all screenshots at once

## Prerequisites

Before running the scripts, ensure you have:

1. **Built the NuGet packages** (required for the scripts to work):
   ```bash
   cd /path/to/Cake.Issues
   ./build.sh --target=Create-NuGet-Packages
   ```

2. **Required tools installed**:
   - `dotnet` (for running Cake scripts)
   - `ansi2html` (for converting ANSI terminal output to HTML)
   - `wkhtmltoimage` (for converting HTML to PNG images)

   Install the Python tools:
   ```bash
   pip install ansi2html
   sudo apt install wkhtmltopdf  # includes wkhtmltoimage
   ```

## Usage

### Generate All Screenshots

To regenerate all console output screenshots at once:

```bash
cd docs/input/assets/scripts/console-examples
./run-examples.sh
```

This will generate all four PNG files in `docs/input/assets/images/console-examples/`:
- `console-default.png`
- `console-grouped.png`
- `console-summaries.png`
- `console-combined.png`

### Generate Individual Screenshots

To generate a specific screenshot:

```bash
cd docs/input/assets/scripts/console-examples
./generate-screenshot.sh <script-name> <output-image-name>
```

Examples:
```bash
./generate-screenshot.sh generate-default.cake console-default.png
./generate-screenshot.sh generate-grouped.cake console-grouped.png
./generate-screenshot.sh generate-summaries.cake console-summaries.png
./generate-screenshot.sh generate-combined.cake console-combined.png
```

### Run Scripts Without Screenshots

To just run the examples and see the console output without generating images:

```bash
cd docs/input/assets/scripts/console-examples
dotnet tool restore
dotnet tool run dotnet-cake generate-default.cake --settings_skippackageversioncheck=true
```

## Screenshot Generation Process

The screenshot generation works as follows:

1. **Run Cake script** - Execute the console report script and capture all output including ANSI color codes
2. **Convert ANSI to HTML** - Use `ansi2html` to convert terminal colors to HTML with CSS
3. **Style HTML** - Apply additional CSS styling for a clean terminal appearance
4. **Generate PNG** - Use `wkhtmltoimage` to convert the styled HTML to a PNG image
5. **Save to docs** - Place the generated PNG in the documentation assets directory

## Sample Data

The `sample-issues.json` file contains realistic issue data representing:
- MSBuild compiler warnings and suggestions
- DupFinder duplicate code detection issues  
- InspectCode style and best practice violations
- markdownlint documentation issues
- Custom script issues

Supporting files (`src/ClassLibrary1/Class1.cs`, `docs/index.md`, `myfile.txt`) are included to ensure the issues reference existing files, which is required for the console diagnostics to display properly.

## Example Output

The scripts demonstrate four distinct console output modes:

### 1. Default Settings
Shows individual diagnostics for each issue with detailed file context and syntax highlighting.

### 2. Grouped by Rule (`GroupByRule: true`)
Groups issues with the same rule ID together, reducing redundancy and improving readability when multiple instances of the same issue exist.

### 3. With Summary Tables (`ShowProviderSummary: true`, `ShowPrioritySummary: true`)
Includes visual bar charts and detailed tables showing issue counts by provider and priority levels.

### 4. Combined Features
Demonstrates the most comprehensive output combining grouped diagnostics with summary tables.

This produces output similar to what users would see in real projects, helping them choose the right console report configuration for their needs.