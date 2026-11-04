#!/usr/bin/env bash

set -euo pipefail

TARGET_NAME="${1:-}"
IMAGE_NAME="${2:-}"

if [[ -z "$TARGET_NAME" || -z "$IMAGE_NAME" ]]; then
    echo "Usage: $0 <cake-target> <output-image-name>" >&2
    exit 1
fi

case "$TARGET_NAME" in
    Create-Reports-Console-Default|Create-Reports-Console-Grouped|Create-Reports-Console-Summaries|Create-Reports-Console-Combined)
        ;;
    *)
        echo "Unknown Console example target: $TARGET_NAME" >&2
        exit 1
        ;;
esac

for tool in dotnet script ansi2html wkhtmltoimage; do
    command -v "$tool" >/dev/null 2>&1 || {
        echo "$tool is required but not installed." >&2
        exit 1
    }
done

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "$SCRIPT_DIR/../../../.." && pwd)"
IMAGE_DIR="$REPO_ROOT/docs/input/assets/images/console-examples"
cd "$SCRIPT_DIR"

TEMP_DIR="$(mktemp -d)"
trap 'rm -rf "$TEMP_DIR"' EXIT

script -q -e -c "dotnet cake build.cake --target=$TARGET_NAME --settings_skippackageversioncheck=true" "$TEMP_DIR/console-output.txt"
ansi2html --scheme dracula < "$TEMP_DIR/console-output.txt" > "$TEMP_DIR/console-output.html"

cat > "$TEMP_DIR/styled-console.html" << 'EOF'
<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <style>
        body {
            background-color: #1e1e2e;
            color: #cdd6f4;
            font-family: 'Consolas', 'Monaco', 'Courier New', monospace;
            font-size: 14px;
            line-height: 1.2;
            margin: 20px;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.3);
        }
        pre {
            margin: 0;
            padding: 0;
            white-space: pre-wrap;
            word-wrap: break-word;
        }
    </style>
</head>
<body>
<pre>
EOF

sed -n '/Script started/,/Script done/p' "$TEMP_DIR/console-output.html" |
    sed '1d;$d' |
    sed 's/^[^>]*>//g' >> "$TEMP_DIR/styled-console.html"

cat >> "$TEMP_DIR/styled-console.html" << 'EOF'
</pre>
</body>
</html>
EOF

mkdir -p "$IMAGE_DIR"
wkhtmltoimage \
    --width 1200 \
    --height 800 \
    --quality 100 \
    --format png \
    --javascript-delay 1000 \
    --no-stop-slow-scripts \
    --enable-local-file-access \
    "$TEMP_DIR/styled-console.html" \
    "$IMAGE_DIR/$IMAGE_NAME"

echo "Screenshot saved to: $IMAGE_DIR/$IMAGE_NAME"
