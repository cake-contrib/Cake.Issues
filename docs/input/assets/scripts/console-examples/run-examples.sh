#!/usr/bin/env bash

# Script to run all console example scripts and generate screenshots

set -e

echo "Generating all console example screenshots..."

# Ensure we have the required tools
command -v dotnet >/dev/null 2>&1 || { echo "dotnet is required but not installed. Aborting." >&2; exit 1; }
command -v ansi2html >/dev/null 2>&1 || { echo "ansi2html is required but not installed. Aborting." >&2; exit 1; }
command -v wkhtmltoimage >/dev/null 2>&1 || { echo "wkhtmltoimage is required but not installed. Aborting." >&2; exit 1; }

# Restore dotnet tools
echo "Restoring dotnet tools..."
dotnet tool restore

# Generate all screenshots
echo "Generating default screenshot..."
./generate-screenshot.sh generate-default.cake console-default.png

echo "Generating grouped screenshot..."  
./generate-screenshot.sh generate-grouped.cake console-grouped.png

echo "Generating summaries screenshot..."
./generate-screenshot.sh generate-summaries.cake console-summaries.png

echo "Generating combined screenshot..."
./generate-screenshot.sh generate-combined.cake console-combined.png

echo ""
echo "All screenshots have been generated successfully!"
echo ""
echo "Generated files:"
ls -la ../../images/console-examples/*.png

echo ""
echo "Screenshots are ready for use in the documentation."