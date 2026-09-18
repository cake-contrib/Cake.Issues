#!/bin/bash
set -e

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

echo "Running Build"
dotnet run --project "$SCRIPT_DIR/build/Build.csproj" -- "$@"