#!/bin/bash

# Script to generate console output and convert to PNG image
# Usage: generate-screenshot.sh <script-name> <output-image-name>

set -e

SCRIPT_NAME="$1"
IMAGE_NAME="$2"
DOCS_IMAGE_DIR="../../images/console-examples"

if [ -z "$SCRIPT_NAME" ] || [ -z "$IMAGE_NAME" ]; then
    echo "Usage: $0 <script-name> <output-image-name>"
    echo "Example: $0 generate-default.cake console-default.png"
    exit 1
fi

echo "Generating console output for $SCRIPT_NAME..."

# Create temp directory for output
TEMP_DIR=$(mktemp -d)
trap "rm -rf $TEMP_DIR" EXIT

# Generate console output with ANSI colors
script -q -c "dotnet tool run dotnet-cake $SCRIPT_NAME --settings_skippackageversioncheck=true 2>&1" "$TEMP_DIR/console-output.txt"

# Convert ANSI to HTML
ansi2html --scheme dracula < "$TEMP_DIR/console-output.txt" > "$TEMP_DIR/console-output.html"

# Add custom CSS for better terminal appearance
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

# Extract the content between first and last "Script" lines and add to styled HTML
sed -n '/Script started/,/Script done/p' "$TEMP_DIR/console-output.html" | \
    sed '1d;$d' | \
    sed 's/^[^>]*>//g' >> "$TEMP_DIR/styled-console.html"

cat >> "$TEMP_DIR/styled-console.html" << 'EOF'
</pre>
</body>
</html>
EOF

# Ensure docs image directory exists
mkdir -p "$DOCS_IMAGE_DIR"

# Convert HTML to PNG with specific dimensions and styling
wkhtmltoimage \
    --width 1200 \
    --height 800 \
    --quality 100 \
    --format png \
    --javascript-delay 1000 \
    --no-stop-slow-scripts \
    --enable-local-file-access \
    "$TEMP_DIR/styled-console.html" \
    "$DOCS_IMAGE_DIR/$IMAGE_NAME"

echo "Screenshot saved to: $DOCS_IMAGE_DIR/$IMAGE_NAME"