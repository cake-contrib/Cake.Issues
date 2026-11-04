#!/usr/bin/env bash

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
cd "$SCRIPT_DIR"

for tool in dotnet script ansi2html wkhtmltoimage; do
    command -v "$tool" >/dev/null 2>&1 || {
        echo "$tool is required but not installed." >&2
        exit 1
    }
done

dotnet tool restore

./generate-screenshot.sh Create-Reports-Console-Default console-default.png
./generate-screenshot.sh Create-Reports-Console-Grouped console-grouped.png
./generate-screenshot.sh Create-Reports-Console-Summaries console-summaries.png
./generate-screenshot.sh Create-Reports-Console-Combined console-combined.png
