#!/usr/bin/env bash

# Default settings example
echo "=== Running Default Console Report Example ==="
dotnet tool restore && dotnet tool run dotnet-cake generate-default.cake --verbosity=minimal

echo ""
echo "=== Running Grouped by Rule Example ==="
dotnet tool restore && dotnet tool run dotnet-cake generate-grouped.cake --verbosity=minimal

echo ""
echo "=== Running Summaries Example ==="
dotnet tool restore && dotnet tool run dotnet-cake generate-summaries.cake --verbosity=minimal

echo ""
echo "=== Running Combined Features Example ==="
dotnet tool restore && dotnet tool run dotnet-cake generate-combined.cake --verbosity=minimal